﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using myweixin.Model.Enum;

namespace myweixin.Model
{
    public static class Helper
    {
        /// <summary>
        /// 获取XDocument转换后的IRequestMessageBse示例
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static IRequestMessageBase GetRequestEntity(XDocument doc)
        {
            RequestMessageBase requestMessage = null;
            RequestMsgType msgType;
            try
            {
                msgType = MsgTypeHelper.GetRequestMsgType(doc);
                switch (msgType)
                {
                    case RequestMsgType.Text:
                        requestMessage = new RequestMessageText();
                        break;
                    case RequestMsgType.Location:
                        requestMessage = new RequestMessageLocation();
                        break;
                    case RequestMsgType.Image:
                        requestMessage = new RequestMessageImage();
                        break;
                    case RequestMsgType.Voice:
                        requestMessage = new RequestMessageVoice();
                        break;
                    case RequestMsgType.Video:
                        requestMessage = new RequestMessageVideo();
                        break;
                    case RequestMsgType.Link:
                        requestMessage = new RequestMessageLink();
                        break;
                    case RequestMsgType.Event:
                        Event eventType = EventHelper.GetEventType(doc);
                        //判断Event类型
                        switch (eventType)
                        {
                            case Event.Loction:
                                requestMessage = new RequestMessageEvent_Location();
                                break;
                            case Event.Subscribe:
                                requestMessage = new RequestMessageEvent_Subscribe();
                                break;
                            case Event.UnSubscribe:
                                requestMessage = new RequestMessageEvent_UnSubscribe();
                                break;
                            case Event.Click:
                                requestMessage = new RequestMessageEvent_Click();
                                break;
                            case Event.Scan:
                                requestMessage = new RequestMessageEvent_Scan();
                                break;
                            default: //其他意外类型（也可以选择抛出异常）
                                requestMessage = new RequestMessageEventBase();
                                break;
                        }
                        break;
                    default:
                        throw new UnKnownRequestMsgTypeException(
                            string.Format("MsgTYPE{0}在RequestMessageFactory中没有相应的处理程序", msgType),
                            new ArgumentOutOfRangeException());
                }
                FillEntityWithXml(requestMessage, doc);
            }
            catch (ArgumentException ex)
            {
                throw new WeixinException(string.Format("RequestMessage转换出错！可能是MsgType不存在，XML:{0}", doc.ToString()), ex);
            }
            return requestMessage;
        }

        public static void FillEntityWithXml<T>(this T entity, XDocument doc) where T : class, new()
        {
            entity = entity ?? new T();
            var root = doc.Root;
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                if (root.Element(propName) != null)
                {
                    switch (prop.PropertyType.Name)
                    {
                        case "DateTime":
                            prop.SetValue(entity, DateTimeHelper.GetDateTimeFromXml(root.Element(propName).Value), null);
                            break;
                        case "Int32":
                            prop.SetValue(entity, int.Parse(root.Element(propName).Value), null);
                            break;
                        case "Int64":
                            prop.SetValue(entity, long.Parse(root.Element(propName).Value), null);
                            break;
                        case "Double":
                            prop.SetValue(entity, double.Parse(root.Element(propName).Value), null);
                            break;
                        //以下为枚举类型
                        case "RequestMsgType":
                        case "ResponseMsgType":
                        case "Event":
                            break;
                        //以下为实体类型
                        case "List`1"://List<T>类型，ResponseMessageNews适用
                            var genericArguments = prop.PropertyType.GetGenericArguments();
                            if (genericArguments[0].Name == "Article")//ResponseMessageNews适用
                            {
                                //文章节点item
                                List<Article> articles=new List<Article>();
                                foreach (var item in root.Element(propName).Elements("item"))
                                {
                                    var article=new Article();
                                    FillEntityWithXml(article,new XDocument(item));
                                    articles.Add(article);
                                }
                                prop.SetValue(entity,articles,null);
                            }
                            break;
                        case "Music"://ResponseMessageMusic适用
                            Music music=new Music();
                            FillEntityWithXml(music,new XDocument(root.Element(propName)));
                            prop.SetValue(entity,music,null);
                            break;
                        case "Image"://ResponseMessageImage适用
                            Image image = new Image();
                            FillEntityWithXml(image, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, image, null);
                            break;
                        case "Voice"://ResponseMessageVoice适用
                            Voice voice = new Voice();
                            FillEntityWithXml(voice, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, voice, null);
                            break;
                        case "Video"://ResponseMessageVideo适用
                            Video video = new Video();
                            FillEntityWithXml(video, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, video, null);
                            break;
                        default:
                            prop.SetValue(entity,root.Element(propName).Value, null);
                            break;
                    }
                }
            }
        }

