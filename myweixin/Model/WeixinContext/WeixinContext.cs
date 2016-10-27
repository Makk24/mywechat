using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace myweixin.Model.WeixinContext
{
    public static class WeixinContetGlobal
    {
        public static object Lock=new object();
        /// <summary>
        /// 是否开启上下文记录
        /// </summary>
        public static bool UseWeixinContext = true;
    }
    /// <summary>
    /// 对话上下文被删除时触发事件的事件数据
    /// </summary>
    public class WeixinContextRemovedEventArgs : EventArgs
    {
        /// <summary>
        /// 用户OpenID
        /// </summary>
        public string OpenId
        {
            get { return MessageContext.UserName; }
        }
        /// <summary>
        /// 最后一次响应时间
        /// </summary>
        public DateTime LastActiveTime { get { return MessageContext.LastActiveTime; } }
        /// <summary>
        /// 上下文对象
        /// </summary>
        public IMessageContext MessageContext { get; set; }

        public WeixinContextRemovedEventArgs(IMessageContext messageContext)
        {
            MessageContext = messageContext;
        }
    }

    /// <summary>
    /// 微信消息上下文（全局）
    /// 默认过期时间：90分钟
    /// </summary>
    public class WeixinContext<TM> where TM : class, IMessageContext, new()
    {
        /// <summary>
        /// 默认过期时间
        /// </summary>
        private const int defaulTexpireMinutes = 90;
        /// <summary>
        /// 所有的MessageContext集合
        /// </summary>
        protected Dictionary<string, TM> MessageCollection { get; set; }
        /// <summary>
        /// MessageContext队列(LastActiveTime升序排序)
        /// </summary>
        protected List<TM> MessageQueue { get; set; }
        /// <summary>
        /// 每一个MessageContext过期时间
        /// </summary>
        public Double ExpireMinutes { get; set; }
        /// <summary>
        /// 最大存储上下文数量（分别针对请求和响应信息）
        /// </summary>
        public int MaxRecordCount { get; set; }

        public WeixinContext()
        {
            Restore();
        }
        /// <summary>
        /// 重置参数
        /// </summary>
        private void Restore()
        {
            MessageCollection =new Dictionary<string, TM>(StringComparer.OrdinalIgnoreCase);
            MessageQueue=new List<TM>();
            ExpireMinutes = defaulTexpireMinutes;
        }
        /// <summary>
        /// 获取MessageContext,如果不存在，则返回NUll
        /// 及时移除过期对象
        /// </summary>
        /// <param name="userName">用户名OpenID</param>
        /// <returns></returns>
        private TM GetMessageContext(string userName)
        {
            //检查移除过期对象
            while (MessageQueue.Count>0)
            {
                var firstMessageContext = MessageQueue[0];
                var timeSpan = DateTime.Now - firstMessageContext.LastActiveTime;
                if (timeSpan.TotalMinutes >= ExpireMinutes)
                {
                    MessageQueue.RemoveAt(0); //从队列中移除过期对象
                    MessageCollection.Remove(firstMessageContext.UserName); //从集合中移除过期对象
                    //添加回调事件
                    firstMessageContext.OnRemoved(); //todo:此处异步处理，或用户自己操作的时候异步处理需要耗时的操作
                }
                else
                {
                    break;
                }
            }
            if (!MessageCollection.ContainsKey(userName))
            {
                return null;
            }
            return MessageCollection[userName];
        }
        /// <summary>
        /// 获取MessageContext
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="createIfNotExists"></param>
        /// <returns></returns>
        private TM GetMessageContext(string userName, bool createIfNotExists)
        {
            var messageContext = GetMessageContext(userName);
            if (messageContext == null)
            {
                if (createIfNotExists)
                {
                    MessageCollection[userName] = new TM()
                    {
                        UserName = userName,
                        MaxRecordCount = MaxRecordCount
                    };
                    MessageQueue.Add(messageContext); //最新的排到队尾
                }
                else
                {
                    return null;
                }
            }
            return messageContext;
        }
        /// <summary>
        /// 获取MessageContext,如果不存在，使用requestMessage初始化一个
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public TM GetMessageContext(IRequestMessageBase requestMessage)
        {
            lock (WeixinContetGlobal.Lock)
            {
                return GetMessageContext(requestMessage.FromUserName,true);
            }
        }
        /// <summary>
        /// 获取MessageContext,如果不存在，使用responseMessage初始化一个
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public TM GetMessageContext(IResponseMessageBase responseMessage)
        {
            lock (WeixinContetGlobal.Lock)
            {
                return GetMessageContext(responseMessage.FromUserName, true);
            }
        }
        /// <summary>
        /// 记录请求信息
        /// </summary>
        /// <param name="requestMessage"></param>
        public void InsertMessage(IRequestMessageBase requestMessage)
        {
            lock (WeixinContetGlobal.Lock)
            {
                var userName = requestMessage.FromUserName;
                var messageContext = GetMessageContext(userName, true);
                if (messageContext.RequestMessages.Count > 0)
                {
                    //如果不是新对象，则把当前对象移到队列尾部
                    var messageContextInQueue = MessageQueue.FindIndex(z => z.UserName == userName);
                    if (messageContextInQueue >= 0)
                    {
                        MessageQueue.RemoveAt(messageContextInQueue);
                        MessageQueue.Add(messageContext);//移除当前对象，插入到队尾
                    }
                }
                messageContext.LastActiveTime=DateTime.Now;
                messageContext.RequestMessages.Add(requestMessage);//录入消息
            }
        }

        /// <summary>
        /// 记录响应消息
        /// </summary>
        /// <param name="responseMessage"></param>
        public void InsertMessage(IResponseMessageBase responseMessage)
        {
            lock (WeixinContetGlobal.Lock)
            {
                var messageContext = GetMessageContext(responseMessage.ToUserName, true);
                messageContext.ResponsMessages.Add(responseMessage);
            }
        }
        /// <summary>
        /// 获取最新一条请求数据，不存在返回null
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IRequestMessageBase GetLastRequestMessage(string userName)
        {
            lock (WeixinContetGlobal.Lock)
            {
                var messageContext = GetMessageContext(userName, true);
                return messageContext.RequestMessages.LastOrDefault();
            }
        }
        /// <summary>
        /// 获取最新一条响应数据，不存在返回null
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IResponseMessageBase GetLastResponseMessage(string userName)
        {
            lock (WeixinContetGlobal.Lock)
            {
                var messageContext = GetMessageContext(userName, true);
                return messageContext.ResponsMessages.LastOrDefault();
            }
        }
    }
    }