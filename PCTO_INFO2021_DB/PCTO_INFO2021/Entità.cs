using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCTO_INFO2021
{
    public class Entità
    {
        private string _nome = "";
        private string _descrizione = "";

        Entità(string nome, string descrizione) 
        {
            Nome = nome;
            Descrizione = descrizione;
        }


        public string Nome
        {
            get
            {
                return _nome;
            }

            set
            {
                _nome = value;
            }
        }

        public string Descrizione
        {
            get
            {
                return _descrizione;
            }

            set
            {
                _descrizione = value;
            }
        }
    }

    public class EntitàStatica : Entità
    {
        EntitàStatica(string nome, string descrizione,Ambiente posizione):
        {

        }
    }
}