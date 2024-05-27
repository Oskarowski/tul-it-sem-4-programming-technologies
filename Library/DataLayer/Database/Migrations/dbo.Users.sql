CREATE TABLE [dbo].[Users]
(
	[guid] VARCHAR(36) NOT NULL PRIMARY KEY, 
    [firstName] NVARCHAR(50) NOT NULL, 
    [lastName] NVARCHAR(50) NOT NULL, 
    [email] NVARCHAR(255) NOT NULL, 
    [balance] FLOAT NOT NULL, 
    [phoneNumber] NVARCHAR(15) NOT NULL
)
