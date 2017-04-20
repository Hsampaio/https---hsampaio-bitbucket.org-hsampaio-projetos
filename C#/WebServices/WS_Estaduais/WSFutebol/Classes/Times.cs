using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSFutebol.Classes
{
    public class Times
    {
        public Times(string sCmapeonato)
        {
            if (sCmapeonato.Equals("BR_A"))
            {
                ListaTimes_A();
            }

            if (sCmapeonato.Equals("BR_B"))
            {
                ListaTimes_B();
            }
        }

        private List<Sigla> listTime;

       
        public List<Sigla> ListTime
        {
            get
            {
                return listTime;
            }

            set
            {
                listTime = value;
            }
        }

        private void ListaTimes_A()
        {

            this.listTime = new List<Sigla>();                  


            listTime.Add(new Sigla(1, "AGO", "Atlético-GO", "BR_A", ""));
            listTime.Add(new Sigla(2, "CAM", "Atlético-MG", "BR_A", ""));
            listTime.Add(new Sigla(3, "CAP", "Atlético-PR", "BR_A", ""));
            listTime.Add(new Sigla(4, "AVA", "Avaí", "BR_A", ""));
            listTime.Add(new Sigla(5, "BAH", "Bahia", "BR_A", ""));
            listTime.Add(new Sigla(6, "BOT", "Botafogo", "BR_A", ""));
            listTime.Add(new Sigla(7, "CHA", "Chapecoense", "BR_A", ""));
            listTime.Add(new Sigla(8, "COR", "Corinthians", "BR_A", ""));
            listTime.Add(new Sigla(9, "CFC", "Coritiba", "BR_A", ""));
            listTime.Add(new Sigla(10, "CRU", "Cruzeiro", "BR_A", ""));
            listTime.Add(new Sigla(11, "FLA", "Flamengo", "BR_A", ""));
            listTime.Add(new Sigla(12, "FLU", "Fluminense", "BR_A", ""));
            listTime.Add(new Sigla(13, "GRE", "Grêmio", "BR_A", ""));
            listTime.Add(new Sigla(14, "PAL", "Palmeiras", "BR_A", ""));
            listTime.Add(new Sigla(15, "PON", "Ponte Preta", "BR_A", ""));
            listTime.Add(new Sigla(16, "SAN", "Santos", "BR_A", ""));
            listTime.Add(new Sigla(17, "SAO", "São Paulo", "BR_A", ""));
            listTime.Add(new Sigla(18, "SPO", "Sport", "BR_A", ""));
            listTime.Add(new Sigla(19, "VAS", "Vasco", "BR_A", ""));
            listTime.Add(new Sigla(20, "VIT", "Vitória", "BR_A", ""));   

 
        }


        private void ListaTimes_B()
        {

            this.listTime = new List<Sigla>();


            listTime.Add(new Sigla(1, "ABC", "ABC", "BR_B", ""));
            listTime.Add(new Sigla(2, "PAR", "Paraná Clube", "BR_B", ""));
            listTime.Add(new Sigla(3, "NAU", "Náutico", "BR_B", ""));
            listTime.Add(new Sigla(4, "AME", "América - MG", "BR_B", ""));
            listTime.Add(new Sigla(5, "CRI", "Criciúma", "BR_B", ""));
            listTime.Add(new Sigla(6, "STA", "Santa Cruz", "BR_B", ""));
            listTime.Add(new Sigla(7, "GOI", "Goiás", "BR_B", ""));
            listTime.Add(new Sigla(8, "FIG", "Figueirense", "BR_B", ""));
            listTime.Add(new Sigla(9, "GUA", "Guarani", "BR_B", ""));
            listTime.Add(new Sigla(10, "BRP", "Brasil de Pelotas", "BR_B", ""));
            listTime.Add(new Sigla(11, "JUV", "Juventude", "BR_B", ""));
            listTime.Add(new Sigla(12, "LUV", "Luverdense", "BR_B", ""));
            listTime.Add(new Sigla(13, "LON", "Londrina", "BR_B", ""));
            listTime.Add(new Sigla(14, "INT", "Internacional", "BR_B", ""));
            listTime.Add(new Sigla(15, "BEC", "Boa Esporte Clube", "BR_B", ""));
            listTime.Add(new Sigla(16, "VIL", "Vila Nova", "BR_B", ""));
            listTime.Add(new Sigla(17, "CRB", "CRB", "BR_B", ""));
            listTime.Add(new Sigla(18, "PAY", "Paysandu", "BR_B", ""));
            listTime.Add(new Sigla(19, "OES", "Oeste", "BR_B", ""));
            listTime.Add(new Sigla(20, "CEA", "Ceará", "BR_B", ""));


        }

    }
}