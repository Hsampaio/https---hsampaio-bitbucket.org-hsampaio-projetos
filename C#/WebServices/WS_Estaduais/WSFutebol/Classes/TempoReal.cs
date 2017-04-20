using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WSFutebol.Classes
{
    public class TempoReal
    {
        public static string BuscarTempoReal(string sUrl, string sCampeonato)
        {

            List<Jogo> listJogos = new List<Jogo>();

            string sHtml = Utils.code(sUrl);

            string sRodadaAtual = BuscarRodadaAtual(sHtml);

            int iInicio = sHtml.IndexOf("lista-de-jogos-item");
            int iFim = sHtml.Length - iInicio;

            string sTable = sHtml.Substring(iInicio, iFim);
            iInicio = sTable.IndexOf("lista-de-jogos-item");
            iFim = sTable.IndexOf("aside");

            string sListaHTML = sTable.Substring(iInicio, iFim);

            string[] separator = new string[]
            {
                    "lista-de-jogos-item"
            };
            string[] separator2 = new string[]
            {
                    "content="
            };
            string[] separator3 = new string[]
            {
                    "placar-jogo-informacoes-local"
            };
            string[] separator4 = new string[]
            {
                    "placar-jogo-equipes-placar-mandante"
            };
            string[] separator5 = new string[]
            {
                    "placar-jogo-equipes-placar-visitante"
            };
            string[] separator6 = new string[]
            {
                    "placar-jogo-informacoes"
            };
            string[] separator7 = new string[]
            {
                    "/span"
            };


            string[] arrayTodo = sListaHTML.Split(separator, StringSplitOptions.RemoveEmptyEntries);


            int iIdJogo = 0;

            for (int i = 0; i < arrayTodo.Length; i++)
            {
                string sLinha1 = arrayTodo[i];

                iIdJogo++;

                string[] arrayInfoJogo = sLinha1.Split(separator2, StringSplitOptions.RemoveEmptyEntries);

                iFim = arrayInfoJogo[3].IndexOf(">");
                string sNomeMandante = arrayInfoJogo[3].Substring(0, iFim);

                iFim = arrayInfoJogo[4].IndexOf(">");
                string sNomeVisit = arrayInfoJogo[4].Substring(0, iFim);

                iInicio = arrayInfoJogo[2].IndexOf("href=");
                iFim = arrayInfoJogo[2].IndexOf("div class");

                try
                {
                    string text8 = arrayInfoJogo[2].Substring(iInicio, iFim - iInicio);
                }
                catch
                {
                }

                string[] arrayLocal = sLinha1.Split(separator3, StringSplitOptions.RemoveEmptyEntries);
                iFim = arrayLocal[1].IndexOf("<");
                string sLocal = arrayLocal[1].Substring(1, iFim);

                string[] arrayPlacarM = sLinha1.Split(separator4, StringSplitOptions.RemoveEmptyEntries);
                iFim = arrayPlacarM[1].IndexOf("<");
                string sPlacarMandante = arrayPlacarM[1].Substring(1, iFim);

                string[] arrayPlacarV = sLinha1.Split(separator5, StringSplitOptions.RemoveEmptyEntries);
                iFim = arrayPlacarV[1].IndexOf("<");
                string sPlacarVisit = arrayPlacarV[1].Substring(1, iFim);

                string[] arrayData = sLinha1.Split(separator6, StringSplitOptions.RemoveEmptyEntries);
                iFim = arrayData[1].IndexOf("<");
                string sData = arrayData[1].Substring(1, iFim);

                string[] arrayHora = arrayData[2].Split(separator7, StringSplitOptions.RemoveEmptyEntries);
                iFim = arrayHora[1].IndexOf("<");
                string sHora = arrayHora[1].Substring(1, iFim);

                Jogo jogo = new Jogo();
         
                if (sRodadaAtual.Equals(""))
                {
                    sRodadaAtual = "0";
                }

                jogo.Idrodada = (int)short.Parse(sRodadaAtual);
                jogo.IdJogo = iIdJogo;

                jogo.SiglaMandante = Sigla.BuscarSigla(sNomeMandante.Replace(">", "").Replace('"', ' ').Replace("<", ""), sCampeonato);
                jogo.SiglaVisitante = Sigla.BuscarSigla(sNomeVisit.Replace(">", "").Replace('"', ' ').Replace("<", ""), sCampeonato);


                jogo.PlacarMandante = sPlacarMandante.Replace(">", "").Replace('"', ' ').Replace("<", "");
                jogo.PlacarVisitante = sPlacarVisit.Replace(">", "").Replace('"', ' ').Replace("<", "");


                if (jogo.PlacarMandante.Trim().Equals(""))
                {
                    jogo.Status = "A";
                    jogo.PlacarMandante = "0";
                }
                else
                {
                    jogo.Status = "E";
                }

                if (jogo.PlacarVisitante.Trim().Equals(""))
                {
                    jogo.PlacarVisitante = "0";
                }

                jogo.Datahora = sData.Replace(">", "").Replace('"', ' ').Replace("<", "") + "-" + sHora.Replace(">", "").Replace('"', ' ').Replace("<", "");

                jogo.Local = sLocal.Replace(">", "").Replace('"', ' ').Replace("<", "");

                listJogos.Add(jogo);
            }


            return JsonConvert.SerializeObject(listJogos, Formatting.Indented);
        }




        public static string BuscarRodadaAtual(string sTexto)
        {
            int iInicio = sTexto.IndexOf("tabela-navegacao-seletor");

            string result;
            if (iInicio >= 0)
            {
                string text = sTexto.Substring(iInicio + 38, 4);
                text = string.Join("", Regex.Split(text, "[^\\d]"));
                result = text;
            }
            else
            {
                result = "0";
            }
            return result;
        }

    }
}