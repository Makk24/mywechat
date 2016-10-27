using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myweixin.Model.WeixinContext
{
    public interface IMessageContext
    {
        /// <summary>
        /// 用户名（）OpenID
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        ///一次活动时间（用户主动发送Resquest请求的时间）
        /// </summary>
        DateTime LastActiveTime { get; set; }
        /// <summary>
        /// 用户发送消息记录
        /// </summary>
        MessageContainer<IRequestMessageBase> RequestMessages { get; set; }
        /// <summary>
        /// 用户接收消息记录
        /// </summary>
        MessageContainer<IResponseMessageBase> ResponsMessages { get; set; }
        /// <summary>
        /// 最大存储容量（分别针对RequestMessages ，ResponsMessages）
        /// </summary>
        int MaxRecordCount { get; set; }
        /// <summary>
        /// 临时存储数据，如用户状态
        /// </summary>
        object StorageData { get; set; }
        /// <summary>
        /// 由具体业务场景根据需求，编写用户上下文被从集合移除时需要额外执行的方法注册到该事件上
        /// </summary>
        event EventHandler<WeixinContextRemovedEventArgs> MessageContextRemoved;
       /// <summary>
       /// 供集合移除用户回话上下文时调用的回调的方法
       /// </summary>
        void OnRemoved();
    }

    public class MessageContext : IMessageContext
    {
        private int _maxRecordCount;
        public string UserName { get; set; }

        public DateTime LastActiveTime { get; set; }

        public MessageContainer<IRequestMessageBase> RequestMessages { get; set; }

        public MessageContainer<IResponseMessageBase> ResponsMessages { get; set; }

        public int MaxRecordCount
        {
            get { return _maxRecordCount; }

            set
            {
                RequestMessages.MaxRecordCount = value;
                ResponsMessages.MaxRecordCount = value;
                _maxRecordCount = value;
            }
        }

        public object StorageData { get; set; }

        public event EventHandler<WeixinContextRemovedEventArgs> MessageContextRemoved=null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MessageContext()
        {
            RequestMessages=new MessageContainer<IRequestMessageBase>(MaxRecordCount);
            ResponsMessages=new MessageContainer<IResponseMessageBase>(MaxRecordCount);
            LastActiveTime=DateTime.Now;
        }
        public virtual void OnRemoved()
        {
            var onRemovedArg=new WeixinContextRemovedEventArgs(this);
            onMessageContextRemoved(onRemovedArg);
        }
        /// <summary>
        /// 执行上下文被移除的事件
        /// 此事件不是实时触发的，而是等过期后，在任意一个人发过来的下一条消息执行之前触发
        /// </summary>
        /// <param name="e"></param>
        private void onMessageContextRemoved(WeixinContextRemovedEventArgs e)
        {
            EventHandler<WeixinContextRemovedEventArgs> temp = MessageContextRemoved;
            if (temp != null)
            {
                temp(this, e);
            }
        }
    }
}