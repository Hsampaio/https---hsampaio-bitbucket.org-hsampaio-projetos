using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace WSFutebol.Classes
{
    public class Notificacao
    {
        private int codigo;
        private string descricao;
        private string rotina;
        private string info;
        private string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string Rotina
        {
            get { return rotina; }
            set { rotina = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }



        public string EnviarNotificacaoTodos()
        {
            List<Pessoa> lista = new List<Pessoa>();

            lista = Pessoa.BuscarListaPessoas();
            string sRetorno = "OK";
            string codigoGCM = "";

            if (lista.Count == 1)
            {
                foreach (Pessoa item in lista)
                {
                    codigoGCM += "\"" + item.Gcm + "\",";
                    try
                    {
                        string postData = "{ \"registration_ids\" : [" + codigoGCM + "],   \"data\" : " + JsonConvert.SerializeObject(this) + " }";
                        sRetorno = Util_GCM.SendGCMNotification(postData);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

            }
            else
            {


                int iCont = 0;

                foreach (Pessoa item in lista)
                {
                    iCont++;
                    codigoGCM += "\"" + item.Gcm + "\",";
                    if (iCont == 999)
                    {

                        try
                        {
                            string postData = "{ \"registration_ids\" : [" + codigoGCM + "],   \"data\" : " + JsonConvert.SerializeObject(this) + " }";
                            sRetorno = Util_GCM.SendGCMNotification(postData);
                            codigoGCM = "";
                            iCont = 0;
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }

                }

                if (codigoGCM != "")
                {
                    try
                    {
                        string postData = "{ \"registration_ids\" : [" + codigoGCM + "],   \"data\" : " + JsonConvert.SerializeObject(this) + " }";
                        sRetorno = Util_GCM.SendGCMNotification(postData);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
            return sRetorno;
        }

        public string EnviarNotificacao(string sCodigoGCM)
        {
            string sRetorno = "OK";
            string codigoGCM = "\"" + sCodigoGCM + "\"";

            try
            {
                string postData = "{ \"registration_ids\" : [" + codigoGCM + "],   \"data\" : " + JsonConvert.SerializeObject(this) + " }";
                sRetorno = Util_GCM.SendGCMNotification(postData);
            }
            catch (Exception)
            {

                throw;
            }
            return sRetorno;
        }





        public static bool EnviarEmail1(string destinatario, string assunto, string mensagem)
        {
            try
            {
                //crio objeto responsável pela mensagem de email

                MailMessage objEmail = new MailMessage();

                //rementente do email
                objEmail.From = new MailAddress("rhsolucoes.bazar@gmail.com");

                //email para resposta(quando o destinatário receber e clicar em responder, vai para:)
                objEmail.ReplyTo = new MailAddress("rhsolucoes.bazar@gmail.com");

                //destinatário(s) do email(s). Obs. pode ser mais de um, pra isso basta repetir a linha
                //abaixo com outro endereço
                objEmail.To.Add(destinatario);

                //se quiser enviar uma cópia oculta pra alguém, utilize a linha abaixo:
                //objEmail.Bcc.Add("equipe.ela@gmail.com");

                //prioridade do email
                objEmail.Priority = MailPriority.Normal;

                //utilize true pra ativar html no conteúdo do email, ou false, para somente texto
                objEmail.IsBodyHtml = true;

                //Assunto do email
                objEmail.Subject = assunto;

                //corpo do email a ser enviado
                objEmail.Body = mensagem;

                //codificação do assunto do email para que os caracteres acentuados serem reconhecidos.
                objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");

                //codificação do corpo do emailpara que os caracteres acentuados serem reconhecidos.
                objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                //cria o objeto responsável pelo envio do email
                SmtpClient objSmtp = new SmtpClient();

                // Esta porta é a utilizada pelo Gmail para envio
                objSmtp.Port = 587;

                //endereço do servidor SMTP(para mais detalhes leia abaixo do código)
                objSmtp.Host = "smtp.gmail.com";

                //Gmail trabalha com Server Secured Layer
                objSmtp.EnableSsl = true;

                //para envio de email autenticado, coloque login e senha de seu servidor de email
                //para detalhes leia abaixo do código
                objSmtp.Credentials = new NetworkCredential("rhsolucoes.bazar@gmail.com", "rh1rh1rh1");


                //envia o email
                objSmtp.Send(objEmail);
                return true;
            }
            catch (Exception ee)
            {
                string eee = ee.Message;
                return false;
                throw;
            }

        }

    }
}