        public static XDocument ConvertEntityToXml<T>(this T entity) where T : class, new()
        {
            entity = entity ?? new T();
            var doc=new XDocument();
            doc.Add(new XElement("xml"));
            var root = doc.Root;
            /*微信对字段排序有严格要求，这里对排序进行强制约束*/
            var propNameOrder = new List<string>() {"ToUserName", "FromUserName", "CreateTime", "MsgType"};
            //不同返回类型需要对应不同的特殊格式进行排序
            if (entity is ResponseMessageNews)
            {
                propNameOrder.AddRange(new[]{"ArticleCount","Articles",/*以下是Article属性*/"Title","Description","PicUrl","Url"});
            }else if (entity is ResponseMessageMusic)
            {
                propNameOrder.AddRange(new[] { "Music",/*以下是Music属性*/"Title", "Description", "MusicUrl", "HQMusicUrl" });
            }
            else if (entity is ResponseMessageImage)
            {
                propNameOrder.AddRange(new[] { "Image", /*以下是Image属性*/"MediaId"});
            }
            else if (entity is ResponseMessageVoice)
            {
                propNameOrder.AddRange(new[] { "Voice", /*以下是Voice属性*/"MediaId" });
            }
            else if (entity is ResponseMessageVideo)
            {
                propNameOrder.AddRange(new[] { "Video",/*以下是Video属性*/"MediaId" });
            }
            else 
            {
                //如Text类型
                propNameOrder.AddRange(new[] { "Content"});
            }
            Func<string, int> orderByPropName = propNameOrder.IndexOf;
            var props = entity.GetType().GetProperties().OrderBy(p => orderByPropName(p.Name)).ToList();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                if (propName == "Articles")
                {
                    //文章列表
                    var articlesElement=new XElement("Articles");
                    var articles = prop.GetValue(entity, null) as List<Article>;
                    foreach (var article in articles)
                    {
                        var subNodes = ConvertEntityToXml(article).Root.Elements();
                        articlesElement.Add(new XElement("item",subNodes));
                    }
                    root.Add(articlesElement);
                }else if (propName == "Music" || propName == "Image" || propName == "Video" || propName == "Voice" ||
                          propName == "Music")
                {
                    var musicElement = new XElement(propName);
                    var media = prop.GetValue(entity, null);
                    var subNodes = ConvertEntityToXml(media).Root.Elements();
                    musicElement.Add(subNodes);
                    root.Add(musicElement);
                }
                else
                {
                    switch (prop.PropertyType.Name)
                    {
                        case "String":
                            root.Add(new XElement(propName,new XCData(prop.GetValue(entity,null) as string ??"")));
                            break;
                        case "DateTime":
                            root.Add(new XElement(propName, DateTimeHelper.GetWeixingDateTime((DateTime)prop.GetValue(entity, null))));
                            break;
                        case "ResponseMsgType":
                            root.Add(new XElement(propName, new XCData(prop.GetValue(entity, null).ToString().ToLower())));
                            break;
                        case "Article":
                            root.Add(new XElement(propName, prop.GetValue(entity, null) as string ?? ""));
                            break;
                        default:
                            root.Add(new XElement(propName,prop.GetValue(entity, null)));
                            break;
                    }
                }
            }
            return doc;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class MsgTypeHelper
    {
        #region RequestMsgType

        public static RequestMsgType GetRequestMsgType(XDocument doc)
        {
            var xElement = doc.Root?.Element("MsgType");
            return GetRequestMsgType(xElement.Value);
        }

        public static RequestMsgType GetRequestMsgType(string str)
        {
            return (RequestMsgType)System.Enum.Parse(typeof(RequestMsgType), str, true);
        }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public static class EventHelper
    {
        #region RequestMsgType

        public static Event GetEventType(XDocument doc)
        {
            var xElement = doc.Root?.Element("Event");
            return GetEventType(xElement.Value);
        }

        public static Event GetEventType(string str)
        {
            return (Event)System.Enum.Parse(typeof(Event), str, true);
        }
        #endregion
    }

    public static class DateTimeHelper
    {
        public static DateTime BaseTime = new DateTime(1970, 1, 1);//Unix起始时间
        /// <summary>
        /// 转换微信时间为c#时间
        /// </summary>
        /// <param name="dateTimeFromXml"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromXml(long dateTimeFromXml)
        {
            return BaseTime.AddTicks((dateTimeFromXml + 8 * 60 * 60) * 10000000);
        }
        /// <summary>
        /// 转换微信时间为c#时间
        /// </summary>
        /// <param name="dateTimeFromXml"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromXml(string dateTimeFromXml)
        {
            return GetDateTimeFromXml(long.Parse(dateTimeFromXml));
        }
        /// <summary>
        /// 获取微信时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetWeixingDateTime(DateTime dateTime)
        {
            return (dateTime.Ticks - BaseTime.Ticks) / 10000000 - 8 * 60 * 60;
        }
    }
}