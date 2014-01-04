CREATE TABLE [WotKit].[Tanks]
(
	[TankId] INT NOT NULL PRIMARY KEY, 
    [Nation] NVARCHAR(50) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Level] INT NOT NULL, 
    [IsPremium] BIT NOT NULL, 
    [Namei18n] NVARCHAR(50) NOT NULL, 
    [Nationi18n] NVARCHAR(50) NOT NULL, 
    [TankType] NVARCHAR(50) NOT NULL
)
