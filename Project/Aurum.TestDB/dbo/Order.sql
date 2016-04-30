CREATE TABLE [dbo].[Order]
(
    [Id] INT NOT NULL PRIMARY KEY Identity,
    [CustomerId] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Customer](Id), 
    [Date] DateTime NOT NULL DEFAULT GETUTCDATE()
)
