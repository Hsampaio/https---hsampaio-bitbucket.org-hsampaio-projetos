using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using WSFutebol.Classes;

namespace WSFutebol
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string BuscarTimes(string sChave, string sCampeonato)
        {

            Times times = new Times(sCampeonato);
            return JsonConvert.SerializeObject(times.ListTime, Formatting.Indented); ;

        }

        [WebMethod]
        public string BuscarTempoReal(string sChave, string sCampeonato)
        {

            if (sCampeonato.Equals("BR_A"))
            {
                //return TempoReal.BuscarTempoReal("http://globoesporte.globo.com/futebol/brasileirao-serie-a/", sCampeonato);
                return Utils.codeiso8859("http://inaltum.futebol.servicos.ws/json_temporeal_BR_A.json");
            }
            else

                if (sCampeonato.Equals("BR_B"))
            {
                //return TempoReal.BuscarTempoReal("http://globoesporte.globo.com/futebol/brasileirao-serie-b/", sCampeonato);
                return Utils.codeiso8859("http://inaltum.futebol.servicos.ws/json_temporeal_BR_B.json");
            }
            else
                return "[]";

        }


        [WebMethod]
        public string BuscarClassificacao(string sChave, string sCampeonato)
        {

            if (sCampeonato.Equals("BR_A"))
            {

                return Utils.codeiso8859("http://inaltum.futebol.servicos.ws/json_classificacao_BR_A.json");
            }
            else
                if (sCampeonato.Equals("BR_B"))
            {
                return Utils.codeiso8859("http://inaltum.futebol.servicos.ws/json_classificacao_BR_B.json");
            }
            else
                return "[]";

        }
        [WebMethod]
        public string BuscarTogosJogos(string sChave, string sCampeonato)
        {
            if (sCampeonato.Equals("BR_A"))
            {
                // return TodosJogos.BuscarTodosJogos_Uol("https://esporte.uol.com.br/futebol/campeonatos/brasileirao/jogos/", sCampeonato);
                // return TodosJogos.BuscarTodosJogos_TabelaBR("http://www.tabeladobrasileirao.net/serie-a/", sCampeonato);
                return Utils.codeiso8859("http://inaltum.futebol.servicos.ws/json_todosjogos_BR_A.json");
            }
            else
                if (sCampeonato.Equals("BR_B"))
            {
                //return TodosJogos.BuscarTodosJogos_Uol("https://esporte.uol.com.br/futebol/campeonatos/serie-b/jogos/", sCampeonato);
                return Utils.codeiso8859("http://inaltum.futebol.servicos.ws/json_todosjogos_BR_B.json");
                //return TodosJogos.BuscarTodosJogos_TabelaBR("http://www.tabeladobrasileirao.net/serie-b/", sCampeonato);

            }
            else
                return "[]";


        }




    }
}
