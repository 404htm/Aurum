CREATE TABLE [dbo].[CustomerAddress]
(
    [Id] INT NOT NULL PRIMARY KEY Identity,
    [CustomerId] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Customer](Id), 
    [Line1] VARCHAR(200) NULL, 
    [Line2] VARCHAR(200) NULL, 
    [City] VARCHAR(100) NULL, 
    [State] VARCHAR(2) NULL, 
    [Zip] VARCHAR(5) NULL
)
