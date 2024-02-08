using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCTO_INFO2021
{
    public class Entità
    {
        /// <summary>
        /// Costruttore entità
        /// </summary>
        /// <param name="nome">Nome entità</param>
        /// <param name="descrizione">Descrizione generale entità</param>
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

        public override string ToString()
        {
            return Nome;
        }
    }

    /// <summary>
    /// Entità che il player sarà in grado di raccogliere/utilizzare
    /// </summary>
    public abstract class EntitàOggetto : Entità 
    {
        private int _idElemento = 0;

        public EntitàOggetto(string nome, string descrizione) : base(nome, descrizione){}

        public abstract int GetIDElemento();
    }

    /// <summary>
    /// Oggetto consumabile
    /// </summary>
    public class EntitàConsumabile : EntitàOggetto
    {
        /// <summary>
        /// Costruttore consumabile
        /// </summary>
        /// <param name="nome">Nome oggetto</param>
        /// <param name="descrizione">Descrizione oggetto</param>
        /// <param name="peso">peso oggetto</param>
        /// <param name="descrizioneUtilizzo">Descrizione utilizzo</param>
        public EntitàConsumabile(string nome, string descrizione, int peso, string descrizioneUtilizzo) : base(nome, descrizione)
        {
            DescrizioneUtilizzo = descrizioneUtilizzo;
            Peso = peso;
        }

        /// <summary>
        /// Descrizione dell'utilizzo dell'oggetto
        /// </summary>
        public string DescrizioneUtilizzo { get; set; }

        public int Peso { get; set; }

        public override int GetIDElemento() { return 0; }
    }

    /// <summary>
    /// Oggetto consumabile curativo
    /// </summary>
    public class Cura : EntitàConsumabile
    {
        /// <summary>
        /// Costruttore cura
        /// </summary>
        /// <param name="nome">Nome oggetto</param>
        /// <param name="descrizione">Descrizione oggetto</param>
        /// <param name="peso">Peso oggetto</param>
        /// <param name="descrizioneUtilizzo">Descrizione utilizzo</param>
        /// <param name="puntiCura">Punti vita guagìdagnati</param>
        public Cura(string nome, string descrizione, int peso, string descrizioneUtilizzo, int puntiCura) : base(nome, descrizione, peso, descrizioneUtilizzo)
        {
            PuntiCura = puntiCura;
        }

        public int PuntiCura { get; set; }

        public override int GetIDElemento()
        {
            return 1;
        }
    }

    /// <summary>
    /// Oggetto danneggiante
    /// </summary>
    public class Danno : EntitàConsumabile
    {
        public Danno(string nome, string descrizione, int peso, string descrizioneUtilizzo, int puntiDanno) : base(nome, descrizione, peso, descrizioneUtilizzo)
        {
            PuntiDanno = puntiDanno;
        }
            
        public int PuntiDanno { get; set; }

        public override int GetIDElemento()
        {
            return 2;
        }
    }

    /// <summary>
    /// Entità ewuipaggiabile dal player
    /// </summary>
    public abstract class EntitàEquipaggiabile : EntitàOggetto
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

        public override int GetIDElemento()
        {
            return 0;
        }
    }

    /// <summary>
    /// Arma equipaggiabile
    /// </summary>
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

        public override int GetIDElemento()
        {
            return 3;
        }
    }

    /// <summary>
    /// Armatura equipaggiabile
    /// </summary>
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

        public override int GetIDElemento()
        {
            return 4;
        }
    }

    /// <summary>
    /// Player
    /// </summary>
    public class Player : Entità
    {
        /// <summary>
        /// Costruttore player
        /// </summary>
        /// <param name="nome">Nome giocatore</param>
        /// <param name="descrizione">Descrizione giocatore</param>
        /// <param name="hp">Punti vita</param>
        /// <param name="def">Difesa</param>
        /// <param name="inventario">Inventario</param>
        public Player(string nome,string descrizione, int hp, int def, List<EntitàOggetto> inventario) : base(nome, descrizione)
        {
            HP = hp;
            Inventario = inventario;
            DEF = def;
        }

        public int HP { get; set; }

        public int DEF { get; set; }

        public List<EntitàOggetto> Inventario { get; set; }
    }

    /// <summary>
    /// Nemico
    /// </summary>
    public class NPC : Entità
    {
        /// <summary>
        /// Costruttore nemici
        /// </summary>
        /// <param name="nome">Nome nemico</param>
        /// <param name="descrizione">Descrizione nemico</param>
        /// <param name="hp">Punti vita</param>
        /// <param name="dmg">Danno</param>
        /// <param name="reward">Ricompensa</param>
        public NPC(string nome, string descrizione, int hp, int dmg, EntitàOggetto reward) : base(nome, descrizione)
        {
            HP = hp;
            DMG = dmg;
            Reward = reward;
        }

        public int HP { get; set; }

        public int DMG { get; set; }

        public EntitàOggetto Reward { get; set; }
    } 
}