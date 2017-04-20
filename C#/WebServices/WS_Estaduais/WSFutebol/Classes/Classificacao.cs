using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSFutebol.Classes
{
    public class Classificacao
    {


        private string equipe;

        private int jogos;

        private int pontos;

        private int vitorias;

        private int derrotas;

        private int empates;

        private int golsP;

        private int golsC;

        private int saldo;

        private int aproveitamento;

        private string grupo;

        private string campeonato;

        public string Equipe
        {
            get
            {
                return this.equipe;
            }
            set
            {
                this.equipe = value;
            }
        }

        public int Jogos
        {
            get
            {
                return this.jogos;
            }
            set
            {
                this.jogos = value;
            }
        }

        public int Pontos
        {
            get
            {
                return this.pontos;
            }
            set
            {
                this.pontos = value;
            }
        }

        public int Vitorias
        {
            get
            {
                return this.vitorias;
            }
            set
            {
                this.vitorias = value;
            }
        }

        public int Derrotas
        {
            get
            {
                return this.derrotas;
            }
            set
            {
                this.derrotas = value;
            }
        }

        public int Empates
        {
            get
            {
                return this.empates;
            }
            set
            {
                this.empates = value;
            }
        }

        public int GolsP
        {
            get
            {
                return this.golsP;
            }
            set
            {
                this.golsP = value;
            }
        }

        public int GolsC
        {
            get
            {
                return this.golsC;
            }
            set
            {
                this.golsC = value;
            }
        }

        public int Saldo
        {
            get
            {
                return this.saldo;
            }
            set
            {
                this.saldo = value;
            }
        }

        public int Aproveitamento
        {
            get
            {
                return this.aproveitamento;
            }
            set
            {
                this.aproveitamento = value;
            }
        }

        public string Grupo
        {
            get
            {
                return this.grupo;
            }
            set
            {
                this.grupo = value;
            }
        }

        public string Campeonato
        {
            get
            {
                return campeonato;
            }

            set
            {
                campeonato = value;
            }
        }


        public void AtualizarClassificacao()
        {
            DBConnect dbUpdate = new DBConnect();
            string sComandoSql = @"UPDATE T_CLASSIFICACAO SET PONTOS = @PONTOS, 
                                                            VITORIAS = @VITORIAS,
                                                            DERROTAS = @DERROTAS,
                                                            EMPATES = @EMPATES,
                                                            GOLSPRO = @GOLSPRO, 
                                                            GOLSCON = @GOLSCON,
                                                            SALDO = @SALDO,
                                                            GRUPO = @GRUPO
                                    WHERE EQUIPE = @EQUIPE
                                      AND CAMPEONATO = @CAMPEONATO";


            MySqlCommand cmd = new MySqlCommand(sComandoSql);

            cmd.Parameters.AddWithValue("@PONTOS", this.pontos);
            cmd.Parameters.AddWithValue("@VITORIAS", this.vitorias);
            cmd.Parameters.AddWithValue("@DERROTAS", this.derrotas);
            cmd.Parameters.AddWithValue("@EMPATES", this.empates);
            cmd.Parameters.AddWithValue("@GOLSPRO", this.golsP);
            cmd.Parameters.AddWithValue("@GOLSCON", this.golsC);
            cmd.Parameters.AddWithValue("@SALDO", this.saldo);
            cmd.Parameters.AddWithValue("@GRUPO", this.grupo);
            cmd.Parameters.AddWithValue("@CAMPEONATO", this.campeonato);
            cmd.Parameters.AddWithValue("@EQUIPE", this.equipe);

            try
            {
                dbUpdate.ExecuteNonQuery(cmd);
                dbUpdate.Close_Connection();
            }
            catch (Exception e)
            {

            }
        }

        public void InserirClassificacao()
        {

            DBConnect dbInsert = new DBConnect();
            string sComandoSql = "INSERT INTO T_CLASSIFICACAO(PONTOS, VITORIAS, DERROTAS, EMPATES, GOLSPRO, GOLSCON, SALDO, GRUPO, CAMPEONATO, EQUIPE) " +
                                 "                     VALUES(@PONTOS, @VITORIAS, @DERROTAS, @EMPATES, @GOLSPRO, @GOLSCON, @SALDO, @GRUPO, @CAMPEONATO, @EQUIPE)";

            MySqlCommand cmd = new MySqlCommand(sComandoSql);

            cmd.Parameters.AddWithValue("@PONTOS", this.pontos);
            cmd.Parameters.AddWithValue("@VITORIAS", this.vitorias);
            cmd.Parameters.AddWithValue("@DERROTAS", this.derrotas);
            cmd.Parameters.AddWithValue("@EMPATES", this.empates);
            cmd.Parameters.AddWithValue("@GOLSPRO", this.golsP);
            cmd.Parameters.AddWithValue("@GOLSCON", this.golsC);
            cmd.Parameters.AddWithValue("@SALDO", this.saldo);
            cmd.Parameters.AddWithValue("@GRUPO", this.grupo);
            cmd.Parameters.AddWithValue("@CAMPEONATO", this.campeonato);
            cmd.Parameters.AddWithValue("@EQUIPE", this.equipe);

            try
            {
                dbInsert.ExecuteNonQuery(cmd);
                dbInsert.Close_Connection();
            }
            catch (Exception e)
            {

            }


        }


        public static string BuscarClassificacao(string sUrl)
        {

     
            string codigoHTML = Utils.code(sUrl);
            List<Classificacao> list = new List<Classificacao>();
            int num = codigoHTML.IndexOf("Único");
            int num2 = codigoHTML.IndexOf("</table>");
            num2 = codigoHTML.Length - num2;
            string text2 = codigoHTML.Substring(num, num2);
            string[] separator = new string[]
            {
                        "<tr"
            };
            string[] array = text2.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            string[] separator2 = new string[]
            {
                        "nome-comum"
            };
            string[] separator3 = new string[]
            {
                        "<span>"
            };
            for (int i = 2; i < array.Length; i++)
            {
                Classificacao classificacao = new Classificacao();
                array[i] = array[i].Replace("\t", "");
                array[i] = array[i].Replace("\r", "");
                array[i] = array[i].Replace("\n", "");
                string[] array2 = array[i].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                num = array2[1].IndexOf(">") + 1;
                num2 = array2[1].IndexOf("<");
                classificacao.Equipe = array2[1].Substring(num, num2 - num);
                string[] array3 = array2[1].Split(separator3, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 1; j < array3.Length; j++)
                {
                    num = 0;
                    num2 = array3[j].IndexOf("<");
                    if (j == 1)
                    {
                        classificacao.Pontos = int.Parse(array3[j].Substring(num, num2 - num));
                    }
                    if (j == 2)
                    {
                        classificacao.Jogos = int.Parse(array3[j].Substring(num, num2 - num));
                    }
                    if (j == 3)
                    {
                        classificacao.Vitorias = int.Parse(array3[j].Substring(num, num2 - num));
                    }
                    if (j == 4)
                    {
                        classificacao.Empates = int.Parse(array3[j].Substring(num, num2 - num));
                    }
                    if (j == 5)
                    {
                        classificacao.Derrotas = int.Parse(array3[j].Substring(num, num2 - num));
                    }
                    if (j == 6)
                    {
                        classificacao.GolsP = int.Parse(array3[j].Substring(num, num2 - num));
                    }
                    if (j == 7)
                    {
                        classificacao.GolsC = int.Parse(array3[j].Substring(num, num2 - num));
                    }
                    if (j == 8)
                    {
                        classificacao.Saldo = int.Parse(array3[j].Substring(num, num2 - num));
                    }
                    if (j == 9)
                    {

                    }
                }
                list.Add(classificacao);
            }
            return JsonConvert.SerializeObject(list, Formatting.Indented);

          
        }

    }


}