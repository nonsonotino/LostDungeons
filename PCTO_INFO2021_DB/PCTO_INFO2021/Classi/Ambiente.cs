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
        public Ambiente(string titolo, string descrizione, Passaggio[] passaggi)//Lista di Entità
        {
            Titolo = titolo;
            Descrizione = descrizione;
            Passaggi = passaggi;
        }

        /// <summary>
        /// Titolo dell'ambiente
        /// </summary>
        public string Titolo { get; set; }

        /// <summary>
        /// Descrizione della zona
        /// </summary>
        public string Descrizione { get; set; }

        /// <summary>
        /// Passaggi verso altre zone
        /// </summary>
        public Passaggio[] Passaggi { get; set; }

        public override string ToString()
        {
            string output = Titolo + ":\n\n" + Descrizione+"\n\n";

            for (int i = 0; i < 4; i++) 
            {
                if(Passaggi[i] != null)
                    output += Passaggi[i].Titolo + " = " + Passaggi[i].Descrizione + "\n\n";
            }

            return output;
        }
    }

    public class Passaggio
    {
        /// <summary>
        /// Crea passaggio.
        /// </summary>
        /// <param name="titolo">Titolo</param>
        /// <param name="descrizione">Descrizione</param>
        /// <param name="riferimento">Riferimento</param>
        public Passaggio(string titolo, string descrizione, int riferimento)
        {
            Titolo = titolo;
            Descrizione = descrizione;
            Riferimento = riferimento;
        }

        public string Titolo { get; set; }

        public string Descrizione { get; set; }

        public int Riferimento { get; set; }
    }

    enum Direzione
    {
        Nord,
        Sud,
        Ovest,
        Est
    }
}