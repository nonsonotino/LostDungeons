CREATE DATABASE LostDungeons
GO

USE LostDungeons
GO

CREATE TABLE Ambiente(
	Titolo NVARCHAR(50) PRIMARY KEY NOT NULL,
	Combattimento BIT,
	Descrizione NVARCHAR(100),
	Immagine NVARCHAR(50)
)

CREATE TABLE Passaggio(
	Nome NVARCHAR(50) PRIMARY KEY NOT NULL,
	Direzione INT CHECK(Direzione >= -1 AND Direzione <= 4),
	Riferimento INT,
	Descrizione NVARCHAR(100),
	Ambiente NVARCHAR(50) REFERENCES Ambiente(Titolo) ON DELETE CASCADE
)

CREATE TABLE NPC(
	Nome NVARCHAR(50) PRIMARY KEY NOT NULL,
	Descrizione NVARCHAR(100),
	DMG INT,
	HP INT,
	Ambiente NVARCHAR(50) REFERENCES Ambiente(Titolo) ON DELETE CASCADE
)

CREATE TABLE Player(
	Nome NVARCHAR(50) PRIMARY KEY NOT NULL,
	Descrizione NVARCHAR(100),
	HP INT,
	DEF INT,
	Ambiente NVARCHAR(50) REFERENCES Ambiente(Titolo) ON DELETE CASCADE
)

CREATE TABLE Oggetto(
	Nome NVARCHAR(50) PRIMARY KEY NOT NULL,
	Descrizione NVARCHAR(100),
	Peso INT,
	Tipo NVARCHAR(10),
	DescrizioneUtilizzo NVARCHAR(100),
	PuntiDanno INT,
	PuntiCura INT,
	Durabilità INT,
	Danno INT,
	Protezione INT,
	NPCOwner NVARCHAR(50) REFERENCES NPC(Nome) ON DELETE CASCADE, 
	PlayerOwner NVARCHAR(50) REFERENCES Player(Nome) ON DELETE NO ACTION
)

INSERT INTO Ambiente VALUES 
('Corridoio principale', 0, 'Un corridoio vuoto con una botola sul soffitto, dalla quale sei entrato.', '~/Images/Room0.png'),
('Cucina', 0, 'La cucina personale del Re, non trovi nulla di interessante se non cibo e pentole sporche.', '~/Images/Room4.jpg'),
('Dispensa', 1, 'Uno dispensa che contiene ben poco di valore.', '~/Images/Room1.png'),
('Forgia reale', 1, 'La forgia personale del re, molti dicono che armi leggendarie siano state forgiate in questo luogo', '~/Images/Room6.jpg'),
('Stanza del trono', 1, 'La stanza più sfarzosa del complesso, e al centro di essa un trono dorato.', '~/Images/BossRoom.jpg'),
('Zona comune', 1, 'Una stanza comune, con diverse porte verso varie parti del complesso.', '~/Images/PreBossRoom.jpg')

INSERT INTO Passaggio VALUES 
('Porta Est(0)', 3, -1 , NULL , 'Corridoio principale'),
('Porta Est(1)', 3, -1 , NULL , 'Dispensa'),
('Porta Est(2)', 3, 3 , 'Una porta porta che dà sulla forgia reale.' , 'Zona comune'),
('Porta Est(3)', 3, 5 , 'Una porta verso la zona comune.' , 'Cucina'),
('Porta Est(4)', 3, -1 , NULL, 'Stanza del trono'),
('Porta Est(5)', 3, -1 , NULL, 'Forgia reale'),
('Porta Nord(0)', 0, 5, 'Una porta verso la stanza comune.', 'Corridoio principale'),
('Porta Nord(1)', 0, 0 , 'Una porta verso il corridoio principale', 'Dispensa'),
('Porta Nord(2)', 0, 4 , 'Una porta verso la stanza del trono.', 'Zona comune'),
('Porta Nord(3)', 0, -1 , NULL , 'Cucina'),
('Porta Nord(4)', 0, -1 , NULL , 'Stanza del trono'),
('Porta Nord(5)', 0, -1 , NULL , 'Forgia reale'),
('Porta Ovest(0)', 2, -1 , NULL , 'Corridoio principale'),
('Porta Ovest(1)', 2, -1 , NULL , 'Dispensa'),
('Porta Ovest(2)', 2, 1 , 'Una porta verso le cucine della reggia.' , 'Zona comune'),
('Porta Ovest(3)', 2, -1 , NULL , 'Cucina'),
('Porta Ovest(4)', 2, -1 , NULL , 'Stanza del trono'),
('Porta Ovest(5)', 2, 5 , 'Una porta verso la zona comune.' , 'Forgia reale'),
('Porta Sud(0)', 1, 2 , 'Una porta verso quello che sembra essere una dispensa.' , 'Corridoio principale'),
('Porta Sud(1)', 1, -1 , NULL , 'Dispensa'),
('Porta Sud(2)', 1, 0 , 'Una porta verso il corridoio principale.' , 'Zona comune'),
('Porta Sud(3)', 1, -1 , NULL , 'Cucina'),
('Porta Sud(4)', 1, 5 , 'Una porta verso la zona comune.' , 'Stanza del trono'),
('Porta Sud(5)', 1, -1 , NULL , 'Forgia reale')


INSERT INTO NPC VALUES
('Guardia tombale', 'Un semplice guerriero sottoposto al Re del Sottosuolo', 2, 5, 'Zona comune'),
('Il Guardiano della Forgia', 'Il guariano della Spada Finale!', 3, 10, 'Forgia reale'),
('Il Re del Sottosuolo', 'Monarca del sottosuolo e boss del dungeon.', 5, 15, 'Stanza del trono'),
('Topo', 'Un topo malnutrito e debole, che si rende ostile non appena entri nella stanza', 1, 2, 'Dispensa')

INSERT INTO Player VALUES
('Player', 'Il protagonista della storia', 20, 0, 'Corridoio principale')

INSERT INTO Oggetto VALUES
('Coltello', 'Unsemplice coltello da caccia', 1, 'Arma', NULL, NULL, NULL, 100, 1, NULL, NULL, 'Player'),
('La Spada Finale', 'Una spada capace di distruggere qualsiasi essere vivente con cui venga a contatto!', 3, 'Arma', NULL, NULL, NULL, 100, 3, NULL, 'Il Guardiano della Forgia', NULL),
('Lancia', 'Un arma capace di infliggere 3 danni',1, 'Arma', NULL, NULL, NULL, 100, 3, NULL, 'Guardia tombale', NULL),
('Pozione della resurrezione', 'Una pozione in grado di riportare al mondo coloro che hanno perso la vita', 1 , 'Cura', 'Una luce divina ti avvolge, ti senti VIVO!', NULL, 10000, NULL, NULL, NULL, 'Il Re del Sottosuolo', NULL),
('Pozione di cura', 'Una pozione per curare 10HP', 1 , 'Cura', 'Bevi la pozione e ti senti rinvigorito, guadagni 10HP', NULL, 10, NULL, NULL, NULL, 'Topo', NULL)
