CREATE TABLE [WotKit].[Tanks]
(
	[TankId] INT NOT NULL PRIMARY KEY, 
    [Nation] NVARCHAR(50) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Level] INT NOT NULL, 
    [IsPremium] BIT NOT NULL, 
    [Namei18n] NVARCHAR(50) NOT NULL, 
    [Nationi18n] NVARCHAR(50) NOT NULL, 
    [TankType] NVARCHAR(50) NOT NULL,
	[ExpectedFrag] FLOAT NOT NULL, 
    [ExpectedDamage] FLOAT NOT NULL, 
    [ExpectedSpot] FLOAT NOT NULL, 
    [ExpectedDefense] FLOAT NOT NULL, 
    [ExpectedWin] FLOAT NOT NULL
)
