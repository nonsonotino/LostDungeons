using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCTO_INFO2021
{
    public class Ambiente
    {
        private string _titolo = "";
        private string _descrizione = "";
        private Passaggio[] _passaggi = new Passaggio[4];

        //Costruttore
        Ambiente(string titolo, string descrizione, Passaggio[] passaggi)//Lista di Entità
        {
            _titolo = titolo;
            _passaggi = passaggi;
        }

        /// <summary>
        /// Titolo dell'ambiente
        /// </summary>
        public string Titolo
        {
            get
            {
                return _titolo;
            }

            set
            {
                _titolo = Titolo;
            }
        }

        /// <summary>
        /// Descrizione della zona
        /// </summary>
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

        /// <summary>
        /// Passaggi verso altre zone
        /// </summary>
        public Passaggio[] Passaggi
        {
            get
            {
                return _passaggi;
            }
        }
    }

    public class Passaggio
    {
        public Passaggio(string titolo, string descrizione, int riferimento)
        {
            Titolo = titolo;
        }
    }
}