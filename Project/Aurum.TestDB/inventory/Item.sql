CREATE TABLE [inventory].[Item]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [Active] BIT NOT NULL DEFAULT 1,
    [Code] VARCHAR(10),
    [Description] VARCHAR(200)

)
