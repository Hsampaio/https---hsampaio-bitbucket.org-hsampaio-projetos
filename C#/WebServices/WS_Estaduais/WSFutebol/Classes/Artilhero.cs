using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSFutebol.Classes
{
    public class Artilhero
    {
        private int codigo;
        private string nome;
        private int qtdGols;
        private string siglaTime;

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

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public int QtdGols
        {
            get
            {
                return qtdGols;
            }

            set
            {
                qtdGols = value;
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
    }
}