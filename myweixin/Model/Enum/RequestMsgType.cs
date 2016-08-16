namespace myweixin.Model.Enum
{
    /// <summary>
    /// 用户发送消息类型
    /// </summary>
    public enum RequestMsgType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        Text,
        /// <summary>
        /// 地理位置消息
        /// </summary>
        Location,
        /// <summary>
        /// 图片
        /// </summary>
        Image,
        /// <summary>
        /// 语音
        /// </summary>
        Voice,
        /// <summary>
        /// 视频
        /// </summary>
        Video,
        /// <summary>
        /// 链接
        /// </summary>
        Link,
        /// <summary>
        /// 事件消息
        /// </summary>
        Event
    }
}