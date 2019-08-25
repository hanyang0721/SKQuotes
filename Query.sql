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


--------------------------------------------Create table value function to return tick data-----------------------
USE [Stock]
GO
/****** Object:  UserDefinedFunction [dbo].[GetTodayTickAM]    Script Date: 8/26/2019 00:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetTodayTickAM]
(	
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT stockIdx,sdate, ' ' + LEFT(stime,5) AS stime ,  nOpen, High, Low, nClose, nQty AS vol FROM (
	SELECT stockIdx, SUBSTRING(LTRIM(Str(S.ndate)),5,2) +'/'+RIGHT(S.ndate,2)+'/'+ LEFT(S.ndate,4) AS sdate,
					   DATEADD(MINUTE, 1 ,DATEADD(hour, (Time2 / 100) % 100,
					   DATEADD(minute, (Time2 / 1) % 100, cast('00:00:00' as time(0)))))  AS stime,
       Max(nClose)                                                                AS High,
       Min(nClose)                                                                AS Low,
       dbo.Ptrvalue(Min(Ptr))                                                     AS nOpen,
       dbo.Ptrvalue(Max(Ptr))                                                     AS nClose,
       Sum(nQty)                                                                  AS nQty
	FROM   [Stock].[dbo].[TickData] X
    INNER JOIN (SELECT ndate, lTimehms / 100 AS Time2 FROM   [Stock].[dbo].[TickData] 
				GROUP  BY ndate,lTimehms / 100) S
                ON S.ndate = X.ndate AND S.Time2 = X.lTimehms / 100 --WHERE lTimehms <=104959
	GROUP  BY Time2, S.ndate, stockIdx) E
	WHERE  CAST(stime as time(0)) >= '08:45:00' AND CAST(stime as time(0)) <= '13:45:00' 
	--AND cast(sdate as date) = cast(GETDATE() as date) 
)


-----------------------------------------Create procedure that returns 5 min K bar at specified data, inclduing tick data if needed
USE [Stock]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTicksIn5Min]    Script Date: 8/26/2019 00:07:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_GetTicksIn5Min] 
@from date,
@to date,
@stockID varchar(8)

AS
	SET NOCOUNT ON-----This make sp work like a query, prevent any insert rowcount returns
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	DECLARE @dtdaily date, @ticktime date
	SELECT @dtdaily = MAX(CAST([sdate] as date)) FROM Stock.dbo.StockHisotryMin
	SELECT @ticktime = MAX(CONVERT(datetime,convert(char(8),ndate))) FROM Stock.dbo.TickData

	SELECT (CAST([sdate] AS DATE)) [sdate], 
	--(stime),LEFT(stime,4) + case when RIGHT(stime,1)<='5' then '0' else '5' END stimeround,
	CAST(CAST([sdate] AS DATE) AS VARCHAR) + ' ' +
			LEFT(CASE WHEN RIGHT(stime,1)='0' THEN DATEADD(MINUTE,-5,cast(stime as time(0)))
			WHEN RIGHT(stime,1)>='1' AND RIGHT(stime,1)<='5'THEN DATEADD(MINUTE,-CAST(RIGHT(stime,1) as int),cast(stime as time(0)))
			WHEN RIGHT(stime,1)>='6' AND RIGHT(stime,1)<='9'THEN DATEADD(MINUTE,-(CAST(RIGHT(stime,1) as int)-5),cast(stime as time(0)))
			END,5) AS stime2 ,
	CONVERT(DECIMAL(8,2), [open]/100) [open] , 
	CONVERT(DECIMAL(8,2), [highest]/100) [highest],
	CONVERT(DECIMAL(8,2), [lowest]/100) [lowest], 
	CONVERT(DECIMAL(8,2), [close]/100) [close], [vol] ,
	RANK() OVER (partition by 
	CAST(CAST([sdate] AS DATE) AS VARCHAR) + ' ' +
			LEFT(CASE WHEN RIGHT(stime,1)='0' THEN DATEADD(MINUTE,-5,CAST(stime as time(0)))
			WHEN RIGHT(stime,1)>='1' AND RIGHT(stime,1)<='5'THEN DATEADD(MINUTE,-CAST(RIGHT(stime,1) as int),cast(stime as time(0)))
			WHEN RIGHT(stime,1)>='6' AND RIGHT(stime,1)<='9'THEN DATEADD(MINUTE,-(CAST(RIGHT(stime,1) as int)-5),cast(stime as time(0)))
			END,5) 
	ORDER BY CAST([sdate] AS DATE), stime) [Rank]
    INTO #TEMP1
	FROM  Stock..StockHisotryMin WHERE stockNo=@stockID
	AND [sdate] BETWEEN @from AND @to 

	--If tick data is greater than StockHistoryMin, then it's today
	IF @ticktime>@dtdaily
	BEGIN
		print('Tickkkkkkk')
		INSERT INTO #TEMP1
		SELECT (CAST([sdate] AS DATE)) [sdate], 
		CAST(CAST([sdate] AS DATE) AS VARCHAR) + ' ' +
			LEFT(CASE WHEN RIGHT(stime,1)='0' THEN DATEADD(MINUTE,-5,cast(stime as time(0)))
			WHEN RIGHT(stime,1)>='1' AND RIGHT(stime,1)<='5'THEN DATEADD(MINUTE,-CAST(RIGHT(stime,1) as int),cast(stime as time(0)))
			WHEN RIGHT(stime,1)>='6' AND RIGHT(stime,1)<='9'THEN DATEADD(MINUTE,-(CAST(RIGHT(stime,1) as int)-5),cast(stime as time(0)))
			END,5) AS stime2,
		CONVERT(DECIMAL(8,2), [nopen]/100) [open] , 
		CONVERT(DECIMAL(8,2), High/100) [High],
		CONVERT(DECIMAL(8,2), Low/100) [lowest], 
		CONVERT(DECIMAL(8,2), nClose/100) [close], [vol] ,
		RANK() OVER (partition by 
		CAST(CAST([sdate] AS DATE) AS VARCHAR) + ' ' +
			LEFT(CASE WHEN RIGHT(stime,1)='0' THEN DATEADD(MINUTE,-5,cast(stime as time(0)))
			WHEN RIGHT(stime,1)>='1' AND RIGHT(stime,1)<='5'THEN DATEADD(MINUTE,-CAST(RIGHT(stime,1) as int),cast(stime as time(0)))
			WHEN RIGHT(stime,1)>='6' AND RIGHT(stime,1)<='9'THEN DATEADD(MINUTE,-(CAST(RIGHT(stime,1) as int)-5),cast(stime as time(0)))
			END,5) 
		ORDER BY CAST([sdate] AS DATE), stime) [Rank]
		FROM  Stock..GetTodayTickAM()
	END

	SELECT stime2, MAX([Rank] ) RK INTO #TEMP2 FROM #TEMP1 GROUP BY stime2

	------------prepare index for later join
	create index idx on #TEMP1 (stime2) 
	create index idx on #TEMP2 (stime2) 

	SELECT  CAST(stime2 AS datetime) stime2, 
			MAX([open]) [open], 
			MAX(highest) highest, 
			MIN(lowest) lowest,
			MAX([close]) [close], 
			SUM(vol) vol FROM (
		SELECT S.stime2, 
		CASE WHEN [Rank]=1 THEN [open] ELSE 0 END [open], highest, lowest,
		CASE WHEN [Rank]=RK THEN [close] ELSE 0 END [close] , vol, T.RK 
		FROM #TEMP1 S INNER JOIN #TEMP2 T ON S.stime2=T.stime2) E
	--WHERE CAST(stime2 as time) Between '00:45:00' AND '13:45:00'
	GROUP BY stime2 
	ORDER BY stime2 ASC


-------------------------------------------Create procedure that returns daily K bar at specified data, inclduing tick data if needed
USE [Stock]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTicksDaily]    Script Date: 8/26/2019 00:09:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_GetTicksDaily]
@from date,
@to date,
@stockID varchar(8)

AS
BEGIN

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @dtdaily date, @ticktime date
SELECT @dtdaily = MAX(CAST([sdate] as date)) FROM Stock.dbo.StockHistoryDaily
SELECT @ticktime = MAX(CONVERT(datetime,convert(char(8),ndate))) FROM Stock.dbo.TickData

--select FORMAT(@ticktime,'yyyyMMdd')

--DECLARE @testtime int 
--select @testtime=100955

IF @ticktime>@dtdaily
BEGIN
	DECLARE @tickopen float, @tickclose float, @tickhigh float, @ticklow float, @tickvol int
	SELECT @tickopen = nClose FROM Stock.dbo.TickData WHERE Ptr=(SELECT MIN(Ptr) FROM Stock.dbo.TickData WHERE ndate=FORMAT(@ticktime,'yyyyMMdd') AND lTimehms BETWEEN 84500 AND 134500)
	SELECT @tickclose = nClose FROM Stock.dbo.TickData WHERE Ptr=(SELECT MAX(Ptr) FROM Stock.dbo.TickData WHERE ndate=FORMAT(@ticktime,'yyyyMMdd') AND lTimehms BETWEEN 84500 AND 134500)
	SELECT @tickhigh = MAX(nClose) FROM Stock.dbo.TickData WHERE ndate=FORMAT(@ticktime,'yyyyMMdd') AND lTimehms BETWEEN 84500 AND 134500
	SELECT @ticklow = MIN(nClose) FROM Stock.dbo.TickData WHERE ndate=FORMAT(@ticktime,'yyyyMMdd') AND lTimehms BETWEEN 84500 AND 134500
	SELECT @tickvol = SUM(nQty) FROM Stock.dbo.TickData WHERE ndate=FORMAT(@ticktime,'yyyyMMdd') AND lTimehms BETWEEN 84500 AND 134500

	SELECT @ticktime AS [sdate] , CONVERT(DECIMAL(8,2), @tickopen/100), CONVERT(DECIMAL(8,2), @tickhigh/100), CONVERT(DECIMAL(8,2), @ticklow/100), CONVERT(DECIMAL(8,2), @tickclose/100), @tickvol
	UNION

	SELECT CAST([sdate] as date) AS [sdate],CONVERT(DECIMAL(8,2), [open]/100) , CONVERT(DECIMAL(8,2), [highest]/100)  ,CONVERT(DECIMAL(8,2), [lowest]/100),  
				CONVERT(DECIMAL(8,2), [close]/100), [vol] FROM Stock.dbo.StockHistoryDaily WHERE stockNo=@stockID AND CAST([sdate] as date) 
				 BETWEEN @from AND @to 
	ORDER BY [sdate] ASC
END

ELSE
BEGIN
	SELECT CAST([sdate] as date) AS [sdate],CONVERT(DECIMAL(8,2), [open]/100) , CONVERT(DECIMAL(8,2), [highest]/100)  ,CONVERT(DECIMAL(8,2), [lowest]/100),  
				CONVERT(DECIMAL(8,2), [close]/100), [vol] FROM Stock.dbo.StockHistoryDaily WHERE stockNo=@stockID AND CAST([sdate] as date) 
				 BETWEEN @from AND @to 
	ORDER BY [sdate] ASC
END
END










