USE [Hotel]
GO
/****** Object:  Table [dbo].[Camere]    Script Date: 08/03/2024 20:32:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Camere](
	[IdCamera] [int] IDENTITY(1,1) NOT NULL,
	[NumeroCamera] [int] NOT NULL,
	[Descrizione] [nvarchar](500) NOT NULL,
	[Tipologia] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Camere] PRIMARY KEY CLUSTERED 
(
	[IdCamera] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clienti]    Script Date: 08/03/2024 20:32:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clienti](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[CodiceFiscale] [nvarchar](16) NOT NULL,
	[Cognome] [nvarchar](100) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Citta] [nvarchar](100) NOT NULL,
	[Provincia] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Telefono] [nvarchar](15) NULL,
	[Cellulare] [nvarchar](15) NULL,
 CONSTRAINT [PK_Clienti] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DettagliServizi]    Script Date: 08/03/2024 20:32:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DettagliServizi](
	[IdDettaglio] [int] IDENTITY(1,1) NOT NULL,
	[IdPrenotazione] [int] NOT NULL,
	[IdServizio] [int] NOT NULL,
	[Data] [date] NOT NULL,
	[Quantita] [int] NOT NULL,
 CONSTRAINT [PK_DettagliServizi] PRIMARY KEY CLUSTERED 
(
	[IdDettaglio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prenotazioni]    Script Date: 08/03/2024 20:32:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prenotazioni](
	[IdPrenotazione] [int] IDENTITY(1,1) NOT NULL,
	[DataPrenotazione] [date] NOT NULL,
	[DataInizio] [date] NOT NULL,
	[DataFine] [date] NOT NULL,
	[Caparra] [decimal](18, 0) NOT NULL,
	[Tariffa] [decimal](18, 0) NOT NULL,
	[Dettagli] [nvarchar](500) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[IdCamera] [int] NOT NULL,
 CONSTRAINT [PK_Prenotazioni] PRIMARY KEY CLUSTERED 
(
	[IdPrenotazione] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiziAggiuntivi]    Script Date: 08/03/2024 20:32:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiziAggiuntivi](
	[IdServizio] [int] IDENTITY(1,1) NOT NULL,
	[Descrizione] [nvarchar](500) NOT NULL,
	[Prezzo] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_ServiziAggiuntivi] PRIMARY KEY CLUSTERED 
(
	[IdServizio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DettagliServizi]  WITH CHECK ADD  CONSTRAINT [FK_DettagliServizi_Prenotazioni] FOREIGN KEY([IdPrenotazione])
REFERENCES [dbo].[Prenotazioni] ([IdPrenotazione])
GO
ALTER TABLE [dbo].[DettagliServizi] CHECK CONSTRAINT [FK_DettagliServizi_Prenotazioni]
GO
ALTER TABLE [dbo].[DettagliServizi]  WITH CHECK ADD  CONSTRAINT [FK_DettagliServizi_ServiziAggiuntivi] FOREIGN KEY([IdServizio])
REFERENCES [dbo].[ServiziAggiuntivi] ([IdServizio])
GO
ALTER TABLE [dbo].[DettagliServizi] CHECK CONSTRAINT [FK_DettagliServizi_ServiziAggiuntivi]
GO
ALTER TABLE [dbo].[Prenotazioni]  WITH CHECK ADD  CONSTRAINT [FK_Prenotazioni_Camere] FOREIGN KEY([IdCamera])
REFERENCES [dbo].[Camere] ([IdCamera])
GO
ALTER TABLE [dbo].[Prenotazioni] CHECK CONSTRAINT [FK_Prenotazioni_Camere]
GO
ALTER TABLE [dbo].[Prenotazioni]  WITH CHECK ADD  CONSTRAINT [FK_Prenotazioni_Clienti] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Clienti] ([IdCliente])
GO
ALTER TABLE [dbo].[Prenotazioni] CHECK CONSTRAINT [FK_Prenotazioni_Clienti]
GO
