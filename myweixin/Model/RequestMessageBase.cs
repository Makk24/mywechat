﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myweixin.Model.Enum;

namespace myweixin.Model
{
    /// <summary>
    /// 用户发送消息数据格式规范
    /// </summary>
    public interface IRequestMessageBase:IMessageBase
    {
        /// <summary>
        /// 用户发送消息类型
        /// </summary>
        RequestMsgType MsgType { get; }
        /// <summary>
        /// 消息ID
        /// </summary>
        long MsgId { get; set; }
    }
    /// <summary>
    /// 用户发送消息基类
    /// </summary>
    public class RequestMessageBase:MessageBase,IRequestMessageBase
    {
        /// <summary>
        /// 用户发送消息类型
        /// </summary>
        public virtual RequestMsgType MsgType => RequestMsgType.Text;
        /// <summary>
        /// 消息ID
        /// </summary>
        public long MsgId { get; set; }
    }
}