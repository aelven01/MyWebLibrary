using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MyWebLibrary
{
    public class AdminHelper
    {
        private static readonly string LoginPage = "/login.aspx";
        private static readonly string indexPage = "index.aspx";


        /// <summary>
        ///适用于webForm页面
        /// </summary>
        /// <param name="loginPath"></param>
        /// <param name="RedirectPath"></param>
        public static void CheckLogin(string loginPath,string RedirectPath)
        {
            HttpContext context =HttpContext.Current;
            if (context.Session["userName"] == null)
            {
                if (!context.Request.Url.ToString().ToLower().Contains(loginPath))
                {
                    context.Response.Redirect(loginPath + "?redirect=" + context.Request.Url);
                    return;
                }
            }
            else
            {
                //已经登录时,在访问登录页时跳转
                if (context.Request.Url.ToString().ToLower().Contains(loginPath))
                {
                    context.Response.Redirect(RedirectPath);
                }
            }
        }

        /// <summary>
        /// 适用于ashx页面   返回ajax状态
        /// </summary>
        public static void CheckLogin()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["userName"] == null)
            {
                context.Response.Write(JsonHelper.GetResultStr(-1,"",""));
                context.Response.End();
                return;
            }
        }

        /// <summary>
        /// 用于验证输入密码次数,超过一定次数输出验证码
        /// </summary>
        /// <returns></returns>
        public static bool CheckRequestCount(int count)
        {
            //初次验证
            HttpContext context = HttpContext.Current;
            if (context.Session["userIp"] == null)
            {
                context.Session["userIp"] = context.Request.UserHostAddress;
                context.Session["requestCount"] = 1;
                return true;
            }
            //再次验证
            else if (context.Session["userIp"] != null)
            {
                //验证超过3次输出验证码
                if (Convert.ToInt16(context.Session["requestCount"]) >= count)
                {
                    return false;
                }

                //记录验证次数
                if (context.Session["userIp"].ToString() == context.Request.UserHostAddress)
                {
                    int requestCount = Convert.ToInt16(context.Session["requestCount"]);
                    requestCount++;
                    context.Session["requestCount"] = requestCount;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 登录成功后清除验证码状态
        /// </summary>
        public static void ClearValidateInfo()
        {
            HttpContext context = HttpContext.Current;
            context.Session["validateCode"] = null;
            context.Session["requestCount"] = null;
            context.Session["userIp"] = null;
        }
    }
}
