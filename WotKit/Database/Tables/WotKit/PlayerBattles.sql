CREATE TABLE [WotKit].[PlayerBattles]
(
    [PlayerId] INT NOT NULL, 
    [BattleId] INT NOT NULL, 
    [DamageDealt] INT NOT NULL, 
    [DamageReceived] INT NOT NULL, 
    [TankName] NVARCHAR(50) NOT NULL, 
    [XP] INT NOT NULL, 
    [XPPenalty] INT NOT NULL, 
    CONSTRAINT [PK_PlayerBattles] PRIMARY KEY ([PlayerId], [BattleId]), 
    CONSTRAINT [FK_PlayerBattles_Players] FOREIGN KEY ([PlayerId]) REFERENCES [WotKit].[Players]([PlayerId]),
    CONSTRAINT [FK_PlayerBattles_Battles] FOREIGN KEY ([BattleId]) REFERENCES [WotKit].[Battles]([BattleId])
)
