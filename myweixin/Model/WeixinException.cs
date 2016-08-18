using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myweixin.Model
{
    /// <summary>
    /// 微信异常处理基类
    /// </summary>
    public class WeixinException:ApplicationException
    {
        public WeixinException(string message):base(message,null)
        {
            
        }
        public WeixinException (string message,Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// 未知请求类型
    /// </summary>
    public class UnKnownRequestMsgTypeException : WeixinException
    {
        public UnKnownRequestMsgTypeException(string message) : base(message) { }
        public UnKnownRequestMsgTypeException(string message,Exception inner) : base(message, inner) { }
    }
}