using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace PCTO_INFO2021
{

    //
    // Sartini Matteo
    // Classe: 5aF
    // Progetto PCTO implementato con salvataggio su DB secondo Elaborato per Esame di Stato
    //
    // Per connessione a SSMS: stringa di connessione a riga 28.
    //

    public partial class Default : System.Web.UI.Page
    {
        //Campi di classe per variabili ricorrenti
        Ambiente[] mappa;
        Ambiente tempA;
        Player tempP;

        //Stringa per la connessione al DB, MultipleActiveResultSets=True necessario per il funzionamento del programma.
        const string CONNECTION = @"Data Source = PC-STUDIO\SQLEXPRESS; Initial Catalog = LostDungeons; Integrated Security=True; MultipleActiveResultSets=True;";
        //Stringa di connessione CASA: @"Data Source = PC-STUDIO\SQLEXPRESS; Initial Catalog = LostDungeons; Integrated Security=True; MultipleActiveResultSets=True;"
        //Stringa di connessione PORTATILE: @"Data Source = PORTATILE\SQLEXPRESS; Initial Catalog = LostDungeons; Integrated Security=True;"
        //Stringa di connessione SCUOLA: @"Data Source = PC1310; Initial Catalog = LostDungeons; User Id = sa; Password = burbero2020"


        //Variabili di riferimento per i nemici
        NPC topo = new NPC("Topo", "Un topo malnutrito e debole, che si rende ostile non appena entri nella stanza", 2, 1, new Cura("Pozione di cura", "Una pozione per curare 10HP", 1, "Bevi la pozione e ti senti rinvigorito, guadagni 10HP", 10));
        NPC guardia = new NPC("Guardia tombale", "Un semplice guerriero sottoposto al Re del Sottosuolo", 5, 2, new Arma("Lancia", "Un'arma capace di infliggere 3 danni", 1, 100, 3));
        NPC guardiano = new NPC("Il Guardiano della Forgia", "Il guariano della Spada Finale!", 10, 3, new Arma("La Spada Finale", "Una spada capace di distruggere qualsiasi essere vivente con cui venga a contatto!", 3, 100, 100));
        NPC boss = new NPC("Il Re del Sottosuolo", "Monarca del sottosuolo e boss del dungeon.", 15, 5, new Cura("Pozione della resurrezione", "Una pozione in grado di riportare al mondo coloro che hanno perso la vita", 1, "Una luce divina ti avvolge, ti senti VIVO!", 10000));

        protected void Page_Load(object sender, EventArgs e)
        {
            //Primo ciclo del programma
            if (Session["firstTime"] == null)
            {
                //ResetDB();

                //Creazione del player
                Player player = new Player("Player", "Il protagonista della storia", 20, 0, new List<EntitàOggetto> { new Arma("Coltello", "Unsemplice coltello da caccia", 1, 100, 1) });
                Ambiente location = null;
                    
                //Introduzione
                txtOut.Text = "Benvenuto avventuriero, sei stato inviato qui nella reggia del Re del Sottosuolo per recuperare una pozione magica in grado di guarire i morti, sembra che sia il più prezioso possediemento del monarca.\nSembra inoltre che sia custodito anche un artefatto di spaventosa potenza da qualche parte.\nBuona fortuna, te ne servirà!\n\n";

                //Download dati dal DB
                Ambiente[] mappa = DownloadFromDB(ref player, ref location);

                //Label per HP
                lblVita.Text = "Vita: " + player.HP;

                //Salvataggio delle mappa nei vari session
                Session["Mappa"] = mappa;
                Session["Location"] = location;
                Session["Player"] = player;

                //Metodi di oggiornamento della componente grafica
                AggiungiContenuto();
                AggiornaAzioni();

                //Scrittura descrizione della stanza attuale
                txtOut.Text += Session["Location"].ToString();

                //Preparazione movimento
                btnN.Enabled = true;
                btnS.Enabled = true;
                btnO.Enabled = true;
                btnE.Enabled = true;

                //Fine del primo ciclo
                Session["firstTime"] = false;

                //Variabile per riconoscere la fine del gioco
                Session["GameOver"] = false;
            }
        }

        #region Bottoni direzionali

        protected void btnN_Click(object sender, EventArgs e)
        {
            //Assegnazione della variabili temporanee
            tempA = (Ambiente)Session["Location"];
            mappa = (Ambiente[])Session["Mappa"];

            //Scrittura passaggi
            if (tempA.Passaggi[0] == null)
            {
                txtOut.Text += "\n\nNon esiste un passaggio in questa direzione.";
            }
            else
            {
                Session["Location"] = mappa[tempA.Passaggi[0].Riferimento];
                txtOut.Text = Session["Location"].ToString();
            }

            //Aggiunta elementi
            AggiungiContenuto();
            CheckCombattimento();
        }

        protected void btnS_Click(object sender, EventArgs e)
        {
            //Assegnazione della variabili temporanee
            tempA = (Ambiente)Session["Location"];
            mappa = (Ambiente[])Session["Mappa"];

            //Scrittura passaggi
            if (tempA.Passaggi[1] == null)
            {
                txtOut.Text += "\n\nNon esiste un passaggio in questa direzione.";
            }
            else
            {
                Session["Location"] = mappa[tempA.Passaggi[1].Riferimento];
                txtOut.Text = Session["Location"].ToString();
            }

            //Aggiunta elementi
            AggiungiContenuto();
            CheckCombattimento();
        }

        protected void btnO_Click(object sender, EventArgs e)
        {
            //Assegnazione della variabili temporanee
            tempA = (Ambiente)Session["Location"];
            mappa = (Ambiente[])Session["Mappa"];

            //Scrittura passaggi
            if (tempA.Passaggi[2] == null)
            {
                txtOut.Text += "\n\nNon esiste un passaggio in questa direzione.";
            }
            else
            {
                Session["Location"] = mappa[tempA.Passaggi[2].Riferimento];
                txtOut.Text = Session["Location"].ToString();
            }

            //Aggiunta elementi
            AggiungiContenuto();
            CheckCombattimento();
        }

        protected void btnE_Click(object sender, EventArgs e)
        {
            //Assegnazione della variabili temporanee
            tempA = (Ambiente)Session["Location"];
            mappa = (Ambiente[])Session["Mappa"];

            //Scrittura passaggi
            if (tempA.Passaggi[3] == null)
            {
                txtOut.Text += "\n\nNon esiste un passaggio in questa direzione.";
            }
            else
            {
                Session["Location"] = mappa[tempA.Passaggi[3].Riferimento];
                txtOut.Text = Session["Location"].ToString();
            }

            //Aggiunta elementi
            AggiungiContenuto();
            CheckCombattimento();
        }

        #endregion

        protected void btnUsa_Click(object sender, EventArgs e)
        {
            //Variabili temporanee
            int oggetto = 0;
            int obiettivo = 0;
            tempP = (Player)Session["Player"];
            tempA = (Ambiente)Session["Location"];

            //Riconoscimento azione da compiere
            bool exit = false;
            for (int i = 0; i < tempP.Inventario.Count() && exit == false; i++)
            {
                if (tempP.Inventario[i].Nome == ddlAction.SelectedValue)
                {
                    oggetto = i;
                    exit = true;
                }
            }

            //Riconoscimento bersaglio dell'azione
            exit = false;
            for (int i = 0; i < tempA.Contenuto.Count() && exit == false; i++)
            {
                if (tempA.Contenuto[i].Nome == ddlTarget.SelectedValue)
                {
                    obiettivo = i;
                    exit = true;
                }
            }

            //Turno del giocatore
            switch (tempP.Inventario[oggetto].GetIDElemento())
            {
                case 1://Cura
                    Cura cura = (Cura)tempP.Inventario[oggetto];
                    tempP.HP += cura.PuntiCura;
                    txtOut.Text += cura.DescrizioneUtilizzo + "\n\n";
                    tempP.Inventario.RemoveAt(oggetto);
                    break;
                case 2://Danno
                    Danno danno = (Danno)tempP.Inventario[oggetto];
                    tempA.Contenuto[obiettivo].HP -= danno.PuntiDanno;
                    txtOut.Text += danno.DescrizioneUtilizzo + "\n\n";
                    tempP.Inventario.RemoveAt(oggetto);
                    break;
                case 3://Arma
                    Arma arma = (Arma)tempP.Inventario[oggetto];
                    tempA.Contenuto[obiettivo].HP -= arma.Danno;
                    break;
            }

            //Turno dei nemici
            if (tempA.Contenuto[obiettivo].HP <= 0)
            {
                //Evento morte nemico
                tempP.Inventario.Add(tempA.Contenuto[obiettivo].Reward);//Ottenimento reward
                txtOut.Text += tempA.Contenuto[obiettivo].Nome + " è stato sconfitto! Hai ottenuto:" + tempA.Contenuto[obiettivo].Reward.Nome + ", " + tempA.Contenuto[obiettivo].Reward.Descrizione + "\n\n";
                tempA.Contenuto.RemoveAt(obiettivo);//Rimozione nemico
            }
            else
            {
                //Resoconto attacco
                txtOut.Text += tempA.Contenuto[obiettivo].Nome + " ha ora: " + tempA.Contenuto[obiettivo].HP + " HP!\n\n";
            }

            //Attacco nemici
            for (int i = 0; i < tempA.Contenuto.Count(); i++)
            {
                tempP.HP -= tempA.Contenuto[i].DMG - tempP.DEF;
                txtOut.Text += tempA.Contenuto[i].Nome + " ti colpisce facendo " + (tempA.Contenuto[i].DMG - tempP.DEF) + " punti di danno!\n\n";
            }
            
            //Aggiornamento label vita
            lblVita.Text = "Vita: " + tempP.HP;

            //Evento di morte di tutti i nemici della stanza
            if (tempA.Contenuto.Count() == 0)
            {
                txtOut.Text += "\nHai sconfitto tutti i nemici!\n\n";
                tempA.Combattimento = false;

                //Condizione di vittoria
                if(tempA.Titolo=="Boss Room")
                {
                    txtOut.Text += "Hai sconfitto il boss, HAI VINTO! \n\nVuoi rigiocare?";
                    GameOver();
                }
            }

            //Divisore turni
            txtOut.Text += "-----------------------------------------------\n\n";

            //Evento morte giocatore
            if (tempP.HP <= 0)
            {
                txtOut.Text += "Game Over! Sei stato sconfitto, vuoi ricominciare?";
                GameOver();
            }
            else
                CheckCombattimento();

            //Aggiornamento delle opzioni
            AggiornaAzioni();
        }

        /// <summary>
        /// Aggiunta grafica degli elementi dell'inventario
        /// </summary>
        void AggiungiContenuto()
        {   
            //Assegnazione variabile temporanea 
            tempA = (Ambiente)Session["Location"];

            //Immagine stanza
            imgImage.ImageUrl = tempA.Image;

            //Se il la stanza contiene qualcosa
            if (tempA.Contenuto.Count > 0) 
                txtOut.Text += "\nIn questa stanza trovi: \n\n";
            //else
            //    txtOut.Text += "\nQuesta stanza è vuota.\n\n";

            //Pulizia bersagli
            ddlTarget.Items.Clear();

            //Aggiunta bersagli
            for (int i = 0; i < tempA.Contenuto.Count(); i++)
            {
                ddlTarget.Items.Add(tempA.Contenuto[i].ToString());
                txtOut.Text += tempA.Contenuto[i].Nome + ": " + tempA.Contenuto[i].Descrizione+"\n";
            }

            txtOut.Text += "\n\n";
        }

        /// <summary>
        /// Aggiornamento azioni personaggio
        /// </summary>
        void AggiornaAzioni()
        {
            //Assegnazione variabile temporanea 
            tempP = (Player)Session["Player"];

            //Aggiunta azioni
            ddlAction.Items.Clear();
            for (int i = 0; i < tempP.Inventario.Count(); i++)
            {
                ddlAction.Items.Add(tempP.Inventario[i].ToString());
            }
        }

        /// <summary>
        /// Gestione dei bottoni per il combattimento
        /// </summary>
        void CheckCombattimento()
        {
            //Assegnazione variabile temporanea 
            tempA = (Ambiente)Session["Location"];

            if (tempA.Combattimento == true)//Disabilitazione movimento
            {
                btnN.Enabled = false;
                btnS.Enabled = false;
                btnO.Enabled = false;
                btnE.Enabled = false;

                btnUsa.Enabled = true;
                ddlAction.Enabled = true;
                ddlTarget.Enabled = true;

                btnSalva.Enabled = false;
            }
            else if ((bool)Session["GameOver"] == false)//Abilitazione movimento
            {
                btnN.Enabled = true;
                btnS.Enabled = true;
                btnO.Enabled = true;
                btnE.Enabled = true;

                btnUsa.Enabled = false;
                ddlAction.Enabled = false;
                ddlTarget.Enabled = false;

                btnSalva.Enabled = true;
            }
        }

        /// <summary>
        /// Evento game over
        /// </summary>
        void GameOver()
        {
            btnN.Enabled = false;
            btnS.Enabled = false;
            btnO.Enabled = false;
            btnE.Enabled = false;

            btnUsa.Enabled = false;
            ddlAction.Enabled = false;
            ddlTarget.Enabled = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            //Reset del gioco
            Session["firstTime"] = null;

            ResetDB();

            CheckCombattimento();

            Page_Load(null, null);
        }

        private void ResetDB()
        {
            //Creazione del player
            Player player = new Player("Player", "Il protagonista della storia", 20, 0, new List<EntitàOggetto> { new Arma("Coltello", "Unsemplice coltello da caccia", 1, 100, 1) });
            Ambiente location = null;

            //Creazione del mondo di gioco
            Ambiente[] mappa = new Ambiente[] {
                new Ambiente("Corridoio principale", "Un corridoio vuoto con una botola sul soffitto, dalla quale sei entrato.",false, new Passaggio[] {
                    new Passaggio("Porta Nord(0)", "Una porta verso la stanza comune.", 5),
                    new Passaggio("Porta Sud(0)", "Una porta verso quello che sembra essere una dispensa.", 2), null, null }, new List<NPC>{ }, "~/Images/Room0.png"),
                new Ambiente("Dispensa", "Uno dispensa che contiene ben poco di valore.",true, new Passaggio[] {
                    new Passaggio("Porta Nord(1)", "Una porta verso il corridoio principale", 0), null, null, null }, new List<NPC>{ topo }, "~/Images/Room1.png"),
                new Ambiente("Zona comune", "Una stanza comune, con diverse porte verso varie parti del complesso.", true, new Passaggio[] {
                    new Passaggio("Porta Nord(2)", "Una porta verso la stanza del trono.", 4),
                    new Passaggio("Porta Sud(2)", "Una porta verso il corridoio principale.", 0),
                    new Passaggio("Porta Ovest(2)", "Una porta verso le cucine della reggia.", 1),
                    new Passaggio("Porta Est(2)", "Una porta porta che dà sulla forgia reale.", 3) },new List<NPC>{ guardia }, "~/Images/PreBossRoom.jpg"),
                new Ambiente("Cucina", "La cucina personale del Re, non trovi nulla di interessante se non cibo e pentole sporche.", false, new Passaggio[] { null, null, null,
                    new Passaggio("Porta Est(3)", "Una porta verso la zona comune.", 5) },new List<NPC>{ }, "~/Images/Room4.jpg"),
                new Ambiente("Stanza del trono", "La stanza più sfarzosa del complesso, e al centro di essa un trono dorato.", true ,new Passaggio[] { null,
                    new Passaggio("Porta Sud(4)", "Una porta verso la zona comune.", 5), null, null},new List<NPC>{ boss }, "~/Images/BossRoom.jpg"),
                new Ambiente("Forgia reale", "La forgia personale del re, molti dicono che armi leggendarie siano state forgiate in questo luogo", true, new Passaggio[] { null, null,
                    new Passaggio("Porta Ovest(5)", "Una porta verso la zona comune.", 5), null },new List<NPC>{ guardiano }, "~/Images/Room6.jpg")
            };
            location = mappa[0];

            //Salvataggio delle mappa nei vari session
            Session["Mappa"] = mappa;
            Session["Location"] = location;
            Session["Player"] = player;

            UploadData();
        }

        /// <summary>
        /// Metodo per lo scaricamento dei dati dal DB
        /// </summary>
        /// <param name="player">Dati del player</param>
        /// <param name="location">Dati dell'ambiente attuale</param>
        /// <returns></returns>
        Ambiente[] DownloadFromDB(ref Player player, ref Ambiente location)
        {
            try
            {
                //Inizializzazione connesione
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    //Apertura connessione
                    connection.Open();

                    //Download dati player
                    #region Player

                    //
                    //Estrazione dati Player
                    //

                    //Nome location
                    string nomeLocation = "";

                    //Creazione comando
                    SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Player", connection);

                    //Esecuzione comando
                    SqlDataReader p = sqlCmd.ExecuteReader();

                    //Lettura dati player
                    while (p.Read())
                    {
                        player.Nome = p[0].ToString().TrimEnd(null);
                        player.Descrizione = p[1].ToString().TrimEnd(null);
                        player.HP = (int)p[2];
                        player.DEF = (int)p[3];

                        //Nome della location
                        nomeLocation = p[4].ToString().TrimEnd(null);
                    }

                    //Fine lettura
                    p.Close();

                    //
                    //Estrazione inventario
                    //

                    //Variabile temporanea per inventario
                    List<EntitàOggetto> inventario = new List<EntitàOggetto> { };

                    //Creazione comando
                    sqlCmd = new SqlCommand(@"SELECT O.Nome, O.Descrizione, O.Peso, O.Tipo, O.DescrizioneUtilizzo, O.PuntiDanno, O.PuntiCura, O.Durabilità, O.Danno, O.Protezione FROM Player AS P INNER JOIN Oggetto AS O ON P.Nome = O.PlayerOwner", connection);

                    //Esecuzione comando
                    p = sqlCmd.ExecuteReader();

                    //Lettura oggetti nell'inventario
                    while (p.Read())
                    {
                        //Tipo di oggetto
                        string tipo = p[3].ToString().TrimEnd(null);

                        //Inserimento oggetto nell'inventario
                        switch (tipo)
                        {
                            case "Danno":
                                inventario.Add(new Danno(p[0].ToString().TrimEnd(null), p[1].ToString().TrimEnd(null), (int)p[2], p[4].ToString().TrimEnd(null), (int)p[5]));
                                break;
                            case "Cura":
                                inventario.Add(new Cura(p[0].ToString().TrimEnd(null), p[1].ToString().TrimEnd(null), (int)p[2], p[4].ToString().TrimEnd(null), (int)p[6]));
                                break;
                            case "Arma":
                                inventario.Add(new Arma(p[0].ToString().TrimEnd(null), p[1].ToString().TrimEnd(null), (int)p[2], (int)p[7], (int)p[8]));
                                break;
                            case "Armatura":
                                inventario.Add(new Armatura(p[0].ToString().TrimEnd(null), p[1].ToString().TrimEnd(null), (int)p[2], (int)p[7], (int)p[9]));
                                break;
                        }
                    }

                    //Fine lettura
                    p.Close();

                    //Inserimento inventario
                    player.Inventario = inventario;

                    #endregion

                    //Download dati mappa
                    #region Mappa

                    //
                    //Download mappa di gioco
                    //

                    //Variabile temporanea per mappa
                    List<Ambiente> mappa = new List<Ambiente> { };

                    //Creazione comando
                    sqlCmd = new SqlCommand("SELECT * FROM Ambiente", connection);

                    //Esecuzione comando
                    p = sqlCmd.ExecuteReader();

                    //Lettura ambienti
                    while (p.Read())
                    {
                        //
                        //Individuazione passaggi
                        //

                        //Variabile temporanea per passaggi
                        Passaggio[] passaggio = new Passaggio[4];

                        //Creazione comando
                        SqlCommand subCmd = new SqlCommand(@"SELECT P.Nome, P.Direzione, P.Riferimento, P.Descrizione FROM  Passaggio AS P WHERE P.Ambiente = '" + p[0].ToString().TrimEnd(null) + "' ORDER BY P.Direzione", connection);

                        SqlDataReader sr = subCmd.ExecuteReader();

                        //Individuazione passaggi
                        while (sr.Read())
                        {
                            //0-Nord, 1-Sud, 3-Ovest, 4-Est
                            int direzione = (int)sr[1];
                            int riferimento = (int)sr[2];

                            if (riferimento != -1)
                            {
                                //Direzione
                                switch (direzione)
                                {
                                    case 0://Nord
                                        passaggio[0] = new Passaggio(sr[0].ToString().TrimEnd(null), sr[3].ToString().TrimEnd(null), (int)sr[2]);
                                        break;
                                    case 1://Sud
                                        passaggio[1] = new Passaggio(sr[0].ToString().TrimEnd(null), sr[3].ToString().TrimEnd(null), (int)sr[2]);
                                        break;
                                    case 2://Ovest
                                        passaggio[2] = new Passaggio(sr[0].ToString().TrimEnd(null), sr[3].ToString().TrimEnd(null), (int)sr[2]);
                                        break;
                                    case 3://Est
                                        passaggio[3] = new Passaggio(sr[0].ToString().TrimEnd(null), sr[3].ToString().TrimEnd(null), (int)sr[2]);
                                        break;
                                }
                            }
                            else
                            {
                                //Direzione
                                switch (direzione)
                                {
                                    case 0://Nord
                                        passaggio[0] = null;
                                        break;
                                    case 1://Sud
                                        passaggio[1] = null;
                                        break;
                                    case 2://Ovest
                                        passaggio[2] = null;
                                        break;
                                    case 3://Est
                                        passaggio[3] = null;
                                        break;
                                }
                            }
                        }

                        //Fine lettura
                        sr.Close();

                        //
                        //Individuazione NPC
                        //

                        //Variabili temporanee
                        List<NPC> npc = new List<NPC> { };
                        EntitàOggetto reward = null;

                        //Creazione comando
                        subCmd = new SqlCommand(@"SELECT N.Nome, N.Descrizione, N.HP, N.DMG FROM NPC AS N WHERE N.Ambiente = '" + p[0].ToString().TrimEnd(null) + "'", connection);

                        //Esecuzione comando
                        sr = subCmd.ExecuteReader();

                        //Lettura NPC
                        while (sr.Read())
                        {
                            //
                            //Individuazione Reward NPC
                            //

                            //Creazione comando
                            SqlCommand selRew = new SqlCommand(@"SELECT O.Nome, O.Descrizione, O.Peso, O.Tipo, O.DescrizioneUtilizzo, O.PuntiDanno, O.PuntiCura, O.Durabilità, O.Danno, O.Protezione FROM Oggetto AS O WHERE O.NPCOwner = '" + sr[0].ToString().TrimEnd(null) + "'", connection);

                            //Esecuzione comando
                            SqlDataReader rewReader = selRew.ExecuteReader();

                            //Lettura reward
                            while (rewReader.Read())
                            {
                                //Tipo di oggetto
                                string tipo = rewReader[3].ToString().TrimEnd(null);

                                //Inserimento oggetto nell'inventario
                                switch (tipo)
                                {
                                    case "Danno":
                                        reward = new Danno(rewReader[0].ToString().TrimEnd(null), rewReader[1].ToString().TrimEnd(null), (int)rewReader[2], rewReader[4].ToString().TrimEnd(null), (int)rewReader[5]);
                                        break;
                                    case "Cura":
                                        reward = new Cura(rewReader[0].ToString().TrimEnd(null), rewReader[1].ToString().TrimEnd(null), (int)rewReader[2], rewReader[4].ToString().TrimEnd(null), (int)rewReader[6]);
                                        break;
                                    case "Arma":
                                        reward = new Arma(rewReader[0].ToString().TrimEnd(null), rewReader[1].ToString().TrimEnd(null), (int)rewReader[2], (int)rewReader[7], (int)rewReader[8]);
                                        break;
                                    case "Armatura":
                                        reward = new Armatura(rewReader[0].ToString().TrimEnd(null), rewReader[1].ToString().TrimEnd(null), (int)rewReader[2], (int)rewReader[7], (int)rewReader[9]);
                                        break;
                                }
                            }

                            //Aggiunta nuovo NPC alla lista
                            npc.Add(new NPC(sr[0].ToString().TrimEnd(null), sr[1].ToString().TrimEnd(null), (int)sr[2], (int)sr[3], reward));
                        }

                        //Check combattimento
                        bool combat = (bool)p[1];

                        if (p[0].ToString().TrimEnd(null) == nomeLocation)
                        {
                            location = new Ambiente(p[0].ToString().TrimEnd(null), p[2].ToString().TrimEnd(null), combat, passaggio, npc, p[3].ToString().TrimEnd(null));
                        }

                        //Aggiunta nuovo ambente alla mappa
                        mappa.Add(new Ambiente(p[0].ToString().TrimEnd(null), p[2].ToString().TrimEnd(null), combat, passaggio, npc , p[3].ToString().TrimEnd(null)));
                    }

                    //Fine lettura
                    p.Close();

                    #endregion

                    //Restituzione mappa
                    return mappa.ToArray();
                }
            }
            catch(Exception ex)
            {
                lblDBError.Text = "Errore nel download dei dati: " + ex;
                return null;
            }
        }

        /// <summary>
        /// Metodo per il salvataggio nel DB dei dati
        /// </summary>
        void UploadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    //Apertura connessione
                    connection.Open();

                    //Variabili per comandi SQL
                    SqlCommand insertCmd;
                    SqlCommand delCmd;

                    //
                    //Eliminazione dati precedenti
                    //

                    //Eliminazione oggetti
                    delCmd = new SqlCommand(@"DELETE FROM Oggetto", connection);

                    delCmd.ExecuteNonQuery();

                    //Eliminazione Ambienti
                    delCmd = new SqlCommand(@"DELETE FROM Ambiente", connection);

                    delCmd.ExecuteNonQuery();

                    //Eliminazione Passaggi
                    delCmd= new SqlCommand(@"DELETE FROM Passaggio", connection);

                    delCmd.ExecuteNonQuery();

                    //Eliminazione NPC
                    delCmd = new SqlCommand(@"DELETE FROM NPC", connection);

                    delCmd.ExecuteNonQuery();

                    //Eliminazione dati player
                    delCmd = new SqlCommand(@"DELETE FROM Player", connection);

                    delCmd.ExecuteNonQuery();


                    //
                    //Upload dati ambienti
                    //

                    Ambiente[] mappa = (Ambiente[])Session["Mappa"];
                    string tipo;
                    int i = 0;

                    foreach (Ambiente ambiente in mappa)
                    {
                        //Varabili temporanee
                        int combattimento = 0;
                        string titoloAmbiente = ambiente.Titolo;

                        //Creazione comando
                        insertCmd = new SqlCommand(@"INSERT INTO Ambiente VALUES (@Titolo, @Combattimento, @Descrizione, @Immagine)", connection);

                        //Convesione "Combattimento" in bit
                        if (ambiente.Combattimento)
                        {
                            combattimento = 1;
                        }

                        //Inserimento dei valori
                        insertCmd.Parameters.AddWithValue("@Titolo", titoloAmbiente);
                        insertCmd.Parameters.AddWithValue("@Combattimento", combattimento);
                        insertCmd.Parameters.AddWithValue("@Descrizione", ambiente.Descrizione);
                        insertCmd.Parameters.AddWithValue("@Immagine", ambiente.Image);

                        //Esecuzione comando
                        insertCmd.ExecuteNonQuery();


                        //
                        //Upload dati passaggi
                        //

                        int c = 0;//Contatore per direzione

                        //Upload
                        foreach (Passaggio passaggio in ambiente.Passaggi)//Controllare in caso null
                        {
                            //Varibili temporanee
                            string nome = "";

                            //Inizializzazione comando
                            insertCmd = new SqlCommand(@"INSERT INTO Passaggio VALUES (@Nome, @Direzione, @Riferimento, @Descrizione, @Ambiente)", connection);

                            if (passaggio is null)
                            {
                                switch (c)
                                {
                                    case 0:
                                        nome = "Porta Nord(" + i + ")";
                                        break;
                                    case 1:
                                        nome = "Porta Sud(" + i + ")";
                                        break;
                                    case 2:
                                        nome = "Porta Ovest(" + i + ")";
                                        break;
                                    case 3:
                                        nome = "Porta Est(" + i + ")";
                                        break;
                                }

                                //Inserimento dati
                                insertCmd.Parameters.AddWithValue("@Nome", nome);
                                insertCmd.Parameters.AddWithValue("@Direzione", c);
                                insertCmd.Parameters.AddWithValue("@Riferimento", -1);
                                insertCmd.Parameters.AddWithValue("@Descrizione", "");
                                insertCmd.Parameters.AddWithValue("@Ambiente", titoloAmbiente);

                            }
                            else
                            {
                                switch (c)
                                {
                                    case 0:
                                        nome = "Porta Nord(" + i + ")";
                                        break;
                                    case 1:
                                        nome = "Porta Sud(" + i + ")";
                                        break;
                                    case 2:
                                        nome = "Porta Ovest(" + i + ")";
                                        break;
                                    case 3:
                                        nome = "Porta Est(" + i + ")";
                                        break;
                                }

                                //Inserimento dati
                                insertCmd.Parameters.AddWithValue("@Nome", nome);
                                insertCmd.Parameters.AddWithValue("@Direzione", c);
                                insertCmd.Parameters.AddWithValue("@Riferimento", passaggio.Riferimento);
                                insertCmd.Parameters.AddWithValue("@Descrizione", passaggio.Descrizione);
                                insertCmd.Parameters.AddWithValue("@Ambiente", titoloAmbiente);
                            }

                            //Esecuzione comando
                            insertCmd.ExecuteNonQuery();

                            c++;
                        }


                        //
                        //Upload dei dati NPC
                        //

                        foreach (NPC npc in ambiente.Contenuto)
                        {
                            //Upload dati npc

                            //Inizializzazione comando
                            insertCmd = new SqlCommand(@"INSERT INTO NPC VALUES (@Nome, @Descrizione, @DMG, @HP, @Ambiente)", connection);

                            //Inserimento dati
                            insertCmd.Parameters.AddWithValue("@Nome", npc.Nome);
                            insertCmd.Parameters.AddWithValue("@Descrizione", npc.Descrizione);
                            insertCmd.Parameters.AddWithValue("@DMG", npc.DMG);
                            insertCmd.Parameters.AddWithValue("@HP", npc.HP);
                            insertCmd.Parameters.AddWithValue("@Ambiente", titoloAmbiente);

                            //Esecuzione comando
                            insertCmd.ExecuteNonQuery();


                            //Upload dati reward npc
                            switch (npc.Reward.GetIDElemento())
                            {
                                case 1://Cura
                                    //Variabili temporanee
                                    Cura cura = (Cura)npc.Reward;
                                    tipo = "Cura";

                                    //Inizializzazione comando
                                    insertCmd = new SqlCommand(@"INSERT INTO Oggetto (Nome, Descrizione, Peso, Tipo, DescrizioneUtilizzo, PuntiCura, NPCOwner) VALUES (@Nome, @Descrizione, @Peso, @Tipo, @DescrizioneUtilizzo, @PuntiCura, @NPCOwner)", connection);

                                    //Inserimento dati
                                    insertCmd.Parameters.AddWithValue("@Nome", cura.Nome);
                                    insertCmd.Parameters.AddWithValue("@Descrizione", cura.Descrizione);
                                    insertCmd.Parameters.AddWithValue("@Peso", cura.Peso);
                                    insertCmd.Parameters.AddWithValue("@Tipo", tipo);
                                    insertCmd.Parameters.AddWithValue("@DescrizioneUtilizzo", cura.DescrizioneUtilizzo);
                                    insertCmd.Parameters.AddWithValue("@PuntiCura", cura.PuntiCura);
                                    insertCmd.Parameters.AddWithValue("@NPCOwner", npc.Nome);
                                    break;
                                case 2://Danno
                                    //Variabili temporanee
                                    Danno danno = (Danno)npc.Reward;
                                    tipo = "Danno";

                                    //Inizializzazione comando
                                    insertCmd = new SqlCommand(@"INSERT INTO Oggetto (Nome, Descrizione, Peso, Tipo, DescrizioneUtilizzo, PuntiDanno, NPCOwner) VALUES (@Nome, @Descrizione, @Peso, @Tipo, @DescrizioneUtilizzo, @PuntiDanno, @NPCOwner)", connection);

                                    //Inserimento dati
                                    insertCmd.Parameters.AddWithValue("@Nome", danno.Nome);
                                    insertCmd.Parameters.AddWithValue("@Descrizione", danno.Descrizione);
                                    insertCmd.Parameters.AddWithValue("@Peso", danno.Peso);
                                    insertCmd.Parameters.AddWithValue("@Tipo", tipo);
                                    insertCmd.Parameters.AddWithValue("@DescrizioneUtilizzo", danno.DescrizioneUtilizzo);
                                    insertCmd.Parameters.AddWithValue("@PuntiCura", danno.PuntiDanno);
                                    insertCmd.Parameters.AddWithValue("@NPCOwner", npc.Nome);
                                    break;
                                case 3://Arma
                                    //Variabili temporanee
                                    Arma arma = (Arma)npc.Reward;
                                    tipo = "Arma";

                                    //Inizializzazione comando
                                    insertCmd = new SqlCommand(@"INSERT INTO Oggetto (Nome, Descrizione, Tipo, Peso, Durabilità, Danno, NPCOwner) VALUES (@Nome, @Descrizione, @Tipo, @Peso, @Durabilità, @Danno, @NPCOwner)", connection);

                                    //Inserimento dati
                                    insertCmd.Parameters.AddWithValue("@Nome", arma.Nome);
                                    insertCmd.Parameters.AddWithValue("@Descrizione", arma.Descrizione);
                                    insertCmd.Parameters.AddWithValue("@Tipo", tipo);
                                    insertCmd.Parameters.AddWithValue("@Peso", arma.Peso);
                                    insertCmd.Parameters.AddWithValue("@Durabilità", arma.Durabilità);
                                    insertCmd.Parameters.AddWithValue("@Danno", arma.Danno);
                                    insertCmd.Parameters.AddWithValue("@NPCOwner", npc.Nome);
                                    break;
                            }

                            //Esecuzione comando
                            insertCmd.ExecuteNonQuery();

                        }

                        i++;
                    }

                    //
                    //Upload dati player e location
                    //

                    insertCmd = new SqlCommand(@"INSERT INTO Player VALUES (@Nome, @Descrizione, @HP, @DEF, @Ambiente)", connection);

                    Player player = (Player)Session["Player"];
                    Ambiente location = (Ambiente)Session["Location"];

                    insertCmd.Parameters.AddWithValue("@Nome", player.Nome);
                    insertCmd.Parameters.AddWithValue("@Descrizione", player.Descrizione);
                    insertCmd.Parameters.AddWithValue("@HP", player.HP);
                    insertCmd.Parameters.AddWithValue("@DEF", player.DEF);
                    insertCmd.Parameters.AddWithValue("@Ambiente", location.Titolo);

                    insertCmd.ExecuteNonQuery();

                    //Upload dati inventario

                    foreach(EntitàOggetto oggetto in player.Inventario)
                    {
                        if(oggetto is Cura)//Caso cura
                        {
                            //Variabili temporanee
                            Cura cura = (Cura)oggetto;
                            tipo = "Cura";

                            //Inizializzazione comando
                            insertCmd = new SqlCommand(@"INSERT INTO Oggetto (Nome, Descrizione, Peso, Tipo, DescrizioneUtilizzo, PuntiCura, PlayerOwner) VALUES (@Nome, @Descrizione, @Peso, @Tipo, @DescrizioneUtilizzo, @PuntiCura, @PlayerOwner)", connection);

                            //Inserimento dati
                            insertCmd.Parameters.AddWithValue("@Nome", cura.Nome);
                            insertCmd.Parameters.AddWithValue("@Descrizione", cura.Descrizione);
                            insertCmd.Parameters.AddWithValue("@Peso", cura.Peso);
                            insertCmd.Parameters.AddWithValue("@Tipo", tipo);
                            insertCmd.Parameters.AddWithValue("@DescrizioneUtilizzo", cura.DescrizioneUtilizzo);
                            insertCmd.Parameters.AddWithValue("@PuntiCura", cura.PuntiCura);
                            insertCmd.Parameters.AddWithValue("@PlayerOwner", player.Nome);
                        }
                        else if(oggetto is Danno)
                        {
                            //Variabili temporanee
                            Danno danno = (Danno)oggetto;
                            tipo = "Danno";

                            //Inizializzazione comando
                            insertCmd = new SqlCommand(@"INSERT INTO Oggetto (Nome, Descrizione, Peso, Tipo, DescrizioneUtilizzo, PuntiDanno, PlayerOwner) VALUES (@Nome, @Descrizione, @Peso, @Tipo, @DescrizioneUtilizzo, @PuntiDanno, @PlayerOwner)", connection);

                            //Inserimento dati
                            insertCmd.Parameters.AddWithValue("@Nome", danno.Nome);
                            insertCmd.Parameters.AddWithValue("@Descrizione", danno.Descrizione);
                            insertCmd.Parameters.AddWithValue("@Peso", danno.Peso);
                            insertCmd.Parameters.AddWithValue("@Tipo", tipo);
                            insertCmd.Parameters.AddWithValue("@Descrizioneutilizzo", danno.DescrizioneUtilizzo);
                            insertCmd.Parameters.AddWithValue("@PuntiCura", danno.PuntiDanno);
                            insertCmd.Parameters.AddWithValue("@PlayerOwner", player.Nome);
                        }
                        else if(oggetto is Arma)
                        {
                            //Variabili temporanee
                            Arma arma = (Arma)oggetto;
                            tipo = "Arma";

                            //Inizializzazione comando
                            insertCmd = new SqlCommand(@"INSERT INTO Oggetto (Nome, Descrizione, Tipo, Peso, Durabilità, Danno, PlayerOwner) VALUES (@Nome, @Descrizione, @Tipo, @Peso, @Durabilità, @Danno, @PlayerOwner)", connection);

                            //Inserimento dati
                            insertCmd.Parameters.AddWithValue("@Nome", arma.Nome);
                            insertCmd.Parameters.AddWithValue("@Descrizione", arma.Descrizione);
                            insertCmd.Parameters.AddWithValue("@Tipo", tipo);
                            insertCmd.Parameters.AddWithValue("@Peso", arma.Peso);
                            insertCmd.Parameters.AddWithValue("@Durabilità", arma.Durabilità);
                            insertCmd.Parameters.AddWithValue("@Danno", arma.Danno);
                            insertCmd.Parameters.AddWithValue("@PlayerOwner", player.Nome);
                        }

                        //Esecuzione comando
                        insertCmd.ExecuteNonQuery();
                    }

                    //debug + implementazione dei metodi + reset
                }
            }
            catch(Exception ex)
            {
                lblDBError.Text = "Errore nel salvataggio dei dati: " + ex;
            }
        }

        //Bottone per il salvataggio
        protected void btnSalva_Click(object sender, EventArgs e)
        {
            UploadData();
        }
    }
}