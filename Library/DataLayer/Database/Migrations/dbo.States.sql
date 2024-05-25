CREATE TABLE [dbo].[States]
(
	[guid] VARCHAR(36) NOT NULL PRIMARY KEY, 
    [productGuid] VARCHAR(36) NOT NULL, 
    [quantity] INT NOT NULL, 
    [LastUpdatedDate] DATE NOT NULL, 
    CONSTRAINT [FK_States_ToProductTable] FOREIGN KEY ([guid]) REFERENCES [Books]([guid])
)
