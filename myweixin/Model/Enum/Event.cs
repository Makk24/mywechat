namespace myweixin.Model.Enum
{
    /// <summary>
    /// 当RequestMsgType类型为Event时，Event属性的类型
    /// </summary>
    public enum Event
    {
        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        Loction,
        /// <summary>
        /// 关注事件
        /// </summary>
        Subscribe,
        /// <summary>
        /// 取消关注事件
        /// </summary>
        UnSubscribe,
        /// <summary>
        /// 自定义菜单单击事件
        /// </summary>
        Click,
        /// <summary>
        /// 扫面带参数二维码事件
        /// </summary>
        Scan
    }
}