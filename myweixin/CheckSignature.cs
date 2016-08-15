using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace myweixin
{
    public class CheckSignature
    {
        /// <summary>
        /// 默认Token
        /// </summary>
        public const string Token = "makkweixin";

        /// <summary>
        /// 检查签名是否正确
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool Check(string signature, string timestamp, string nonce, string token)
        {
            return signature == GetSignaTure(timestamp, nonce, token);
        }
        /// <summary>
        /// 返回正确的签名
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetSignaTure(string timestamp, string nonce, string token = Token)
        {
            token = token ?? Token;
            string[] arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            string arrString = string.Join("", arr);
            string enText = FormsAuthentication.HashPasswordForStoringInConfigFile(arrString, "SHA1").ToLower();
            return enText;
        }
    }
}