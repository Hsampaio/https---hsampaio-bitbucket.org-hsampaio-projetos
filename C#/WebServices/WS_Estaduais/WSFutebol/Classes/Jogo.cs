using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace WSFutebol.Classes
{
    public class Jogo
    {
        private int idrodada;
        private int idJogo;
        private string siglaMandante;
        private string siglaVisitante;
        private string placarMandante;
        private string placarVisitante;
        private string local;
        private string datahora;
        private string status;
        private string campeonato;

        public int Idrodada
        {
            get
            {
                return idrodada;
            }

            set
            {
                idrodada = value;
            }
        }

        public int IdJogo
        {
            get
            {
                return idJogo;
            }

            set
            {
                idJogo = value;
            }
        }

        public string SiglaMandante
        {
            get
            {
                return siglaMandante;
            }

            set
            {
                siglaMandante = value;
            }
        }

        public string SiglaVisitante
        {
            get
            {
                return siglaVisitante;
            }

            set
            {
                siglaVisitante = value;
            }
        }

        public string PlacarMandante
        {
            get
            {
                return placarMandante;
            }

            set
            {
                placarMandante = value;
            }
        }

        public string PlacarVisitante
        {
            get
            {
                return placarVisitante;
            }

            set
            {
                placarVisitante = value;
            }
        }

        public string Local
        {
            get
            {
                return local;
            }

            set
            {
                local = value;
            }
        }

        public string Datahora
        {
            get
            {
                return datahora;
            }

            set
            {
                datahora = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
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

        public bool BuscarJogo()
        {

            DBConnect dbSelect = new DBConnect();
            string sComandoSql = @" SELECT PLACARMANDANTE, PLACARVISITANTE, LOCAL
                                      FROM T_RODADA_CAMP        
                                     WHERE SIGLAMANDANTE = @SIGLAMANDANTE
                                       AND SIGLAVISITANTE = @SIGLAVISITANTE
                                       AND CAMPEONATO = @CAMPEONATO ";
                                        
            MySqlCommand cmd = new MySqlCommand(sComandoSql);
            cmd.Parameters.AddWithValue("@SIGLAMANDANTE", this.siglaMandante);
            cmd.Parameters.AddWithValue("@SIGLAVISITANTE", this.siglaVisitante);
            cmd.Parameters.AddWithValue("@CAMPEONATO", this.campeonato);



            try
            {
                MySqlDataReader dataReader = dbSelect.ExecuteReader(cmd);
                while (dataReader.Read())
                {
                    
                    this.placarMandante = dataReader["PLACARMANDANTE"].ToString();
                    this.placarVisitante = dataReader["PLACARVISITANTE"].ToString();
                    this.local = dataReader["LOCAL"].ToString();

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


        public void AtualizarJogo()
        {
            DBConnect dbUpdate = new DBConnect();
            string sComandoSql = @"UPDATE T_RODADA_CAMP SET PLACARMANDANTE = @PLACARMANDANTE, 
                                                            PLACARVISITANTE = @PLACARVISITANTE,
                                                            LOCAL = @LOCAL,
                                                            DATAHORA = @DATAHORA,
                                                            IDJOGO = @IDJOGO       
                                    WHERE SIGLAMANDANTE = @SIGLAMANDANTE  
                                      AND SIGLAVISITANTE = @SIGLAVISITANTE 
                                      AND CAMPEONATO = @CAMPEONATO";

            MySqlCommand cmd = new MySqlCommand(sComandoSql);

            cmd.Parameters.AddWithValue("@PLACARMANDANTE", this.placarMandante);
            cmd.Parameters.AddWithValue("@PLACARVISITANTE", this.PlacarVisitante);
            cmd.Parameters.AddWithValue("@SIGLAVISITANTE", this.siglaVisitante);
            cmd.Parameters.AddWithValue("@SIGLAMANDANTE", this.siglaMandante);
            cmd.Parameters.AddWithValue("@LOCAL", this.local);
            cmd.Parameters.AddWithValue("@DATAHORA", this.datahora);
            cmd.Parameters.AddWithValue("@CAMPEONATO", this.campeonato);
            cmd.Parameters.AddWithValue("@IDJOGO", this.idJogo);
          //  cmd.Parameters.AddWithValue("@IDRODADA", this.idrodada);

            try
            {
                dbUpdate.ExecuteNonQuery(cmd);
                dbUpdate.Close_Connection();
            }
            catch (Exception e)
            {
               
            }
        }
        
        public void InserirJogo()
        {

            DBConnect dbInsert = new DBConnect();
            string sComandoSql = "INSERT INTO T_RODADA_CAMP(IDRODADA, IDJOGO, SIGLAMANDANTE, SIGLAVISITANTE, DATAHORA, LOCAL, PLACARMANDANTE, PLACARVISITANTE, CAMPEONATO) " +
                                 "                         VALUES(@IDRODADA, @IDJOGO, @SIGLAMANDANTE, @SIGLAVISITANTE, @DATAHORA, @LOCAL, @PLACARMANDANTE, @PLACARVISITANTE, @CAMPEONATO)";

            MySqlCommand cmd = new MySqlCommand(sComandoSql);

            cmd.Parameters.AddWithValue("@IDRODADA", this.idrodada);
            cmd.Parameters.AddWithValue("@IDJOGO", this.idJogo);
            cmd.Parameters.AddWithValue("@SIGLAMANDANTE", this.siglaMandante);
            cmd.Parameters.AddWithValue("@SIGLAVISITANTE", this.siglaVisitante);
            cmd.Parameters.AddWithValue("@DATAHORA", this.datahora);
            cmd.Parameters.AddWithValue("@LOCAL", this.local);
            cmd.Parameters.AddWithValue("@PLACARMANDANTE", this.placarMandante);
            cmd.Parameters.AddWithValue("@PLACARVISITANTE", this.placarVisitante);
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


        public static List<Jogo> BuscarTodosJogos(string sCampeonato)
        {
            List<Jogo> list = new List<Jogo>()
                ;
            DBConnect dbSelect = new DBConnect();
            string sComandoSql = @" SELECT IDRODADA, IDJOGO, SIGLAMANDANTE, SIGLAVISITANTE, DATAHORA, LOCAL, 
                                           PLACARMANDANTE, PLACARVISITANTE, CAMPEONATO
                                      FROM T_RODADA_CAMP        
                                     WHERE CAMPEONATO = @CAMPEONATO ";

            MySqlCommand cmd = new MySqlCommand(sComandoSql);
            cmd.Parameters.AddWithValue("@CAMPEONATO", sCampeonato);



            try
            {
                MySqlDataReader dataReader = dbSelect.ExecuteReader(cmd);
                while (dataReader.Read())
                {
                    Jogo jogo = new Jogo();
                    jogo.idrodada = Int32.Parse(dataReader["IDRODADA"].ToString());
                    jogo.idJogo = Int32.Parse(dataReader["IDJOGO"].ToString());
                    jogo.siglaMandante = dataReader["SIGLAMANDANTE"].ToString();
                    jogo.siglaVisitante = dataReader["SIGLAVISITANTE"].ToString();
                    jogo.datahora = dataReader["DATAHORA"].ToString();
                    jogo.local = dataReader["LOCAL"].ToString();
                    jogo.placarMandante = dataReader["PLACARMANDANTE"].ToString();
                    jogo.placarVisitante = dataReader["PLACARVISITANTE"].ToString();
                    jogo.campeonato = dataReader["CAMPEONATO"].ToString();

                    list.Add(jogo);


                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            dbSelect.Close_Connection();
            return list;

        }
    }
}