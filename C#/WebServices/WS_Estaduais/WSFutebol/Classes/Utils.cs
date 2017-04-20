using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WSFutebol.Classes
{
    public class Utils
    {

        public static string code(string Url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "GET";
            WebResponse response = httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = streamReader.ReadToEnd();
            streamReader.Close();
            response.Close();
            return result;
        }

        public static string codeiso8859(string Url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "GET";
            WebResponse response = httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("iso8859-1"));
            string result = streamReader.ReadToEnd();
            streamReader.Close();
            response.Close();
            return result;
        }


        public static string BuscarStringEntre(string sTAG1, string sTAG2, string sTEXTO)
        {
            string result = "";
            int num = sTEXTO.IndexOf(sTAG1);
            bool flag = sTAG1.Equals("");
            if (flag)
            {
                num = 0;
            }
            int num2 = sTEXTO.IndexOf(sTAG2);
            bool flag2 = num > -1 && num2 > 0 && num < num2;
            if (flag2)
            {
                result = sTEXTO.Substring(num + sTAG1.Length, num2 - (num + sTAG1.Length));
            }
            return result;
        }

    }
}