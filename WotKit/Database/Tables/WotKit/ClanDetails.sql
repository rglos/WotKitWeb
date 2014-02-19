CREATE TABLE [WotKit].[ClanDetails]
(
	[ClanId] INT NOT NULL, 
    [PlayerId] INT NOT NULL, 
    [AsOfDate] DATETIME NOT NULL, 
    [ClanBattles] INT NOT NULL, 
    [AllBattles] INT NOT NULL, 
    [CompanyBattles] INT NOT NULL, 
	[Role] NVARCHAR(50) NOT NULL, 
    [Rolei18n] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_ClanDetails] PRIMARY KEY NONCLUSTERED ([ClanId], [PlayerId], [AsOfDate]), 
    CONSTRAINT [FK_ClanDetails_Clans] FOREIGN KEY ([ClanId]) REFERENCES [WotKit].[Clans]([ClanId]), 
    CONSTRAINT [FK_ClanDetails_Players] FOREIGN KEY ([PlayerId]) REFERENCES [WotKit].[Players]([PlayerId])
)
