using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myweixin.Model.Enum;

namespace myweixin.Model
{
    /// <summary>
    /// 公共号回复消息数据格式规范
    /// </summary>
    public interface IResponseMessageBase : IMessageBase
    {
        /// <summary>
        /// 公共号回复消息类型
        /// </summary>
        ResponseMsgType MsgType { get; }
    }
    /// <summary>
    /// 公共号回复消息基类
    /// </summary>
    public class ResponseMessageBase : MessageBase, IResponseMessageBase
    {
        /// <summary>
        /// 公共号回复消息类型
        /// </summary>
        public virtual ResponseMsgType MsgType => ResponseMsgType.Text;
    }
}