CREATE TABLE [WotKit].[WN8ExpectedTankValues]
(
    [Name] NVARCHAR(50) NOT NULL, 
    [Frag] FLOAT NOT NULL, 
    [Damage] INT NOT NULL, 
    [Spot] FLOAT NOT NULL, 
    [Defense] FLOAT NOT NULL, 
    [Win] FLOAT NOT NULL, 
    [Tier] INT NOT NULL, 
    [Nation] NVARCHAR(2) NOT NULL, 
    [Class] NVARCHAR(3) NOT NULL, 
    [IDNum] INT NOT NULL, 
    PRIMARY KEY ([Name])
)
