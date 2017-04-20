using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSFutebol.Classes
{
    public class Sigla
    {

        private int codigo;
        private string siglaTime;
        private string time;
        private string campeonato;
        private string imagem;

        public Sigla()
        {


        }

        public Sigla(int Codigo, string SiglaTime, string Time, string Campeonato, string Imagem)
        {
            this.codigo = Codigo;
            this.SiglaTime = SiglaTime;
            this.Time = Time;
            this.Campeonato = Campeonato;
            this.Imagem = Imagem;

        }

        public int Codigo
        {
            get
            {
                return codigo;
            }

            set
            {
                codigo = value;
            }
        }

        public string Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
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

        public string SiglaTime
        {
            get
            {
                return siglaTime;
            }

            set
            {
                siglaTime = value;
            }
        }

        public string Imagem
        {
            get
            {
                return imagem;
            }

            set
            {
                imagem = value;
            }
        }
        
                public bool BuscarSigla()
                {

                    DBConnect dbSelect = new DBConnect();
                    string sComandoSql = @" SELECT SIGLA, TIME
                                              FROM T_SIGLA        
                                             WHERE TIME = @TIME
                                               AND CAMPEONATO = @CAMPEONATO ";

                    MySqlCommand cmd = new MySqlCommand(sComandoSql);

                    cmd.Parameters.AddWithValue("@TIME", this.time);
                    cmd.Parameters.AddWithValue("@CAMPEONATO", this.campeonato);



                    try
                    {
                        MySqlDataReader dataReader = dbSelect.ExecuteReader(cmd);
                        while (dataReader.Read())
                        {

                            this.siglaTime = dataReader["SIGLA"].ToString();

                            dbSelect.Close_Connection();
                            return true;


                        }
                    }
                    catch (Exception e)
                    {
                        string ex = e.Message;
                    }
                    dbSelect.Close_Connection();
                    return false;

                }


                public static List<Sigla> BuscarListaSigla(string sCampeonato)
                {


                    List<Sigla> lista = new List<Sigla>();

                    DBConnect dbSelect = new DBConnect();
                    string sComandoSql = @" SELECT SIGLA, TIME
                                              FROM T_SIGLA        
                                             WHERE CAMPEONATO = @CAMPEONATO ";

                    MySqlCommand cmd = new MySqlCommand(sComandoSql);


                    cmd.Parameters.AddWithValue("@CAMPEONATO", sCampeonato);



                    try
                    {
                        MySqlDataReader dataReader = dbSelect.ExecuteReader(cmd);
                        while (dataReader.Read())
                        {



                            lista.Add(new Sigla(0, dataReader["SIGLA"].ToString(), dataReader["TIME"].ToString(), sCampeonato, ""));




                        }
                    }
                    catch (Exception e)
                    {
                        string ex = e.Message;
                    }
                    dbSelect.Close_Connection();
                    return lista;

                }


                public void InserirSigla()
                {

                    DBConnect dbInsert = new DBConnect();
                    string sComandoSql = "INSERT INTO T_SIGLA(SIGLA, TIME, CAMPEONATO) " +
                                         "             VALUES(@SIGLA, @TIME, @CAMPEONATO)";

                    MySqlCommand cmd = new MySqlCommand(sComandoSql);

                    cmd.Parameters.AddWithValue("@SIGLA", this.siglaTime);
                    cmd.Parameters.AddWithValue("@TIME", this.time);
                    cmd.Parameters.AddWithValue("@CAMPEONATO", this.campeonato);


                    try
                    {
                        dbInsert.ExecuteNonQuery(cmd);
                        dbInsert.Close_Connection();
                    }
                    catch (Exception e)
                    {

                    }


                }

        


        public static string BuscarSigla(string sTime, string sCampeonato)
        {
            if (sCampeonato.Equals("BR_A"))
            {
                return BuscarSigla_A(sTime);
            }
            else
            if (sCampeonato.Equals("BR_B"))
            {
                return BuscarSigla_B(sTime);
            }
            else
                return sTime;
        }

        public static string BuscarSigla_B(string sTime)
        {

            List<string> listaTime = new List<string>();
            listaTime.Add("ABC");
            listaTime.Add("Paraná Clube");
            listaTime.Add("Náutico");
            listaTime.Add("América - MG");
            listaTime.Add("Criciúma");
            listaTime.Add("Santa Cruz - PE");
            listaTime.Add("Goiás");
            listaTime.Add("Figueirense");
            listaTime.Add("Guarani");
            listaTime.Add("Brasil de Pelotas");
            listaTime.Add("Juventude");
            listaTime.Add("Luverdense");
            listaTime.Add("Londrina - PR");
            listaTime.Add("Internacional");
            listaTime.Add("Boa Esporte Clube");
            listaTime.Add("Vila Nova-GO");
            listaTime.Add("Paysandu");
            listaTime.Add("Oeste");
            listaTime.Add("CRB");
            listaTime.Add("Ceará");
            listaTime.Add("Londrina");
            listaTime.Add("Santa Cruz");
            listaTime.Add("Boa Esporte");
            listaTime.Add("Vila Nova");
            listaTime.Add("América - MG");



            List<string> listaSigla = new List<string>();
            listaSigla.Add("ABC");
            listaSigla.Add("PAR");
            listaSigla.Add("NAU");
            listaSigla.Add("AME");
            listaSigla.Add("CRI");
            listaSigla.Add("STA");
            listaSigla.Add("GOI");
            listaSigla.Add("FIG");
            listaSigla.Add("GUA");
            listaSigla.Add("BRP");
            listaSigla.Add("JUV");
            listaSigla.Add("LUV");
            listaSigla.Add("LON");
            listaSigla.Add("INT");
            listaSigla.Add("BEC");
            listaSigla.Add("VIL");
            listaSigla.Add("PAY");
            listaSigla.Add("OES");
            listaSigla.Add("CRB");
            listaSigla.Add("CEA");
            listaSigla.Add("LON");
            listaSigla.Add("STA");
            listaSigla.Add("BEC");
            listaSigla.Add("VIL");
            listaSigla.Add("AME");

            int iIndex = 0;

            foreach (string item in listaTime)
            {

                string sTimeLista = item.Replace(" ", "");
                string sTimeParam = sTime.Replace(" ", "");
                if (sTimeLista.Equals(sTimeParam))
                {
                    return listaSigla[iIndex];
                }
                iIndex++;
            }




            return sTime;
        }

        public static string BuscarSigla_A(string sTime)
        {

            List<string> listaTime = new List<string>();
            listaTime.Add("Atlético-GO");
            listaTime.Add("Atlético-MG");
            listaTime.Add("Atlético-PR");
            listaTime.Add("Avaí");
            listaTime.Add("Bahia");
            listaTime.Add("Botafogo");
            listaTime.Add("Chapecoense");
            listaTime.Add("Corinthians");
            listaTime.Add("Coritiba");
            listaTime.Add("Cruzeiro");
            listaTime.Add("Flamengo");
            listaTime.Add("Fluminense");
            listaTime.Add("Grêmio");
            listaTime.Add("Palmeiras");
            listaTime.Add("Ponte Preta");
            listaTime.Add("Santos");
            listaTime.Add("Sport");
            listaTime.Add("São Paulo");
            listaTime.Add("Vasco");
            listaTime.Add("Vitória");


            List<string> listaSigla = new List<string>();
            listaSigla.Add("ACG");
            listaSigla.Add("CAM");
            listaSigla.Add("CAP");
            listaSigla.Add("AME");
            listaSigla.Add("AVA");
            listaSigla.Add("BAH");
            listaSigla.Add("BOT");
            listaSigla.Add("CHA");
            listaSigla.Add("COR");
            listaSigla.Add("CFC");
            listaSigla.Add("CRU");
            listaSigla.Add("FLA");
            listaSigla.Add("FLU");
            listaSigla.Add("GRE");
            listaSigla.Add("PAL");
            listaSigla.Add("PON");
            listaSigla.Add("SAN");
            listaSigla.Add("SPO");
            listaSigla.Add("SAO");
            listaSigla.Add("VAS");
            listaSigla.Add("VIT");


            int iIndex = 0;

            foreach (string item in listaTime)
            {

                string sTimeLista = item.Replace(" ", "");
                string sTimeParam = sTime.Replace(" ", "");
                if (sTimeLista.Equals(sTimeParam))
                {
                    return listaSigla[iIndex];
                }
                iIndex++;
            }




            return sTime;
        }


    }
}