﻿CREATE TABLE [WotKit].[Battles]
(
	[BattleId] INT NOT NULL IDENTITY, 
    [ArenaUniqueId] BIGINT NOT NULL, 
    [ParserVersion] NVARCHAR(20) NOT NULL, 
    [ArenaCreateTime] DATETIME NOT NULL, 
    [ArenaTypeId] INT NOT NULL, 
    [ArenaTypeIcon] NVARCHAR(20) NOT NULL, 
    [ArenaTypeName] NVARCHAR(20) NOT NULL, 
    [BonusType] INT NOT NULL, 
    [BonusTypeName] NVARCHAR(20) NOT NULL, 
    [Duration] FLOAT NOT NULL, 
    [FinishReason] INT NOT NULL, 
    [FinishReasonName] NVARCHAR(10) NOT NULL, 
    [GameplayId] INT NOT NULL, 
    [GameplayName] NVARCHAR(10) NOT NULL, 
    [Result] NVARCHAR(10) NOT NULL, 
    [WinnerTeam] INT NOT NULL, 
    [VehLockMode] INT NOT NULL,
	CONSTRAINT [PK_Battles] PRIMARY KEY NONCLUSTERED ([BattleId])
)

GO

CREATE UNIQUE CLUSTERED INDEX [IX_Battles_ArenaUniqueId] ON [WotKit].[Battles] ([ArenaTypeId])
