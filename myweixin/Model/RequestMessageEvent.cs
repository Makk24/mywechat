using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myweixin.Model.Enum;

namespace myweixin.Model
{
    /// <summary>
    /// 关注
    /// </summary>
    public class RequestMessageEvent_Subscribe:RequestMessageEventBase,IRequestMessageEventBase
    {
        public override Event Event =>Event.Subscribe;
    }
    /// <summary>
    /// 取消关注
    /// </summary>
    public class RequestMessageEvent_UnSubscribe : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event => Event.UnSubscribe;
    }
    /// <summary>
    /// 扫描带参数二维码
    /// </summary>
    public class RequestMessageEvent_Scan : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event => Event.Scan;
        /// <summary>
        /// 二维码参数
        /// </summary>
        public string Ticket { get; set; }
    }
    /// <summary>
    /// 上报地理位置
    /// </summary>
    public class RequestMessageEvent_Location : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event => Event.Loction;
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public double Precision { get; set; }
    }
    /// <summary>
    /// 自定义菜单单击事件
    /// </summary>
    public class RequestMessageEvent_Click : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event => Event.Click;
    }
}