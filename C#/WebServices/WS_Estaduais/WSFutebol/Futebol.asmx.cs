using Newtonsoft.Json;
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
    /// Summary description for Futebol
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Futebol : System.Web.Services.WebService
    {

        [WebMethod]
        public string AtualizarResultadosDB(string sCampeonato)
        {

            if (sCampeonato.Equals("PR"))
            {
                BuscarResultadoUol("http://esporte.uol.com.br/futebol/campeonatos/paranaense/jogos/", sCampeonato);
                try
                {
                    BuscarResultadoGloboEsporte("http://globoesporte.globo.com/pr/futebol/campeonato-paranaense/", sCampeonato);
                }
                catch (Exception e)
                {
                }

            }
            else
                if (sCampeonato.Equals("BR"))
            {
                BuscarResultadoUol("https://esporte.uol.com.br/futebol/campeonatos/brasileirao/jogos/", sCampeonato);
             //   BuscarResultadoGloboEsporte("http://globoesporte.globo.com/futebol/brasileirao-serie-a/", sCampeonato);

            }

            return "OK";
        }


        [WebMethod]
        public string ExibeImagem(string sCampeonato)
        {

            if (sCampeonato.Equals("PR"))
            {
                return code("http://inaltum.futebol.servicos.ws/EXIBE_PR.txt");
            }
            else
                return "N";
        }

        [WebMethod]
        public string BuscarTodosJogos_DB(string sCampeonato)
        {

            return JsonConvert.SerializeObject(Jogo.BuscarTodosJogos(sCampeonato), Formatting.Indented);
        }


        [WebMethod]
        public string BuscarListaTimes(string sCampeonato)
        {

            return JsonConvert.SerializeObject(Sigla.BuscarListaSigla(sCampeonato), Formatting.Indented);
        }

        [WebMethod]
        public string InserirJogo(string sCampeonato, int idRodada, int idJogo, string sSiglaMandante, string sSiglaVisitante, string iPlacarMandante, string iPlacarVisitante, string sLocal, string sDataHora)
        {
            try
            {
                Jogo jogoDB = new Jogo();
                jogoDB.Idrodada = idRodada;
                jogoDB.IdJogo = idJogo;
                jogoDB.SiglaMandante = sSiglaMandante;
                jogoDB.SiglaVisitante = sSiglaVisitante;
                jogoDB.PlacarVisitante = iPlacarMandante;
                jogoDB.PlacarMandante = iPlacarVisitante;
                jogoDB.Local = sLocal;
                jogoDB.Datahora = sDataHora;
                jogoDB.Campeonato = sCampeonato;
                bool bJogoCadastrado = jogoDB.BuscarJogo();
                if (bJogoCadastrado)
                {

                    jogoDB.AtualizarJogo();
                    return "Jogo Atualizado!";
                }
                else
                {
                    jogoDB.InserirJogo();
                    return "Jogo Cadastrado";
                }
            }
            catch (Exception e)
            {

                return "Erro ao cadastrar: " + e.Message;
            }
        }

        [WebMethod]
        public string Classificacao(string sCampeonato)
        {
            string result = "";
            if (sCampeonato.Equals("PR"))
            {
                string sUrl = "http://esporte.uol.com.br/futebol/campeonatos/paranaense/jogos/";

                List<Classificacao> listclassificacao = new List<Classificacao>();
                string sTexto = code(sUrl);
                int ini = sTexto.IndexOf("Único");
                int fim = sTexto.IndexOf("</table>");
                fim = sTexto.Length - fim;
                string sTexto2 = sTexto.Substring(ini, fim);
                string[] stringSeparators = new string[]
                {
                    "<tr"
                };
                string[] svet = sTexto2.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                string[] stringSeparators2 = new string[]
                {
                    "nome-comum"
                };
                string[] stringSeparators3 = new string[]
                {
                    "<span>"
                };
                for (int i = 2; i < svet.Length; i++)
                {
                    Classificacao classificacao = new Classificacao();
                    svet[i] = svet[i].Replace("\t", "");
                    svet[i] = svet[i].Replace("\r", "");
                    svet[i] = svet[i].Replace("\n", "");
                    string[] svet2 = svet[i].Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);
                    ini = svet2[1].IndexOf(">") + 1;
                    fim = svet2[1].IndexOf("<");
                    classificacao.Equipe = svet2[1].Substring(ini, fim - ini);
                    string[] svet3 = svet2[1].Split(stringSeparators3, StringSplitOptions.RemoveEmptyEntries);
                    ini = 0;
                    classificacao.Pontos = int.Parse(svet3[1].Substring(ini, svet3[1].IndexOf("<") - ini));
                    classificacao.Jogos = int.Parse(svet3[2].Substring(ini, svet3[2].IndexOf("<") - ini));
                    classificacao.Vitorias = int.Parse(svet3[3].Substring(ini, svet3[3].IndexOf("<") - ini));
                    classificacao.Empates = int.Parse(svet3[4].Substring(ini, svet3[4].IndexOf("<") - ini));
                    classificacao.Derrotas = int.Parse(svet3[5].Substring(ini, svet3[5].IndexOf("<") - ini));
                    classificacao.GolsP = int.Parse(svet3[6].Substring(ini, svet3[6].IndexOf("<") - ini));
                    classificacao.GolsC = int.Parse(svet3[7].Substring(ini, svet3[7].IndexOf("<") - ini));
                    classificacao.Saldo = int.Parse(svet3[8].Substring(ini, svet3[8].IndexOf("<") - ini));
                    classificacao.Aproveitamento = int.Parse(svet3[9].Substring(ini, svet3[9].IndexOf("<") - ini));
                    classificacao.Campeonato = sCampeonato;
                    listclassificacao.Add(classificacao);
                }
                string json = JsonConvert.SerializeObject(listclassificacao, Formatting.Indented);
                result = json;


            }
            else
            if (sCampeonato.Equals("BR"))
            {
                string sUrl = "https://esporte.uol.com.br/futebol/campeonatos/brasileirao/jogos/";

                List<Classificacao> listclassificacao = new List<Classificacao>();
                string sTexto = code(sUrl);
                int ini = sTexto.IndexOf("Único");
                int fim = sTexto.IndexOf("</table>");
                fim = sTexto.Length - fim;
                string sTexto2 = sTexto.Substring(ini, fim);
                string[] stringSeparators = new string[]
                {
                    "<tr"
                };
                string[] svet = sTexto2.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                string[] stringSeparators2 = new string[]
                {
                    "nome-comum"
                };
                string[] stringSeparators3 = new string[]
                {
                    "<span>"
                };
                for (int i = 2; i < svet.Length; i++)
                {
                    Classificacao classificacao = new Classificacao();
                    svet[i] = svet[i].Replace("\t", "");
                    svet[i] = svet[i].Replace("\r", "");
                    svet[i] = svet[i].Replace("\n", "");
                    string[] svet2 = svet[i].Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);
                    ini = svet2[1].IndexOf(">") + 1;
                    fim = svet2[1].IndexOf("<");
                    classificacao.Equipe = svet2[1].Substring(ini, fim - ini);
                    string[] svet3 = svet2[1].Split(stringSeparators3, StringSplitOptions.RemoveEmptyEntries);
                    ini = 0;
                    classificacao.Pontos = int.Parse(svet3[1].Substring(ini, svet3[1].IndexOf("<") - ini));
                    classificacao.Jogos = int.Parse(svet3[2].Substring(ini, svet3[2].IndexOf("<") - ini));
                    classificacao.Vitorias = int.Parse(svet3[3].Substring(ini, svet3[3].IndexOf("<") - ini));
                    classificacao.Empates = int.Parse(svet3[4].Substring(ini, svet3[4].IndexOf("<") - ini));
                    classificacao.Derrotas = int.Parse(svet3[5].Substring(ini, svet3[5].IndexOf("<") - ini));
                    classificacao.GolsP = int.Parse(svet3[6].Substring(ini, svet3[6].IndexOf("<") - ini));
                    classificacao.GolsC = int.Parse(svet3[7].Substring(ini, svet3[7].IndexOf("<") - ini));
                    classificacao.Saldo = int.Parse(svet3[8].Substring(ini, svet3[8].IndexOf("<") - ini));
                    classificacao.Aproveitamento = int.Parse(svet3[9].Substring(ini, svet3[9].IndexOf("<") - ini));
                    classificacao.Campeonato = sCampeonato;
                    listclassificacao.Add(classificacao);
                }
                string json = JsonConvert.SerializeObject(listclassificacao, Formatting.Indented);
                result = json;


            }
            return result;
        }



        [WebMethod]
        public string TempoReal(string sCampeonato)
        {

            if (sCampeonato.Equals("PR"))
            {

                return JsonConvert.SerializeObject(BuscarTempoRealGloboEsporte("http://globoesporte.globo.com/pr/futebol/campeonato-paranaense/", sCampeonato), Formatting.Indented); 

            }
            else
                if (sCampeonato.Equals("RJ"))
            {

                return JsonConvert.SerializeObject(BuscarTempoRealGloboEsporte("http://globoesporte.globo.com/rj/futebol/campeonato-carioca/", sCampeonato), Formatting.Indented);

            }

            else
                if (sCampeonato.Equals("BR"))
            {

                return JsonConvert.SerializeObject(BuscarTempoRealGloboEsporte("https://esporte.uol.com.br/futebol/campeonatos/brasileirao/jogos/", sCampeonato), Formatting.Indented);

            }

            return "OK";
        }


        public void BuscarResultadoGloboEsporte(string sUrl, string sCampeonato)
        {

           string sCodePage = code(sUrl);

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
                jogo.SiglaMandante = BuscarSigla_PR(text5.Replace(">", "").Replace('"', ' ').Replace("<", ""));
                jogo.SiglaVisitante = BuscarSigla_PR(text6.Replace(">", "").Replace('"', ' ').Replace("<", ""));
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



        public List<Jogo> BuscarTempoRealGloboEsporte(string sUrl, string sCampeonato)
        {
            List<Jogo> lista = new List<Jogo>();
            string sCodePage = code(sUrl);
            string sRodada = BuscarRodadaGloboesporte(sCodePage);
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
                jogo.Idrodada = Int32.Parse(sRodada);
                jogo.IdJogo = num2;
                jogo.SiglaMandante = BuscarSigla_PR(text5.Replace(">", "").Replace('"', ' ').Replace("<", ""));
                jogo.SiglaVisitante = BuscarSigla_PR(text6.Replace(">", "").Replace('"', ' ').Replace("<", ""));
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

                lista.Add(jogo);
            }

            return lista;
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
            for (int i = 1; i < arrayJogos.Length; i++)
            {
                string sLinhaRodada = arrayJogos[i];
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


        public static string BuscarSigla_PR(string sTime)
        {

            string result = sTime.Trim();
            if (sTime.Trim() == "Cianorte" || sTime.Trim() == "Cianorte")
            {
                result = "CIA";
            }
            else
               if (sTime.Trim() == "Coritiba" || sTime.Trim() == "Coritiba")
            {
                result = "CFC";
            }
            else
                if (sTime.Trim() == "PSTC" || sTime.Trim() == "PSTC")
            {
                result = "PST";
            }
            else
                if (sTime.Trim() == "Toledo" || sTime.Trim() == "Toledo")
            {
                result = "TOL";
            }
            else
                if (sTime.Trim() == "Londrina" || sTime.Trim() == "Londrina")
            {
                result = "LON";
            }
            else
                if (sTime.Trim() == "Prudentópolis" || sTime.Trim() == "Prudentópolis")
            {
                result = "PRU";
            }
            else
                if (sTime.Trim() == "Rio Branco-PR" || sTime.Trim() == "RioBranco-PR")
            {
                result = "RPR";
            }
            else
                if (sTime.Trim() == "Atlético-PR" || sTime.Trim() == "Atlético-PR")
            {
                result = "CAP";
            }
            else
                if (sTime.Trim() == "JMalucelli" || sTime.Trim() == "JMalucelli")
            {
                result = "JMA";
            }
            else
                if (sTime.Trim() == "FC Cascavel" || sTime.Trim() == "FCCascavel")
            {
                result = "FCC";
            }
            else
                if (sTime.Trim() == "Paraná" || sTime.Trim() == "Paraná")
            {
                result = "PAR";
            }
            else
                if (sTime.Trim() == "Foz do Iguaçu" || sTime.Trim() == "FozdoIguaçu")
            {
                result = "FOZ";
            }



            return result;
        }


        public static string BuscarSigla_RJ(string sTime)
        {

            string result = sTime.Trim();
            if (sTime.Trim() == "Campos" || sTime.Trim() == "Campos")
            {
                result = "CAA";
            }
            else
               if (sTime.Trim() == "Bonsucesso" || sTime.Trim() == "Bonsucesso")
            {
                result = "BON";
            }
            else
                if (sTime.Trim() == "Tigres do Brasil" || sTime.Trim() == "Tigres do Brasil")
            {
                result = "TIG";
            }
            else
                if (sTime.Trim() == "Cabofriense" || sTime.Trim() == "Cabofriense")
            {
                result = "CAB";
            }
            else
                if (sTime.Trim() == "Portuguesa-RJ" || sTime.Trim() == "Portuguesa")
            {
                result = "POR";
            }
            else
                if (sTime.Trim() == "Nova Iguaçu" || sTime.Trim() == "Nova Iguaçu")
            {
                result = "NIG";
            }
            else
                if (sTime.Trim() == "Flamengo" || sTime.Trim() == "Flamengo")
            {
                result = "FLA";
            }
            else
                if (sTime.Trim() == "Atlético-PR" || sTime.Trim() == "Atlético-PR")
            {
                result = "BVT";
            }
            else
                if (sTime.Trim() == "JMalucelli" || sTime.Trim() == "JMalucelli")
            {
                result = "MAC";
            }
            else
                if (sTime.Trim() == "FC Cascavel" || sTime.Trim() == "FCCascavel")
            {
                result = "MAD";
            }
            else
                if (sTime.Trim() == "Paraná" || sTime.Trim() == "Paraná")
            {
                result = "VAS";
            }
            else
                if (sTime.Trim() == "Foz do Iguaçu" || sTime.Trim() == "FozdoIguaçu")
            {
                result = "FLU";
            }
            else
                if (sTime.Trim() == "Paraná" || sTime.Trim() == "Paraná")
            {
                result = "RES";
            }
            else
                if (sTime.Trim() == "Foz do Iguaçu" || sTime.Trim() == "FozdoIguaçu")
            {
                result = "VRE";
            }
            else
                if (sTime.Trim() == "Botafogo" || sTime.Trim() == "Botafogo")
            {
                result = "BOT";
            }
            else
                if (sTime.Trim() == "Boavista-RJ" || sTime.Trim() == "Botafogo")
            {
                result = "BOV";
            }





            return result;
        }


        public string BuscarRodadaGloboesporte(string sCodePage)
        {
            string sRodada = "0";

            try
            {

                int iPosIni = sCodePage.IndexOf("data-rodada") + 2;
                int iPosFim = sCodePage.IndexOf("data-rodadas");
                sRodada = sCodePage.Substring(iPosIni, (iPosFim - iPosIni));
            }
            catch
            {
            }

            sRodada = String.Join("", System.Text.RegularExpressions.Regex.Split(sRodada, @"[^\d]"));

            try
            {

                int iRodada = 0;
                iRodada = Int32.Parse(sRodada);
            }
            catch
            {

                sRodada = "";
            }

            return sRodada;
        }


        public string ListaTimes(string sCampeonato)
        {
            return "";

        }

    }
}
