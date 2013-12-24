CREATE TABLE [WotKit].[Players]
(
	[PlayerId] INT NOT NULL IDENTITY, 
    [AccountDBID] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_Players] PRIMARY KEY NONCLUSTERED ([PlayerId])
)

GO

CREATE UNIQUE CLUSTERED INDEX [IX_Players_AccountDBID] ON [WotKit].[Players] ([AccountDBID])
