using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myweixin.Model
{
    /// <summary>
    /// 所有Request ,Response消息的数据格式规范
    /// </summary>
    public interface IMessageBase
    {
       string ToUserName { get; set; }
       string FromUserName { get; set; }
       DateTime CreateTime { get; set; }
    }
    /// <summary>
    /// 所有Request ,Response消息的基类
    /// </summary>
    public class MessageBase
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}