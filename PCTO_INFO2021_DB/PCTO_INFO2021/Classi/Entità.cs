using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCTO_INFO2021
{
    public class Entità
    {
        public Entità(string nome, string descrizione)
        {
            Nome = nome;
            Descrizione = descrizione;
        }

        /// <summary>
        /// Nome dell'entità
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrizione dell'entità
        /// </summary>
        public string Descrizione { get; set; }
    }

    public class EntitàEquipaggiabile : Entità
    {
        public EntitàEquipaggiabile(string nome, string descrizione, int peso, int durabilità) : base(nome, descrizione)
        {
            Peso = peso;
            Durabilità = durabilità;
            Equipaggiata = false;//Quando viene creato l'oggetto non è mai equipaggiato !!Possibilità oggetti di partenza
        }

        /// <summary>
        /// Durabilità oggetto
        /// </summary>
        public int Durabilità { get; set; }

        /// <summary>
        /// Peso nell'inventario
        /// </summary>
        public int Peso { get; set; }

        /// <summary>
        /// True->Entità equipaggiata, False->Entità non equipaggiata
        /// </summary>
        public bool Equipaggiata { get; set; }
    }

    public class Arma : EntitàEquipaggiabile
    {
        public Arma(string nome, string descrizione, int peso, int durabilità, int danno) : base(nome, descrizione, peso, durabilità)
        {
            Danno = danno;
        }

        /// <summary>
        /// Danno dell'arma
        /// </summary>
        public int Danno { get; set; }
    }

    public class Armatura : EntitàEquipaggiabile
    {
        public Armatura(string nome, string descrizione, int peso, int durabilità, int protezione) : base(nome, descrizione, peso, durabilità)
        {
            Protezione = protezione;
        }

        /// <summary>
        /// Statistica di protezione dell'armatura
        /// </summary>
        public int Protezione { get; set; }
    }
}