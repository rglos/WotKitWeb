/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--IF OBJECT_ID('TempDB..#TempWN8ExpectedValues') IS NOT NULL 
--	BEGIN
--		DROP TABLE #TempWN8ExpectedValues
--	END

--CREATE TABLE #TempWN8ExpectedValues
--(
--    [name] NVARCHAR(50) NOT NULL, 
--    [frag] FLOAT NOT NULL, 
--    [dmg] INT NOT NULL, 
--    [spot] FLOAT NOT NULL, 
--    [def] FLOAT NOT NULL, 
--    [win] FLOAT NOT NULL, 
--    [Tier] INT NOT NULL, 
--    [Nation] NVARCHAR(2) NOT NULL, 
--    [Class] NVARCHAR(3) NOT NULL,
--	[IDNum] INT NOT NULL
--)

--BULK INSERT #TempWN8ExpectedValues
--FROM 'D:\Source\GitHub\WotKitWeb1\WotKit\Database\Data\expected_tank_values_v13.csv'
--WITH 
--(
--	FIELDTERMINATOR = ',',
--	ROWTERMINATOR = '\n',
--	FIRSTROW = 2
--)

--SELECT * FROM #TempWN8ExpectedValues;

--DELETE FROM [WotKit].WN8ExpectedTankValues;

--INSERT INTO [WotKit].WN8ExpectedTankValues
--SELECT
--	name, frag, dmg, spot, def, win, Tier, Nation, Class, IDNum
--FROM #TempWN8ExpectedValues;

--SELECT * FROM [WotKit].WN8ExpectedTankValues;