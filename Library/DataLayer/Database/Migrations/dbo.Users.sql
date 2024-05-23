CREATE TABLE [dbo].[Users]
(
	[guid] NCHAR(30) NOT NULL PRIMARY KEY, 
    [firstName] NCHAR(10) NOT NULL, 
    [lastName] NCHAR(10) NOT NULL, 
    [email] NCHAR(10) NOT NULL, 
    [balance] FLOAT NOT NULL, 
    [phoneNumber] NCHAR(10) NOT NULL
)
