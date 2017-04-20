using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace WSFutebol.Classes
{
    class Util_GCM
    {
        //Modelo de postData
        //{   "registration_ids" : ["APA91bHJDhq5GgUwYW0UUho9PKqWbaZG3I0aK-44cl9R-ql9wTjtg4vm1toZfUqA1FAzgO6sARZruWXuosFyEMq1FNieNCU1yYi4m7DpwzEhhw8504HUAqSlhuaqp3qiJpFWmxWggBFXwSbNmVpNMaoReTq-XkV83A"],   "data" : {     "Team" : "Portugal",     "Score" : "3",     "Player" : "Varela",   }, }
        /// <summary>
        /// Send a Google Cloud Message. Uses the GCM service and your provided api key.
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="postDataContentType"></param>
        /// <returns>The response string from the google servers</returns>
        //LocaWeb//string API_CHAVESERVIDOR = "AIzaSyCDmWyYgcbauEtS_lBDcyCiAabELHgE9qU";
        //Local
        static string API_CHAVESERVIDOR = "AIzaSyCDAvB9830cOPHSl-qWZ8_K0rx0GA--Ijg";//Server
        //string API_CHAVESERVIDOR = "AIzaSyCZVjMRyBYXte_jfNMHPRTkTD8pVYDk9yk";
        public static string SendGCMNotification(string postData, string postDataContentType = "application/json")
        {
            //   ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

            //
            //  MESSAGE CONTENT
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //
            //  CREATE REQUEST
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
            Request.Method = "POST";
            Request.KeepAlive = false;
            Request.ContentType = postDataContentType;
            Request.Headers.Add(string.Format("Authorization: key={0}", API_CHAVESERVIDOR));
            Request.ContentLength = byteArray.Length;

            Stream dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //
            //  SEND MESSAGE
            try
            {
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                {
                    var text = "Unauthorized - need new token";
                }
                else if (!ResponseCode.Equals(HttpStatusCode.OK))
                {
                    var text = "Response from web service isn't OK";
                }

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string responseLine = Reader.ReadToEnd();
                Reader.Close();

                return responseLine;
            }
            catch (Exception e)
            {
                string tes = e.ToString();
                tes = "";
            }
            return "error";
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}