using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using WSFutebol.Classes;

namespace WSFutebol
{
    /// <summary>
    /// Summary description for GerarJSON
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GerarJSON : System.Web.Services.WebService
    {

        [WebMethod]
        public string GerarJSON_TodoJogosSerieA_UOL()
        {
            GerarJSON_TodoJogos("", "BR_A", "UOL");
            return "ok";
        }

        [WebMethod]
        public string GerarJSON_TodoJogosSerieB_UOL()
        {
            GerarJSON_TodoJogos("", "BR_B", "UOL");
            return "ok";
        }

        [WebMethod]
        public string GerarJSON_TempoReal_Auto()
        {
            GerarJSON_TempoReal("", "BR_A");
            GerarJSON_TempoReal("", "BR_B");
            return "ok";
        }




        [WebMethod]
        public string GerarJSON_TodoJogos(string sChave, string sCampeonato, string sURL)
        {
            string json = "[]";

            if (sCampeonato.Equals("BR_A"))
            {
                if (sURL.ToUpper().Equals("UOL"))
                    json = TodosJogos.BuscarTodosJogos_Uol("https://esporte.uol.com.br/futebol/campeonatos/brasileirao/jogos/", sCampeonato);
                else
                    json = TodosJogos.BuscarTodosJogos_TabelaBR("http://www.tabeladobrasileirao.net/serie-a/", sCampeonato);

            }
            else
                if (sCampeonato.Equals("BR_B"))
            {
                if (sURL.ToUpper().Equals("UOL"))
                    json = TodosJogos.BuscarTodosJogos_Uol("https://esporte.uol.com.br/futebol/campeonatos/serie-b/jogos/", sCampeonato);
                else
                    json = TodosJogos.BuscarTodosJogos_TabelaBR("http://www.tabeladobrasileirao.net/serie-b/", sCampeonato);

            }

            if (!json.Equals("[]"))
            {

                string sFile = "json_todosjogos_" + sCampeonato + ".json";

                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.inaltum.futebol.servicos.ws/WEB/" + sFile);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential("inaltum", "androidapk1");



                // Copy the contents of the file to the request stream.
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] fileContents = Encoding.GetEncoding("iso8859-1").GetBytes(json);
                //and now plug that into your example
                try
                {
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(fileContents, 0, fileContents.Length);
                    requestStream.Close();
                }
                catch
                {

                }

                request.ContentLength = fileContents.Length;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                return "( http://inaltum.futebol.servicos.ws/" + sFile + " )Complete status :" + response.StatusDescription;

            }
            else
            {
                return "erro ao gerar dados";
            }
        }


        [WebMethod]
        public string GerarJSON_TempoReal(string sChave, string sCampeonato)
        {
            string json = "[]";

            if (sCampeonato.Equals("BR_A"))
                json = TempoReal.BuscarTempoReal("http://globoesporte.globo.com/futebol/brasileirao-serie-a/", sCampeonato);
            else
                if (sCampeonato.Equals("BR_B"))
                json = TempoReal.BuscarTempoReal("http://globoesporte.globo.com/futebol/brasileirao-serie-b/", sCampeonato);
            else
                json = "[]";



            if (!json.Equals("[]"))
            {

                string sFile = "json_temporeal_" + sCampeonato + ".json";

                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.inaltum.futebol.servicos.ws/WEB/" + sFile);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential("inaltum", "androidapk1");



                // Copy the contents of the file to the request stream.
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] fileContents = Encoding.GetEncoding("iso8859-1").GetBytes(json);
                //and now plug that into your example
                try
                {
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(fileContents, 0, fileContents.Length);
                    requestStream.Close();
                }
                catch
                {

                }

                request.ContentLength = fileContents.Length;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                return "( http://inaltum.futebol.servicos.ws/" + sFile + " )Complete status :" + response.StatusDescription;

            }
            else
            {
                return "erro ao gerar dados";
            }


        }

        [WebMethod]
        public string GerarJSON_Classificacao(string sChave, string sCampeonato)
        {
            string json = "[]";

            if (sCampeonato.Equals("BR_A"))
                json = Classificacao.BuscarClassificacao("http://esporte.uol.com.br/futebol/campeonatos/brasileirao/jogos/");
            else
                if (sCampeonato.Equals("BR_B"))
                json = Classificacao.BuscarClassificacao("http://esporte.uol.com.br/futebol/campeonatos/serie-b/jogos/");
            else
                json = "[]";



            if (!json.Equals("[]"))
            {

                string sFile = "json_classificacao_" + sCampeonato + ".json";

                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.inaltum.futebol.servicos.ws/WEB/" + sFile);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential("inaltum", "androidapk1");



                // Copy the contents of the file to the request stream.
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] fileContents = Encoding.GetEncoding("iso8859-1").GetBytes(json);
                //and now plug that into your example
                try
                {
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(fileContents, 0, fileContents.Length);
                    requestStream.Close();
                }
                catch
                {

                }

                request.ContentLength = fileContents.Length;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                return "( http://inaltum.futebol.servicos.ws/" + sFile + " )Complete status :" + response.StatusDescription;

            }
            else
            {
                return "erro ao gerar dados";
            }


        }





    }
}
