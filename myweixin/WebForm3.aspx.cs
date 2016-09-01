using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GetHtmlPages;
using Newtonsoft.Json;

namespace myweixin
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            string doi = TextBox1.Text;
            List<string> doiList = doi.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (doiList.Count <= 0)
            {
                Response.Write("doi填写错误");
                return;
            }
            string url = "http://api.altmetric.com/v1/doi/";

            ReturnModel model = null;
            List<ReturnModel> list = new List<ReturnModel>();
            foreach (var option in doiList)
            {

                var returnStr = GetHttpPage(url + option, "", "get");
                try
                {
                    model = JsonConvert.DeserializeObject<ReturnModel>(returnStr);
                }
                catch
                {
                    model = null;
                }
                if (model != null)
                {
                    list.Add(model);
                }
            }
            if (list.Count <= 0)
            {
                Response.Write("not find data");
            }
            CreateExcel(list);
        }
        public static string GetHttpPage(string url, string ntext, string post)
        {
            string ApiStatus = string.Empty;

            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                try
                {
                    if (!string.IsNullOrEmpty(post) && post.ToLower() == "post".ToLower())
                    {
                        ApiStatus = wc.UploadString(url, "POST", ntext);
                    }
                    else
                    {
                        ApiStatus = wc.DownloadString(url + "?" + ntext);
                    }
                }
                catch (Exception ex)
                {
                    ApiStatus = "ERROR:" + ex.Message.ToString();
                }
            }
            return ApiStatus;
        }
        /// <summary>
        /// 生成模板
        /// </summary>
        /// <param name="data">数据</param>
        private static void CreateExcel(List<ReturnModel> data)
        {
            #region "表头"
            StringBuilder strb = new StringBuilder();
            strb.Append(" <html xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            strb.Append("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            strb.Append("xmlns=\"http://www.w3.org/TR/REC-html40\"");
            strb.Append(" <head> <meta http-equiv='Content-Type' content='text/html; charset=gb2312'>");
            strb.Append(" <style>");
            strb.Append(".xl26");
            strb.Append(" {mso-style-parent:style0;");
            strb.Append(" font-family:\"Times New Roman\", serif;");
            strb.Append(" mso-font-charset:0;");
            strb.Append(" mso-number-format:\"@\";}");
            strb.Append(" </style>");
            strb.Append(" <xml>");
            strb.Append(" <x:ExcelWorkbook>");
            strb.Append("  <x:ExcelWorksheets>");
            strb.Append("  <x:ExcelWorksheet>");
            strb.Append("    <x:Name>Sheet1 </x:Name>");
            strb.Append("    <x:WorksheetOptions>");
            strb.Append("    <x:DefaultRowHeight>285 </x:DefaultRowHeight>");
            strb.Append("    <x:Selected/>");
            strb.Append("    <x:Panes>");
            strb.Append("      <x:Pane>");
            strb.Append("      <x:Number>3 </x:Number>");
            strb.Append("      <x:ActiveCol>1 </x:ActiveCol>");
            strb.Append("      </x:Pane>");
            strb.Append("    </x:Panes>");
            strb.Append("    </x:WorksheetOptions>");
            strb.Append("  </x:ExcelWorksheet>");
            strb.Append("  <x:WindowHeight>6750 </x:WindowHeight>");
            strb.Append("  <x:WindowWidth>10620 </x:WindowWidth>");
            strb.Append("  <x:WindowTopX>480 </x:WindowTopX>");
            strb.Append("  <x:WindowTopY>75 </x:WindowTopY>");
            strb.Append(" </x:ExcelWorkbook>");
            strb.Append(" </xml>");
            strb.Append("");
            strb.Append("<style> .xl28{mso-style-parent:style0;	mso-number-format:'\\@';	white-space:normal;	mso-font-charset:134;}</style>");
            strb.Append(" </head> <body> <table align=\"center\" style='border-collapse:collapse;table-layout:fixed'> <tr >");

            #endregion

            strb.Append("<th rowspan='3'>title</th>");
            strb.Append("<th rowspan='3'>doi</th>");
            strb.Append("<th rowspan='3'>pmid</th>");
            strb.Append("<th rowspan='3'>tq</th>");
            strb.Append("<th rowspan='3'>ads_id</th>");
            strb.Append("<th rowspan='3'>altmetric_jid</th>");
            strb.Append("<th rowspan='3'>issns</th>");
            strb.Append("<th rowspan='3'>journal</th>");
            strb.Append("<th colspan='4'>cohorts</th>");
            strb.Append("<th colspan='20'>context</th>");
            strb.Append("<th rowspan='3'>type</th>");
            strb.Append("<th rowspan='3'>altmetric_id</th>");
            strb.Append("<th rowspan='3'>schema</th>");
            strb.Append("<th rowspan='3'>is_oa</th>");
            strb.Append("<th rowspan='3'>cited_by_fbwalls_count</th>");

            strb.Append("<th rowspan='3'>cited_by_feeds_count</th>");
            strb.Append("<th rowspan='3'>cited_by_gplus_count</th>");
            strb.Append("<th rowspan='3'>cited_by_posts_count</th>");
            strb.Append("<th rowspan='3'>cited_by_tweeters_count</th>");
            strb.Append("<th rowspan='3'>cited_by_accounts_count</th>");
            strb.Append("<th rowspan='3'>last_updated</th>");
            strb.Append("<th rowspan='3'>score</th>");
            strb.Append("<th rowspan='3'>url</th>");
            strb.Append("<th rowspan='3'>added_on</th>");
            strb.Append("<th rowspan='3'>published_on</th>");
            strb.Append("<th rowspan='3'>subjects</th>");
            strb.Append("<th colspan='3'>readers</th>");
            strb.Append("<th rowspan='3'>readers_count</th>");
            strb.Append("<th colspan='3'>images</th>");
            strb.Append("<th rowspan='3'>details_url</th>");
            strb.Append(" </tr>");

            strb.Append("<tr > ");
            strb.Append("<th rowspan='2'>sci</th>");
            strb.Append("<th rowspan='2'>pub</th>");
            strb.Append("<th rowspan='2'>com</th>");
            strb.Append("<th rowspan='2'>doc</th>");
            strb.Append("<th colspan='5'>all</th>");
            strb.Append("<th colspan='5'>journal</th>");
            strb.Append("<th colspan='5'>similar_age_3m</th>");
            strb.Append("<th colspan='5'>similar_age_journal_3m</th>");
            strb.Append("<th rowspan='2'>citeulike</th>");
            strb.Append("<th rowspan='2'>mendeley</th>");
            strb.Append("<th rowspan='2'>connotea</th>");
            strb.Append("<th rowspan='2'>small</th>");
            strb.Append("<th rowspan='2'>medium</th>");
            strb.Append("<th rowspan='2'>large</th>");
            strb.Append(" </tr>");

            strb.Append("<tr > ");
            strb.Append("<th >count</th>");
            strb.Append("<th >mean</th>");
            strb.Append("<th >rank</th>");
            strb.Append("<th >pct</th>");
            strb.Append("<th >higher_than</th>");
            strb.Append("<th >count</th>");
            strb.Append("<th >mean</th>");
            strb.Append("<th >rank</th>");
            strb.Append("<th >pct</th>");
            strb.Append("<th >higher_than</th>");
            strb.Append("<th >count</th>");
            strb.Append("<th >mean</th>");
            strb.Append("<th >rank</th>");
            strb.Append("<th >pct</th>");
            strb.Append("<th >higher_than</th>");
            strb.Append("<th >count</th>");
            strb.Append("<th >mean</th>");
            strb.Append("<th >rank</th>");
            strb.Append("<th >pct</th>");
            strb.Append("<th >higher_than</th>");
            strb.Append(" </tr>");
            string goodType = string.Empty;
            foreach (ReturnModel item in data)
            {
                strb.Append("<tr>");
                strb.AppendFormat("<td >{0}</td>", item.title);
                strb.AppendFormat("<td>{0}</td>", item.doi);
                strb.AppendFormat("<td x:str>{0}</td>", item.pmid);
                strb.AppendFormat("<td>{0}</td>", ArrayToString(item.tq));
                strb.AppendFormat("<td>{0}</td>", item.ads_id);
                strb.AppendFormat("<td>{0}</td>", item.altmetric_jid);
                strb.AppendFormat("<td >{0}</td>", ArrayToString(item.issns));
                strb.AppendFormat("<td>{0}</td>", item.journal);
                strb.AppendFormat("<td >{0}</td>", item.cohorts.sci);
                strb.AppendFormat("<td >{0}</td>", item.cohorts.pub);
                strb.AppendFormat("<td>{0}</td>", item.cohorts.com);
                strb.AppendFormat("<td x:str>{0}</td>", item.cohorts.doc);
                strb.AppendFormat("<td  x:str>{0}</td>", item.context.all.count);
                strb.AppendFormat("<td x:str>{0}</td>", item.context.all.mean);
                strb.AppendFormat("<td x:str>{0}</td>", item.context.all.rank);
                strb.AppendFormat("<td x:str >{0}</td>", item.context.all.pct);
                strb.AppendFormat("<td x:str>{0}</td>", item.context.all.higher_than);
                strb.AppendFormat("<td x:str >{0}</td>", item.context.journal.count);
                strb.AppendFormat("<td x:str>{0}</td>", item.context.journal.mean);
                strb.AppendFormat("<td x:str >{0}</td>", item.context.journal.rank);
                strb.AppendFormat("<td x:str >{0}</td>", item.context.journal.pct);
                strb.AppendFormat("<td x:str>{0}</td>", item.context.journal.higher_than);
                strb.AppendFormat("<td x:str >{0}</td>", item.context.similar_age_3m.count);
                strb.AppendFormat("<td x:str>{0}</td>", item.context.similar_age_3m.mean);
                strb.AppendFormat("<td x:str>{0}</td>", item.context.similar_age_3m.rank);
                strb.AppendFormat("<td x:str >{0}</td>", item.context.similar_age_3m.pct);
                strb.AppendFormat("<td x:str>{0}</td>", item.context.similar_age_3m.higher_than);
                strb.AppendFormat("<td x:str >{0}</td>", item.context.similar_age_journal_3m.count);
                strb.AppendFormat("<td x:str>{0}</td>", item.context.similar_age_journal_3m.mean);
                strb.AppendFormat("<td x:str>{0}</td>", item.context.similar_age_journal_3m.rank);
                strb.AppendFormat("<td x:str >{0}</td>", item.context.similar_age_journal_3m.pct);
                strb.AppendFormat("<td x:str>{0}</td>", item.context.similar_age_journal_3m.higher_than);

                strb.AppendFormat("<td x:str>{0}</td>", item.type);
                strb.AppendFormat("<td  x:str>{0}</td>", item.altmetric_id);
                strb.AppendFormat("<td  x:str>{0}</td>", item.schema);
                strb.AppendFormat("<td x:str>{0}</td>", item.is_oa);
                strb.AppendFormat("<td x:str >{0}</td>", item.cited_by_fbwalls_count);
                strb.AppendFormat("<td x:str>{0}</td>", item.cited_by_feeds_count);
                strb.AppendFormat("<td x:str>{0}</td>", item.cited_by_gplus_count);
                strb.AppendFormat("<td x:str >{0}</td>", item.cited_by_posts_count);
                strb.AppendFormat("<td x:str>{0}</td>", item.cited_by_tweeters_count);
                strb.AppendFormat("<td x:str>{0}</td>", item.cited_by_accounts_count);
                strb.AppendFormat("<td x:str >{0}</td>", GetDateTimeFromXml(item.last_updated));
                strb.AppendFormat("<td x:str>{0}</td>", item.score);
                strb.AppendFormat("<td x:str >{0}</td>", item.url); ;
                strb.AppendFormat("<td x:str >{0}</td>", GetDateTimeFromXml(item.added_on));
                strb.AppendFormat("<td x:str>{0}</td>", GetDateTimeFromXml(item.published_on));
                strb.AppendFormat("<td x:str>{0}</td>", ArrayToString(item.subjects));
                strb.AppendFormat("<td x:str >{0}</td>", item.readers.citeulike);
                strb.AppendFormat("<td x:str>{0}</td>", item.readers.mendeley);
                strb.AppendFormat("<td x:str>{0}</td>", item.readers.connotea);

                strb.AppendFormat("<td x:str >{0}</td>", item.readers_count);
                strb.AppendFormat("<td x:str>{0}</td>", item.images.small);
                strb.AppendFormat("<td x:str >{0}</td>", item.images.medium); ;
                strb.AppendFormat("<td x:str >{0}</td>", item.images.large);
                strb.AppendFormat("<td x:str>{0}</td>", item.details_url);
                strb.Append("</tr>");
            }
            strb.Append("</table>");
            strb.Append(" </body> </html>");
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.Charset = "GB2312";
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("产品导出" + DateTime.Now.ToString("yyyyMMdd") + ".xls", Encoding.UTF8).ToString());
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文   
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。   
            System.Web.HttpContext.Current.Response.Write(strb);
            System.Web.HttpContext.Current.Response.End();

        }

        public static string ArrayToString(string[] str)
        {
            if (str == null || str.Length <= 0)
            {
                return string.Empty;
            }
            return string.Join(",", str);
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
    }
}