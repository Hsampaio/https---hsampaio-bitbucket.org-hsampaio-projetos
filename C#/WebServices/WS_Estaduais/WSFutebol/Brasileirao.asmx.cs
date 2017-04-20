using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services;
using WSFutebol.Classes;

namespace WSFutebol
{
    [ToolboxItem(false), WebService(Namespace = "http://tempuri.org/"), WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Brasileirao : WebService
    {


        [WebMethod]
        public string WS_TodosJogosSerieA(string sChave)
        {
            bool flag = sChave == "fvhbr2014;";
            string result;
            if (flag)
            {
                //              string text = Brasileirao.TodosJogos("a", "2016");
                return JsonConvert.SerializeObject(Jogo.BuscarTodosJogos("BR"), Formatting.Indented);

                //                result = text;
            }
            else
            {
                result = "Tentativa de invasão detectada! Você está sendo rastreado...";
            }
            return result;
        }




        [WebMethod]
        public string AtualizarResultadosDB(string sCampeonato)
        {

            if (sCampeonato.Equals("BR"))
            {
                BuscarResultadoUol("https://esporte.uol.com.br/futebol/campeonatos/brasileirao/jogos/", sCampeonato);
                BuscarResultadoGloboEsporte("http://globoesporte.globo.com/futebol/brasileirao-serie-a/", sCampeonato);

            }

            return "OK";
        }


        [WebMethod]
        public string WS_ExibeImagem(string sChave)
        {
            bool flag = sChave == "fvhbr2014;";
            string result;
            if (flag)
            {
                result = Brasileirao.code("http://inaltum.futebol.servicos.ws/EXIBEIMG_E.txt");
            }
            else
            {
                result = "Tentativa de invasão detectada! Você está sendo rastreado...";
            }
            return result;
        }

        [WebMethod]
        public string WS_RegistrarGCM(string sChave, string sJSONPessoa)
        {
            bool flag = sChave == "fvhbr2014;";
            string result;
            if (flag)
            {
                result = JsonConvert.DeserializeObject<Pessoa>(sJSONPessoa).Inserir().ToString();
            }
            else
            {
                result = "Tentativa de invasão detectada! Você está sendo rastreado...";
            }
            return result;
        }

        [WebMethod]
        public string WS_TempoReal(string sChave)
        {
            bool flag = sChave == "fvhbr2014;";
            string result;
            if (flag)
            {
                string text = this.TempoReal("http://globoesporte.globo.com/futebol/brasileirao-serie-a/", "N");
                result = text;
            }
            else
            {
                result = "Tentativa de invasão detectada! Você está sendo rastreado...";
            }
            return result;
        }

        [WebMethod]
        public string WS_GCM(string sChave, string sTipo, string sDescricao, string sInfo)
        {
            bool flag = sChave == "fvhbr2014;";
            string result;
            if (flag)
            {
                result = new Notificacao
                {
                    Tipo = sTipo,
                    Descricao = sDescricao,
                    Info = sInfo
                }.EnviarNotificacaoTodos();
            }
            else
            {
                result = "Tentativa de invasão detectada! Você está sendo rastreado...";
            }
            return result;
        }

        [WebMethod]
        public string WS_BuscarArtilheiros(string sChave)
        {
            bool flag = sChave == "fvhbr2014;";
            string result;
            if (flag)
            {
                result = "[]";// Brasileirao.GetArtilheiros();
            }
            else
            {
                result = "Tentativa de invasão detectada! Você está sendo rastreado...";
            }
            return result;
        }

        [WebMethod]
        public string WS_BuscarInfoJogo(string sChave)
        {
            bool flag = sChave == "fvhbr2014;";
            string result;
            if (flag)
            {
                result = this.InfoJogo("http://globoesporte.globo.com/sp/futebol/brasileirao-serie-a/jogo/25-06-2016/corinthians-santa-cruz/", "N");
            }
            else
            {
                result = "Tentativa de invasão detectada! Você está sendo rastreado...";
            }
            return result;
        }

        public static string GetArtilheiros()
        {
            //string text = Brasileirao.code("http://www.centralbrasileirao.com.br/brasileirao-serie-a/artilheiros/");
            //int num = text.IndexOf("table class");
            //int num2 = text.IndexOf("</table>");
            //string text2 = text.Substring(num, num2 - num);
            List<Artilhero> list = new List<Artilhero>();
            //string[] array = Regex.Split(text2, "img src");
            //int num3 = 0;
            //int num4;
            //for (int i = 1; i <= array.Length - 1; i = num4 + 1)
            // {
            //     string[] array2 = Regex.Split(array[i], "/");
            //     try
            //    {
            //        num4 = num3;
            //        num3 = num4 + 1;
            //        num = 0;
            //       num2 = array2[3].IndexOf(".");
            //       string sTime = array2[3].Substring(num, num2 - num);
            //       num = array2[5].IndexOf("<td>") + 5;
            //      num2 = array2[5].Length - 2;
            //      string nome = array2[5].Substring(num, num2 - num);
            //        num = array2[6].IndexOf(";\">") + 3;
            //        num2 = array2[6].Length - 2;
            //        string input = array2[6].Substring(num, num2 - num);
            //        list.Add(new Artilhero
            //        {
            //            Codigo = num3,
            //           Nome = nome,
            //          QtdGols = int.Parse(string.Join("", Regex.Split(input, "[^\\d]"))),
            //            SiglaTime = Brasileirao.BuscarSigla(sTime)
            //        });
            //    }
            //    catch (Exception var_15_13D)
            //   {
            //   }
            //  string text3 = text2.Substring(num);
            //  num4 = i;
            //}
            return JsonConvert.SerializeObject(list, Formatting.Indented);
        }

        public string BuscarRodadaAtual(string sTexto)
        {
            int num = sTexto.IndexOf("tabela-navegacao-seletor");
            bool flag = num >= 0;
            string result;
            if (flag)
            {
                string text = sTexto.Substring(num + 38, 4);
                text = string.Join("", Regex.Split(text, "[^\\d]"));
                result = text;
            }
            else
            {
                result = "0";
            }
            return result;
        }

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

        public static string BuscarSigla(string sTime)
        {
            bool flag = sTime.Trim() == "Atlético-MG" || sTime.Trim() == "atletico-mg";
            string result;
            if (flag)
            {
                result = "CAM";
            }
            else
            {
                bool flag2 = sTime.Trim() == "Atlético-PR" || sTime.Trim() == "atletico-pr";
                if (flag2)
                {
                    result = "CAP";
                }
                else
                {
                    bool flag3 = sTime.Trim() == "Bahia" || sTime.Trim() == "";
                    if (flag3)
                    {
                        result = "BAH";
                    }
                    else
                    {
                        bool flag4 = sTime.Trim() == "Botafogo" || sTime.Trim() == "botafogo";
                        if (flag4)
                        {
                            result = "BOT";
                        }
                        else
                        {
                            bool flag5 = sTime.Trim() == "Chapecoense" || sTime.Trim() == "chapecoense";
                            if (flag5)
                            {
                                result = "CHA";
                            }
                            else
                            {
                                bool flag6 = sTime.Trim() == "Corinthians" || sTime.Trim() == "corinthians";
                                if (flag6)
                                {
                                    result = "COR";
                                }
                                else
                                {
                                    bool flag7 = sTime.Trim() == "Coritiba" || sTime.Trim() == "coritiba";
                                    if (flag7)
                                    {
                                        result = "CFC";
                                    }
                                    else
                                    {
                                        bool flag8 = sTime.Trim() == "Criciúma" || sTime.Trim() == "Criciúma";
                                        if (flag8)
                                        {
                                            result = "CRI";
                                        }
                                        else
                                        {
                                            bool flag9 = sTime.Trim() == "Cruzeiro" || sTime.Trim() == "cruzeiro";
                                            if (flag9)
                                            {
                                                result = "CRU";
                                            }
                                            else
                                            {
                                                bool flag10 = sTime.Trim() == "Figueirense" || sTime.Trim() == "figueirense";
                                                if (flag10)
                                                {
                                                    result = "FIG";
                                                }
                                                else
                                                {
                                                    bool flag11 = sTime.Trim() == "Flamengo" || sTime.Trim() == "flamengo";
                                                    if (flag11)
                                                    {
                                                        result = "FLA";
                                                    }
                                                    else
                                                    {
                                                        bool flag12 = sTime.Trim() == "Fluminense" || sTime.Trim() == "fluminense";
                                                        if (flag12)
                                                        {
                                                            result = "FLU";
                                                        }
                                                        else
                                                        {
                                                            bool flag13 = sTime.Trim() == "Goiás" || sTime.Trim() == "";
                                                            if (flag13)
                                                            {
                                                                result = "GOI";
                                                            }
                                                            else
                                                            {
                                                                bool flag14 = sTime.Trim() == "Grêmio" || sTime.Trim() == "gremio";
                                                                if (flag14)
                                                                {
                                                                    result = "GRE";
                                                                }
                                                                else
                                                                {
                                                                    bool flag15 = sTime.Trim() == "Internacional" || sTime.Trim() == "internacional";
                                                                    if (flag15)
                                                                    {
                                                                        result = "INT";
                                                                    }
                                                                    else
                                                                    {
                                                                        bool flag16 = sTime.Trim() == "Palmeiras" || sTime.Trim() == "palmeiras";
                                                                        if (flag16)
                                                                        {
                                                                            result = "PAL";
                                                                        }
                                                                        else
                                                                        {
                                                                            bool flag17 = sTime.Trim() == "Santos" || sTime.Trim() == "santos";
                                                                            if (flag17)
                                                                            {
                                                                                result = "SAN";
                                                                            }
                                                                            else
                                                                            {
                                                                                bool flag18 = sTime.Trim() == "São Paulo" || sTime.Trim() == "saopaulo";
                                                                                if (flag18)
                                                                                {
                                                                                    result = "SAO";
                                                                                }
                                                                                else
                                                                                {
                                                                                    bool flag19 = sTime.Trim() == "Sport" || sTime.Trim() == "sport";
                                                                                    if (flag19)
                                                                                    {
                                                                                        result = "SPO";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        bool flag20 = sTime.Trim() == "Vitória" || sTime.Trim() == "vitoria";
                                                                                        if (flag20)
                                                                                        {
                                                                                            result = "VIT";
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            bool flag21 = sTime.Trim() == "Santa Cruz" || sTime.Trim() == "santa-cruz";
                                                                                            if (flag21)
                                                                                            {
                                                                                                result = "STA";
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                bool flag22 = sTime.Trim() == "Ponte Preta" || sTime.Trim() == "ponte-preta";
                                                                                                if (flag22)
                                                                                                {
                                                                                                    result = "PON";
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    bool flag23 = sTime.Trim() == "América-MG" || sTime.Trim() == "america-mg";
                                                                                                    if (flag23)
                                                                                                    {
                                                                                                        result = "AMG";
                                                                                                    }


                                                                                                    else
                                                                                                    {
                                                                                                        bool flag24 = sTime.Trim() == "Atlético-GO" || sTime.Trim() == "atlético-go";
                                                                                                        if (flag24)
                                                                                                        {
                                                                                                            result = "ACG";
                                                                                                        }

                                                                                                        else
                                                                                                        {
                                                                                                            bool flag25 = sTime.Trim() == "Avaí" || sTime.Trim() == "avaí";
                                                                                                            if (flag25)
                                                                                                            {
                                                                                                                result = "AVA";
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                bool flag26 = sTime.Trim() == "Vasco" || sTime.Trim() == "vasco";
                                                                                                                if (flag26)
                                                                                                                {
                                                                                                                    result = "VAS";
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    bool flag27 = sTime.Trim() == "Vasco" || sTime.Trim() == "vasco";
                                                                                                                    if (flag27)
                                                                                                                    {
                                                                                                                        result = "VAS";
                                                                                                                    }


                                                                                                                    else
                                                                                                                    {
                                                                                                                        result = sTime;
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static string TodosJogos(string sSerie, string sAno)
        {
            string text = Brasileirao.code(string.Concat(new string[]
            {
                "http://www.tabeladobrasileirao.net/",
                sAno,
                "/serie-",
                sSerie,
                "/"
            }));
            int num = text.IndexOf("table id");
            int num2 = text.IndexOf("</tbody></table>");
            string text2 = text.Substring(num, num2 - num);
            List<Jogo> list = new List<Jogo>();
            string[] array = Regex.Split(text2, "<tr class");
            int num3 = 0;
            int num4;
            for (int i = 2; i <= array.Length - 1; i = num4 + 1)
            {
                string[] array2 = Regex.Split(array[i], "<td");
                try
                {
                    string s = Brasileirao.BuscarStringEntre(">", "<", array2[1]);
                    string str = Brasileirao.BuscarStringEntre(">", "<", array2[2]);
                    string text3 = Brasileirao.BuscarStringEntre(">", "<", array2[3]);
                    string text4 = Brasileirao.BuscarStringEntre(">", "<", array2[4]);
                    string sTime = Brasileirao.BuscarStringEntre(">", "<", array2[5].Remove(0, 16));
                    string text5 = Brasileirao.BuscarStringEntre(">", "<", array2[6]);
                    string text6 = Brasileirao.BuscarStringEntre(">", "<", array2[8]);
                    string[] array3 = Regex.Split(array2[9], ">");
                    string sTime2 = Brasileirao.BuscarStringEntre("", "<", array3[4]);
                    string local = Brasileirao.BuscarStringEntre(">", "<", array2[10]);
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
                        SiglaMandante = Brasileirao.BuscarSigla(sTime),
                        SiglaVisitante = Brasileirao.BuscarSigla(sTime2),
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

        public string TempoReal(string sUrl, string sContingencia)
        {
            bool flag = sContingencia == "S";
            string result;
            if (flag)
            {
                result = Brasileirao.code(sUrl);
            }
            else
            {
                List<Jogo> list = new List<Jogo>();
                string text = Brasileirao.code(sUrl);
                string text2 = this.BuscarRodadaAtual(text);
                int num = text.IndexOf("lista-de-jogos-item");
                int num2 = text.Length - num;
                string text3 = text.Substring(num, num2);
                num = text3.IndexOf("lista-de-jogos-item");
                num2 = text3.IndexOf("aside");
                string text4 = text3.Substring(num, num2);
                string[] separator = new string[]
                {
                    "lista-de-jogos-item"
                };
                string[] array = text4.Split(separator, StringSplitOptions.RemoveEmptyEntries);
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
                int num3 = 0;
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string text5 = array2[i];
                    int num4 = num3;
                    num3 = num4 + 1;
                    string[] array3 = text5.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                    num2 = array3[3].IndexOf(">");
                    string text6 = array3[3].Substring(0, num2);
                    num2 = array3[4].IndexOf(">");
                    string text7 = array3[4].Substring(0, num2);
                    num = array3[2].IndexOf("href=");
                    num2 = array3[2].IndexOf("div class");
                    try
                    {
                        string text8 = array3[2].Substring(num, num2 - num);
                    }
                    catch
                    {
                    }
                    string[] array4 = text5.Split(separator3, StringSplitOptions.RemoveEmptyEntries);
                    num2 = array4[1].IndexOf("<");
                    string text9 = array4[1].Substring(1, num2);
                    string[] array5 = text5.Split(separator4, StringSplitOptions.RemoveEmptyEntries);
                    num2 = array5[1].IndexOf("<");
                    string text10 = array5[1].Substring(1, num2);
                    string[] array6 = text5.Split(separator5, StringSplitOptions.RemoveEmptyEntries);
                    num2 = array6[1].IndexOf("<");
                    string text11 = array6[1].Substring(1, num2);
                    string[] array7 = text5.Split(separator6, StringSplitOptions.RemoveEmptyEntries);
                    num2 = array7[1].IndexOf("<");
                    string text12 = array7[1].Substring(1, num2);
                    string[] array8 = array7[2].Split(separator7, StringSplitOptions.RemoveEmptyEntries);
                    num2 = array8[1].IndexOf("<");
                    string text13 = array8[1].Substring(1, num2);
                    Jogo jogo = new Jogo();
                    bool flag2 = text2.Equals("");
                    if (flag2)
                    {
                        text2 = "0";
                    }
                    jogo.Idrodada = (int)short.Parse(text2);
                    jogo.IdJogo = num3;

                      jogo.SiglaMandante = Brasileirao.BuscarSigla(text6.Replace(">", "").Replace('"', ' ').Replace("<", ""));
                      jogo.SiglaVisitante = Brasileirao.BuscarSigla(text7.Replace(">", "").Replace('"', ' ').Replace("<", ""));




                    jogo.PlacarMandante = text10.Replace(">", "").Replace('"', ' ').Replace("<", "");
                    jogo.PlacarVisitante = text11.Replace(">", "").Replace('"', ' ').Replace("<", "");
                    bool flag3 = jogo.PlacarMandante.Trim().Equals("");
                    if (flag3)
                    {
                        jogo.Status = "A";
                        jogo.PlacarMandante = "0";
                    }
                    else
                    {
                        jogo.Status = "E";
                    }
                    bool flag4 = jogo.PlacarVisitante.Trim().Equals("");
                    if (flag4)
                    {
                        jogo.PlacarVisitante = "0";
                    }
                    jogo.Datahora = text12.Replace(">", "").Replace('"', ' ').Replace("<", "") + "-" + text13.Replace(">", "").Replace('"', ' ').Replace("<", "");
                    jogo.Local = text9.Replace(">", "").Replace('"', ' ').Replace("<", "");
                    list.Add(jogo);
                }
                string text14 = JsonConvert.SerializeObject(list, Formatting.Indented);
                result = text14;
            }
            return result;
        }

        public string InfoJogo(string sUrl, string sContingencia)
        {
            bool flag = sContingencia == "S";
            string result;
            if (flag)
            {
                result = Brasileirao.code(sUrl);
            }
            else
            {
                List<Jogo> list = new List<Jogo>();
                string text = Brasileirao.code(sUrl);

                int ini = text.IndexOf("placar-conteudo");
                int fim = text.IndexOf("posicao-visitante");

                string text3 = text.Substring(ini, fim - ini);

                string[] separator = new string[]
                {
                    "time-mandante-infos-body"
                };
                string[] separator2 = new string[]
                {
                    "time-visitante-infos-body"
                 };
                string[] vet1 = text3.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                string[] vet2 = text3.Split(separator2, StringSplitOptions.RemoveEmptyEntries);


                string text13 = JsonConvert.SerializeObject(list, Formatting.Indented);
                result = text13;
            }
            return result;
        }

        public void TemGol(string sUrl)
        {
            string text = Brasileirao.code(sUrl);
            int num = text.IndexOf("lista-de-jogos-item");
            int length = text.Length - num;
            string text2 = text.Substring(num, length);
            num = text2.IndexOf("lista-de-jogos-item");
            length = text2.IndexOf("aside");
            string text3 = text2.Substring(num, length);
            string[] separator = new string[]
            {
                "lista-de-jogos-item"
            };
            string[] array = text3.Split(separator, StringSplitOptions.RemoveEmptyEntries);
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
            int num2 = 0;
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text4 = array2[i];
                int num3 = num2;
                num2 = num3 + 1;
                string[] array3 = text4.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                length = array3[3].IndexOf(">");
                string text5 = array3[3].Substring(0, length);
                length = array3[4].IndexOf(">");
                string text6 = array3[4].Substring(0, length);
                string[] array4 = text4.Split(separator3, StringSplitOptions.RemoveEmptyEntries);
                length = array4[1].IndexOf("<");
                string text7 = array4[1].Substring(1, length);
                string[] array5 = text4.Split(separator4, StringSplitOptions.RemoveEmptyEntries);
                length = array5[1].IndexOf("<");
                string text8 = array5[1].Substring(1, length);
                string[] array6 = text4.Split(separator5, StringSplitOptions.RemoveEmptyEntries);
                length = array6[1].IndexOf("<");
                string text9 = array6[1].Substring(1, length);
                string[] array7 = text4.Split(separator6, StringSplitOptions.RemoveEmptyEntries);
                length = array7[1].IndexOf("<");
                string text10 = array7[1].Substring(1, length);
                string[] array8 = array7[2].Split(separator7, StringSplitOptions.RemoveEmptyEntries);
                length = array8[1].IndexOf("<");
                string text11 = array8[1].Substring(1, length);
                Jogo jogo = new Jogo();
                jogo.Idrodada = 0;
                jogo.IdJogo = num2;
                jogo.SiglaMandante = Brasileirao.BuscarSigla(text5.Replace(">", "").Replace('"', ' ').Replace("<", ""));
                jogo.SiglaVisitante = Brasileirao.BuscarSigla(text6.Replace(">", "").Replace('"', ' ').Replace("<", ""));
                jogo.PlacarMandante = text8.Replace(">", "").Replace('"', ' ').Replace("<", "");
                jogo.PlacarVisitante = text9.Replace(">", "").Replace('"', ' ').Replace("<", "");
                bool flag = jogo.PlacarMandante.Trim().Equals("");
                if (flag)
                {
                    jogo.Status = "A";
                    jogo.PlacarMandante = "0";
                }
                else
                {
                    jogo.Status = "E";
                }
                bool flag2 = jogo.PlacarVisitante.Trim().Equals("");
                if (flag2)
                {
                    jogo.PlacarVisitante = "0";
                }
                jogo.Datahora = text10.Replace(">", "").Replace('"', ' ').Replace("<", "") + "-" + text11.Replace(">", "").Replace('"', ' ').Replace("<", "");
                jogo.Local = text7.Replace(">", "").Replace('"', ' ').Replace("<", "");
                Jogo jogo2 = new Jogo();
                jogo2.Idrodada = jogo.Idrodada;
                jogo2.IdJogo = jogo.IdJogo;
                jogo2.SiglaMandante = jogo.SiglaMandante;
                jogo2.SiglaVisitante = jogo.SiglaVisitante;
                jogo2.PlacarVisitante = jogo.PlacarVisitante;
                jogo2.PlacarMandante = jogo.PlacarMandante;
                jogo2.Local = jogo.Local;
                jogo2.Datahora = jogo.Datahora;
                bool flag3 = jogo2.BuscarJogo();
                if (flag3)
                {
                    bool flag4 = !jogo2.PlacarMandante.Equals(jogo.PlacarMandante.ToString());
                    if (flag4)
                    {
                        string mensagem = new Notificacao
                        {
                            Tipo = "GOL",
                            Descricao = "tem gol",
                            Info = JsonConvert.SerializeObject(jogo, Formatting.Indented)
                        }.EnviarNotificacaoTodos();
                        Notificacao.EnviarEmail1("helton.jhon@hotmail.com", "Teve Gol BR2016", mensagem);
                        jogo.AtualizarJogo();
                    }
                    else
                    {
                        bool flag5 = !jogo2.PlacarVisitante.Equals(jogo.PlacarVisitante.ToString());
                        if (flag5)
                        {
                            string mensagem = new Notificacao
                            {
                                Tipo = "GOL",
                                Descricao = "tem gol",
                                Info = JsonConvert.SerializeObject(jogo, Formatting.Indented)
                            }.EnviarNotificacaoTodos();
                            Notificacao.EnviarEmail1("helton.jhon@hotmail.com", "Teve Gol BR2016", mensagem);
                            jogo.AtualizarJogo();
                        }
                    }
                }
                else
                {
                    jogo.InserirJogo();
                }
            }
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


        public void BuscarResultadoUol(string sUrl, string sCampeonato)
        {

            string sCodePage = code(sUrl);

            int num = sCodePage.IndexOf("rodadas");
            int length = sCodePage.Length - num;
            string sCodeFiltro1 = sCodePage.Substring(num, length);


            //  num = sCodeFiltro1.IndexOf("lista-de-jogos-item");
            //  length = sCodeFiltro1.IndexOf("aside");
            //  string sCodeFiltro2 = sCodeFiltro1.Substring(num, length);


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
                                jogo.SiglaMandante = BuscarSigla(sTime1, sCampeonato);
                                jogo.SiglaVisitante = BuscarSigla(sTime2, sCampeonato);
                                jogo.PlacarMandante = sGolsTime1;
                                jogo.PlacarVisitante = sGolsTime2;
                                jogo.Campeonato = sCampeonato;

                                jogo.Datahora = sData;
                                jogo.Local = sLocal;

                                bool bJogoCadastrado = jogo.BuscarJogo();
                                if (sLocal != "ERRO")
                                    jogo.Local = sLocal;

                                if (bJogoCadastrado)
                                {
                                    jogo.AtualizarJogo();
                                }
                                else
                                {
                                    jogo.InserirJogo();
                                }
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
        }


        public string BuscarTime_UOL(string sInfo)
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


        public string BuscarLocal_UOL(string sInfo)
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


        public string BuscarData_UOL(string sInfo)
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


        public string BuscarGols_UOL(string sInfo)
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

        public static string BuscarSigla(string sTime, string sCampeonato)
        {


            Sigla sigla = new Sigla();
            sigla.Time = sTime;
            sigla.Campeonato = sCampeonato;
            sigla.SiglaTime = "";
            if (!sigla.BuscarSigla())
            {
                sigla.InserirSigla();
                return sigla.Time;
            }
            else
            {
                if (sigla.SiglaTime.Equals(""))
                    return sigla.Time;
                else
                    return sigla.SiglaTime;
            }

                    /*if (sCampeonato.Equals("PR"))
                        return BuscarSigla_PR(sTime);
                    else if (sCampeonato.Equals("RJ"))
                        return BuscarSigla_RJ(sTime);
                    else*/
                    ;
        }


        public void BuscarResultadoGloboEsporte(string sUrl, string sCampeonato)
        {

            string sCodePage = Brasileirao.code(sUrl);

            int num = sCodePage.IndexOf("lista-de-jogos-item");
            int length = sCodePage.Length - num;
            string sCodeFiltro1 = sCodePage.Substring(num, length);


            num = sCodeFiltro1.IndexOf("lista-de-jogos-item");
            length = sCodeFiltro1.IndexOf("aside");
            string sCodeFiltro2 = sCodeFiltro1.Substring(num, length);


            string[] separator = new string[]
            {
                "lista-de-jogos-item"
            };
            string[] array = sCodeFiltro2.Split(separator, StringSplitOptions.RemoveEmptyEntries);
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
            int num2 = 0;
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text4 = array2[i];
                int num3 = num2;
                num2 = num3 + 1;
                string[] array3 = text4.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                length = array3[3].IndexOf(">");
                string text5 = array3[3].Substring(0, length);
                length = array3[4].IndexOf(">");
                string text6 = array3[4].Substring(0, length);
                string[] array4 = text4.Split(separator3, StringSplitOptions.RemoveEmptyEntries);
                length = array4[1].IndexOf("<");
                string text7 = array4[1].Substring(1, length);
                string[] array5 = text4.Split(separator4, StringSplitOptions.RemoveEmptyEntries);
                length = array5[1].IndexOf("<");
                string text8 = array5[1].Substring(1, length);
                string[] array6 = text4.Split(separator5, StringSplitOptions.RemoveEmptyEntries);
                length = array6[1].IndexOf("<");
                string text9 = array6[1].Substring(1, length);
                string[] array7 = text4.Split(separator6, StringSplitOptions.RemoveEmptyEntries);
                length = array7[1].IndexOf("<");
                string text10 = array7[1].Substring(1, length);
                string[] array8 = array7[2].Split(separator7, StringSplitOptions.RemoveEmptyEntries);
                length = array8[1].IndexOf("<");
                string text11 = array8[1].Substring(1, length);

                Jogo jogo = new Jogo();
                jogo.Idrodada = 0;
                jogo.IdJogo = num2;
                jogo.SiglaMandante = BuscarSigla(text5.Replace(">", "").Replace('"', ' ').Replace("<", ""));
                jogo.SiglaVisitante = BuscarSigla(text6.Replace(">", "").Replace('"', ' ').Replace("<", ""));
                jogo.PlacarMandante = text8.Replace(">", "").Replace('"', ' ').Replace("<", "");
                jogo.PlacarVisitante = text9.Replace(">", "").Replace('"', ' ').Replace("<", "");
                jogo.Campeonato = sCampeonato;



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

                jogo.Datahora = text10.Replace(">", "").Replace('"', ' ').Replace("<", "") + "-" + text11.Replace(">", "").Replace('"', ' ').Replace("<", "");
                jogo.Local = text7.Replace(">", "").Replace('"', ' ').Replace("<", "");
                string sLocal = text7.Replace(">", "").Replace('"', ' ').Replace("<", "");
                Jogo jogoDB = new Jogo();
                jogoDB.Idrodada = jogo.Idrodada;
                jogoDB.IdJogo = jogo.IdJogo;
                jogoDB.SiglaMandante = jogo.SiglaMandante;
                jogoDB.SiglaVisitante = jogo.SiglaVisitante;
                jogoDB.PlacarVisitante = jogo.PlacarVisitante;
                jogoDB.PlacarMandante = jogo.PlacarMandante;
                jogoDB.Local = jogo.Local;
                jogoDB.Datahora = jogo.Datahora;
                jogoDB.Campeonato = sCampeonato;
                bool bJogoCadastrado = jogoDB.BuscarJogo();

                jogo.Local = sLocal;
                if (bJogoCadastrado)
                {
                    jogo.AtualizarJogo();

                }
                else
                {
                    jogo.InserirJogo();
                }
            }
        }

    }
}
