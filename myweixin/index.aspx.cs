using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myweixin
{
    public partial class index : System.Web.UI.Page
    {
        private readonly String Token = "weixinmakk";

        protected void Page_Load(object sender, EventArgs e)
        {
            Auth();
        }
        private void Auth()
        {
            string signature = QueryString("signature");
            string timestamp = QueryString("timestamp");
            string nonce = QueryString("nonce");
            string echostr = QueryString("echostr");
            if (Request.HttpMethod == "GET")
            {
                if (CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    WriteContent(echostr);
                }
                else
                {
                    WriteContent("failed:" + signature + "," + CheckSignature.GetSignaTure(timestamp, nonce, Token) + "." + "如果你在浏览器里看到这句话，说明此地址可以被作为微信公共账号后台的URL，请注意保持Token一致。");
                }
            }
        }
        private void WriteContent(string str)
        {
            Response.Output.Write(str);
        }
        private string QueryString(string name)
        {
            if (string.IsNullOrWhiteSpace(Request[name]))
            {
                return HttpContext.Current.Request.QueryString["signature"];
            }
            else
            {
                return Request[name];
            }
        }
    }
}