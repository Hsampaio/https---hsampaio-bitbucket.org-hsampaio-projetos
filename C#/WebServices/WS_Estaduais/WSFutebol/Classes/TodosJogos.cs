using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WSFutebol.Classes
{
    public class TodosJogos
    {
        public static string BuscarTodosJogos_Uol(string sUrl, string sCampeonato)
        {
            List<Jogo> listjogos = new List<Jogo>();

            string sCodePage = Utils.code(sUrl);

            int num = sCodePage.IndexOf("rodadas");
            int length = sCodePage.Length - num;
            string sCodeFiltro1 = sCodePage.Substring(num, length);


            string[] separator = new string[]
            {
                "li class"
            };
            string[] arrayJogos = sCodeFiltro1.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            string[] separator2 = new string[]
            {
                "article"
            };
            string[] separator3 = new string[]
            {
                "div class"
            };
            string[] separator4 = new string[]
            {
                "gols"
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

            int num2 = 0;
            int iRodada = 0;
            int iJogo = 0;
            for (int k = 1; k < arrayJogos.Length; k++)
            {
                string sLinhaRodada = arrayJogos[k];
                int num3 = num2;
                num2 = num3 + 1;
                string[] arrayLinhaJogo = sLinhaRodada.Split(separator2, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < arrayLinhaJogo.Length; j++)
                {

                    string sJogo = arrayLinhaJogo[j];
                    if (j == 0)
                    {
                        try
                        {
                            iJogo = 1;
                            int iPosIni = sJogo.IndexOf("Rodada");
                            int iTamanho = sJogo.Length - iPosIni;
                            string sRodada = sJogo.Substring(iPosIni, iTamanho);
                            iPosIni = 0;
                            int iPosFIm = sRodada.IndexOf("<");
                            sRodada = sRodada.Substring(0, iPosFIm);

                            sRodada = String.Join("", System.Text.RegularExpressions.Regex.Split(sRodada, @"[^\d]"));

                            iRodada = Int32.Parse(sRodada);

                        }
                        catch
                        {
                            //   sRodada = "0";
                        }


                    }
                    else
                    {
                        string[] arrayInfoJogo = sJogo.Split(separator3, StringSplitOptions.RemoveEmptyEntries);
                        if (arrayInfoJogo.Length == 5)
                        {
                            string sInfoTime1 = arrayInfoJogo[2];
                            string sInfoTime2 = arrayInfoJogo[3];
                            string sInfoJogo = arrayInfoJogo[4];

                            string sGolsTime1 = "";
                            string sGolsTime2 = "";
                            string sTime1 = "";
                            string sTime2 = "";
                            string sLocal = "";
                            string sData = "";

                            try
                            {
                                sGolsTime1 = BuscarGols_UOL(sInfoTime1);
                                sGolsTime2 = BuscarGols_UOL(sInfoTime2);
                            }
                            catch
                            {
                                sGolsTime1 = "ERRO";
                                sGolsTime1 = "ERRO";
                            }

                            try
                            {
                                sTime1 = BuscarTime_UOL(sInfoTime1);
                                sTime2 = BuscarTime_UOL(sInfoTime2);
                            }
                            catch
                            {
                                sTime1 = "ERRO";
                                sTime2 = "ERRO";
                            }

                            try
                            {
                                sLocal = BuscarLocal_UOL(sInfoJogo);
                            }
                            catch
                            {
                                sLocal = "ERRO";
                            }

                            try
                            {
                                sData = BuscarData_UOL(sInfoJogo);
                            }
                            catch
                            {
                                sData = "ERRO";
                            }

                            if (!sTime1.Equals("ERRO"))
                            {

                                Jogo jogo = new Jogo();
                                jogo.Idrodada = iRodada;
                                jogo.IdJogo = iJogo;
                                jogo.SiglaMandante = Sigla.BuscarSigla(sTime1, sCampeonato);
                                jogo.SiglaVisitante = Sigla.BuscarSigla(sTime2, sCampeonato);
                                jogo.PlacarMandante = sGolsTime1;
                                jogo.PlacarVisitante = sGolsTime2;
                                jogo.Campeonato = sCampeonato;

                                jogo.Datahora = sData;
                                jogo.Local = sLocal;

              
                                if (sLocal != "ERRO")
                                    jogo.Local = sLocal;

                                listjogos.Add(jogo);
                                iJogo++;
                            }
                            else
                            {
                                string ERRO = "";
                            }

                        }
                    }

                }

            }
            return JsonConvert.SerializeObject(listjogos, Formatting.Indented);
        }

        public static string BuscarTime_UOL(string sInfo)
        {
            string[] separatorGols = new string[]
                    {
                                "abbr title"
                    };

            string[] arrayInfoGols1 = sInfo.Split(separatorGols, StringSplitOptions.RemoveEmptyEntries);

            int iPosIni = arrayInfoGols1[1].IndexOf("=") + 2;
            int iPosFim = arrayInfoGols1[1].IndexOf(">") - 1;
            string sTime = arrayInfoGols1[1].Substring(iPosIni, (iPosFim - iPosIni));
            if (sTime.Equals(""))
            {
                sTime = "Ops..";
            }

            return sTime;

        }


        public static string BuscarLocal_UOL(string sInfo)
        {
            string[] separatorGols = new string[]
                    {
                                "local"
                    };

            string[] arrayInfoGols1 = sInfo.Split(separatorGols, StringSplitOptions.RemoveEmptyEntries);

            int iPosIni = arrayInfoGols1[1].IndexOf(">") + 1;
            int iPosFim = arrayInfoGols1[1].IndexOf("<");
            string sLocal = arrayInfoGols1[1].Substring(iPosIni, (iPosFim - iPosIni));
            if (sLocal.Equals(""))
            {
                sLocal = "A definir";
            }

            return sLocal;

        }


        public static string BuscarData_UOL(string sInfo)
        {
            string sDataFinal = "";
            try
            {
                string[] separatorGols = new string[]
                        {
                                "<label>"
                        };

                string[] arrayInfoGols1 = sInfo.Split(separatorGols, StringSplitOptions.RemoveEmptyEntries);


                int iPosIni = 0;
                int iPosFim = arrayInfoGols1[1].IndexOf("<");
                string sDiaSemana = arrayInfoGols1[1].Substring(iPosIni, (iPosFim - iPosIni));


                iPosIni = arrayInfoGols1[1].IndexOf(">") + 1; ;
                iPosFim = arrayInfoGols1[1].IndexOf("</time>");
                string sDiaDataHora = arrayInfoGols1[1].Substring(iPosIni, (iPosFim - iPosIni));

                sDataFinal = sDiaSemana + " " + sDiaDataHora;

                if (sDiaSemana.Equals(""))
                {
                    sDataFinal = "A definir";
                }
            }
            catch
            {
                sDataFinal = "A definir";
            }

            return sDataFinal;

        }


        public static string BuscarGols_UOL(string sInfo)
        {
            string[] separatorGols = new string[]
                    {
                                "label class"
                    };

            string[] arrayInfoGols1 = sInfo.Split(separatorGols, StringSplitOptions.RemoveEmptyEntries);

            int iPosIni = arrayInfoGols1[1].IndexOf(">") + 1;
            int iPosFim = arrayInfoGols1[1].IndexOf("<");
            string sGols = arrayInfoGols1[1].Substring(iPosIni, (iPosFim - iPosIni));
            if (sGols.Equals(""))
            {
                sGols = "0";
            }

            return sGols;

        }

        public static string BuscarTodosJogos_TabelaBR(string sUrl, string sCampeonato)
        {
            string text = Utils.code(sUrl);
            int num = text.IndexOf("table-row");
            int num2 = text.IndexOf("table-legend");
            string text2 = text.Substring(num, num2 - num);
            List<Jogo> list = new List<Jogo>();
            string[] array = Regex.Split(text2, "table-row");
            int num3 = 0;
            int num4;
            for (int i = 1; i <= array.Length - 1; i++)
            {

                string sLinhaJogo = array[i];

                string[] array2 = Regex.Split(sLinhaJogo, "</div>");
                try
                {
                    string s = Utils.BuscarStringEntre(">", "<", array2[1]);
                    string str = Utils.BuscarStringEntre(">", "<", array2[2]);
                    string text3 = Utils.BuscarStringEntre(">", "<", array2[3]);
                    string text4 = Utils.BuscarStringEntre(">", "<", array2[4]);
                    string sTime = Utils.BuscarStringEntre(">", "<", array2[5].Remove(0, 16));
                    string text5 = Utils.BuscarStringEntre(">", "<", array2[6]);
                    string text6 = Utils.BuscarStringEntre(">", "<", array2[8]);
                    string[] array3 = Regex.Split(array2[9], ">");
                    string sTime2 = Utils.BuscarStringEntre("", "<", array3[4]);
                    string local = Utils.BuscarStringEntre(">", "<", array2[10]);
                    bool flag = text4.Equals("");
                    if (flag)
                    {
                        text4 = "A Definir";
                    }
                    bool flag2 = text6.Equals("");
                    if (flag2)
                    {
                        text6 = "0";
                    }
                    bool flag3 = text5.Equals("");
                    if (flag3)
                    {
                        text5 = "0";
                    }
                    list.Add(new Jogo
                    {
                        Datahora = str + " - " + text4,
                        IdJogo = num3,
                        Idrodada = (int)short.Parse(s),
                        Local = local,
                        PlacarMandante = text5,
                        PlacarVisitante = text6,
                        SiglaMandante = Sigla.BuscarSigla(sTime, sCampeonato),
                        SiglaVisitante = Sigla.BuscarSigla(sTime2, sCampeonato),
                        Status = ""
                    });
                }
                catch (Exception var_24_276)
                {

                }
                num4 = num3;
                num3 = num4 + 1;
                string text7 = text2.Substring(num);
                num4 = i;
            }
            return JsonConvert.SerializeObject(list, Formatting.Indented);
        }
    }
}