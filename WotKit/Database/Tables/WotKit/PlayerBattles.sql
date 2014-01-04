CREATE TABLE [WotKit].[PlayerBattles]
(
    [PlayerId] INT NOT NULL, 
    [BattleId] INT NOT NULL, 
    [DamageDealt] INT NOT NULL, 
    [DamageReceived] INT NOT NULL, 
    [TankName] NVARCHAR(50) NOT NULL, 
    [XP] INT NOT NULL, 
    [XPPenalty] INT NOT NULL, 
    [tmenXP] INT NOT NULL, 
    [freeXP] INT NOT NULL, 
    [eventXP] INT NOT NULL, 
    [eventTMenXP] INT NOT NULL, 
    [eventFreeXP] INT NOT NULL, 
    [credits] INT NOT NULL, 
    [autoRepairCost] INT NOT NULL, 
    [autoEquipCost] INT NOT NULL, 
    [autoLoadCost] INT NOT NULL, 
    [won] BIT NOT NULL, 
	[kills] INT NOT NULL
    CONSTRAINT [PK_PlayerBattles] PRIMARY KEY ([PlayerId], [BattleId]), 
    [spotted] INT NOT NULL, 
    [capturePoints] INT NOT NULL, 
    [droppedCapturePoints] INT NOT NULL, 
    [typeCompDescr] INT NULL, 
    CONSTRAINT [FK_PlayerBattles_Players] FOREIGN KEY ([PlayerId]) REFERENCES [WotKit].[Players]([PlayerId]),
    CONSTRAINT [FK_PlayerBattles_Battles] FOREIGN KEY ([BattleId]) REFERENCES [WotKit].[Battles]([BattleId])
)
