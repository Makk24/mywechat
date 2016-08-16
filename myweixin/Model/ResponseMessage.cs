using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myweixin.Model.Enum;

namespace myweixin.Model
{
    /// <summary>
    /// 回复文本
    /// </summary>
    public class ResponseMessageText : ResponseMessageBase, IResponseMessageBase
    {
        public new virtual ResponseMsgType MsgType => ResponseMsgType.Text;
        public string Content { get; set; }
    }
    /// <summary>
    /// 回复图片
    /// </summary>
    public class ResponseMessageImage : ResponseMessageBase, IResponseMessageBase
    {
        public ResponseMessageImage()
        {
            Image=new Image();
        }
        public new virtual ResponseMsgType MsgType => ResponseMsgType.Image;
        public string MediaId { get; set; }
        public string PicUrl { get; set; }
        public Image Image { get; set; }
    }

    public class Image
    {
        public string MediaId { get; set; }
    }

    /// <summary>
    /// 回复语音
    /// </summary>
    public class ResponseMessageVoice : ResponseMessageBase, IResponseMessageBase
    {
        public ResponseMessageVoice()
        {
            Voice=new Voice();
        }

        public new virtual ResponseMsgType MsgType => ResponseMsgType.Voice;
        public Voice Voice { get; set; }
    }

    public class Voice
    {
        public string MediaId { get; set; }
    }
    /// <summary>
    /// 回复视频
    /// </summary>
    public class ResponseMessageVideo : ResponseMessageBase, IResponseMessageBase
    {
        public ResponseMessageVideo()
        {
            Video=new Video();
        }
        public new virtual ResponseMsgType MsgType => ResponseMsgType.Video;
        public Video Video { get; set; }
    }

    public class Video
    {
        public string MediaId { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }

    }
    /// <summary>
    /// 回复音乐消息
    /// </summary>
    public class ResponseMessageMusic : ResponseMessageBase, IResponseMessageBase
    {
        public ResponseMessageMusic()
        {
            Music=new Music();
        }
        public new virtual ResponseMsgType MsgType => ResponseMsgType.Music;
        public Music Music { get; set; }
    }

    public class Music
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MusicUrl { get; set; }
        public string HQMusicUrl { get; set; }
        /// <summary>
        /// 缩略图的媒体ID，通过上传多媒体文件，得到的ID
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
    /// <summary>
    /// 回复图文
    /// </summary>
    public class ResponseMessageNews : ResponseMessageBase, IResponseMessageBase
    {
        public ResponseMessageNews()
        {
            Articles = new List<Article>();
        }
        public new virtual ResponseMsgType MsgType => ResponseMsgType.News;

        public int ArticleCount
        {
            get { return (Articles ?? new List<Article>()).Count; }
            set
            {
                //这里开放set 只为了你想从Response的XML转成实体的操作一致性，没有实际意义
            }
        }
        /// <summary>
        /// 文章列表，微信客户端只能输出前十条（可能未来数字会有变化，由于视觉效果考虑，建议控制在八条以内）
        /// </summary>
        public List<Article> Articles { get; set; } 
    }

    public class Article
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string Url { get; set; }
    }
}