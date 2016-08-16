using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myweixin.Model.Enum;

namespace myweixin.Model
{
    /// <summary>
    /// 文本
    /// </summary>
    public class RequestMessageText:RequestMessageBase,IRequestMessageBase
    {
        public override RequestMsgType MsgType => RequestMsgType.Text;
        public string Content { get; set; }
    }
    /// <summary>
    /// 图片
    /// </summary>
    public class RequestMessageImage : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType => RequestMsgType.Image;
        public string MediaId { get; set; }
        public string PicUrl { get; set; }
    }
    /// <summary>
    /// 语音
    /// </summary>
    public class RequestMessageVoice : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType => RequestMsgType.Voice;
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式：amr
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 语音识别结果，UTF8编码
        /// </summary>
        public string Recognition { get; set; }
    }
    /// <summary>
    /// 视频
    /// </summary>
    public class RequestMessageVideo : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType => RequestMsgType.Video;
        public string MediaId { get; set; }
        public string ThumbMediaId { get; set; }
    }
    /// <summary>
    /// 地理位置
    /// </summary>
    public class RequestMessageLocation : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType => RequestMsgType.Location;
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Location_Y { get; set; }
        public int Scale { get; set; }
        public string Label { get; set; }
    }
    /// <summary>
    /// 链接
    /// </summary>
    public class RequestMessageLink : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType => RequestMsgType.Link;
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}