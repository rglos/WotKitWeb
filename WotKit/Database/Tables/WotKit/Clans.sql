CREATE TABLE [WotKit].[Clans]
(
	[ClanId] INT NOT NULL, 
    [Abbreviation] NVARCHAR(50) NOT NULL, 
    [Name] NVARCHAR(255) NOT NULL, 
    [EmblemLarge] NVARCHAR(100) NOT NULL, 
    [EmblemSmall] NVARCHAR(100) NOT NULL, 
    [EmblemBWTank] NVARCHAR(100) NOT NULL, 
    [EmblemMedium] NVARCHAR(100) NOT NULL,
    [CreatedAtDate] DATETIME NOT NULL, 
    [UpdatedAtDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_Clans] PRIMARY KEY NONCLUSTERED ([ClanId])
)
