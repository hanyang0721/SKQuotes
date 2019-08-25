USE [Stock]
GO

/****** Object:  Table [dbo].[StockHisotryMin]    Script Date: 8/25/2019 23:27:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------Table for Minute data------------------------------------------- 
CREATE TABLE [dbo].[StockHisotryMin](
	[stockNo] [varchar](10) NOT NULL,
	[sdate] [varchar](10) NOT NULL,
	[stime] [varchar](6) NOT NULL,
	[open] [float] NULL,
	[highest] [float] NULL,
	[lowest] [float] NULL,
	[Close] [float] NULL,
	[vol] [float] NULL,
	[EntryDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[sdate] ASC,
	[stime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[StockHisotryMin] ADD  CONSTRAINT [dfxx1]  DEFAULT (getdate()) FOR [EntryDate]
GO


------------------------------------------------Table for Daily data------------------------------------------- 
CREATE TABLE [dbo].[StockHistoryDaily](
	[stockNo] [varchar](10) NOT NULL,
	[sdate] [varchar](16) NOT NULL,
	[open] [float] NULL,
	[highest] [float] NULL,
	[lowest] [float] NULL,
	[Close] [float] NULL,
	[vol] [float] NULL,
	[EntryDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[stockNo] ASC,
	[sdate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[StockHistoryDaily] ADD  DEFAULT (getdate()) FOR [EntryDate]
GO

------------------------------------------------Table for Tick data------------------------------------------- 
CREATE TABLE [dbo].[TickData](
	[stockIdx] [varchar](16) NOT NULL,
	[Ptr] [int] NOT NULL,
	[ndate] [int] NULL,
	[lTimehms] [int] NULL,
	[lTimeMS] [int] NULL,
	[nBid] [float] NULL,
	[nAsk] [float] NULL,
	[nClose] [float] NULL,
	[nQty] [int] NULL,
	[Source] [varchar](8) NULL,
	[EntryDate] [datetime] NULL,
 CONSTRAINT [PK_TickData] PRIMARY KEY CLUSTERED 
(
	[Ptr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TickData] ADD  DEFAULT (getdate()) FOR [EntryDate]
GO


---------------------------------------------Check if tick is running, if not start up SKQuote using powershell
GO
CREATE procedure [dbo].[ChkTick] AS

BEGIN

IF EXISTS (SELECT 1 FROM dbo.TickData WITH (NOLOCK) HAVING ISNULL(MAX(EntryDate),0) < DATEADD(MINUTE, -1,GETDATE()))
BEGIN
    EXEC xp_cmdshell 'powershell.exe ""E:\stopprocess.ps1""  '
    EXEC xp_cmdshell 'powershell.exe Start-Process -FilePath ""C:\Users\HY\Dropbox\SKQuotes\obj\Debug\SKQuote.exe"" '
END

END

