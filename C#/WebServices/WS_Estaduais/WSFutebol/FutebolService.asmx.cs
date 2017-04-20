using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using WSFutebol.Classes;

namespace WSFutebol
{
    /// <summary>
    /// Summary description for FutebolService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FutebolService : System.Web.Services.WebService
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
                return TempoReal.BuscarTempoReal("http://globoesporte.globo.com/futebol/brasileirao-serie-a/", sCampeonato);
            else
                if (sCampeonato.Equals("BR_B"))
                return TempoReal.BuscarTempoReal("http://globoesporte.globo.com/futebol/brasileirao-serie-b/", sCampeonato);
            else
                return "[]";

        }
        [WebMethod]
        public string BuscarTogosJogos(string sChave, string sCampeonato)
        {
            if (sCampeonato.Equals("BR_A"))
            {
                return TodosJogos.BuscarTodosJogos_Uol("https://esporte.uol.com.br/futebol/campeonatos/brasileirao/jogos/", sCampeonato);
               // return TodosJogos.BuscarTodosJogos_TabelaBR("http://www.tabeladobrasileirao.net/serie-a/", sCampeonato);
                
            }
            else
                if (sCampeonato.Equals("BR_B"))
            {
                return TodosJogos.BuscarTodosJogos_Uol("https://esporte.uol.com.br/futebol/campeonatos/serie-b/jogos/", sCampeonato);
                //return TodosJogos.BuscarTodosJogos_TabelaBR("http://www.tabeladobrasileirao.net/serie-b/", sCampeonato);

            }
            else
                return "[]";
            
                
        }



    }
}
