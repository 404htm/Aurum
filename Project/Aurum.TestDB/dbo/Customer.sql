CREATE TABLE [dbo].[Customer]
(
    [Id] INT NOT NULL PRIMARY KEY Identity,
    [Active] BIT NOT NULL DEFAULT 1,
    [FirstName] VARCHAR(100),
    [LastName] VARCHAR(100),
    [Notes] VARCHAR(MAX),
    [InsertDate] DateTime NOT NULL DEFAULT GETUTCDATE()
)
