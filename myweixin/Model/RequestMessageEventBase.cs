using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myweixin.Model.Enum;

namespace myweixin.Model
{
    public interface IRequestMessageEventBase : IRequestMessageBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        Event Event { get; }
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中的KEY值对应
        /// </summary>
        string EventKey { get; set; }
    }
    /// <summary>
    /// 用户发送事件消息基类
    /// </summary>
    public class RequestMessageEventBase:RequestMessageBase,IRequestMessageEventBase
    {
        public override RequestMsgType MsgType=>RequestMsgType.Event;
        /// <summary>
        /// 事件类型
        /// </summary>
        public virtual Event Event=>Event.Click;
        /// <summary>
        /// 事件key值，与自定义菜单接口中的key值对应
        /// </summary>
        public string EventKey { get; set; }
    }
}