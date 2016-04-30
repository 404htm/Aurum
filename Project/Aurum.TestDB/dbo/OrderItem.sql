CREATE TABLE [dbo].[OrderItem]
(
    [OrderId] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Order](Id),
    [ItemId] INT NOT NULL  FOREIGN KEY REFERENCES [inventory].[Item](Id),
    [UnitPrice] NUMERIC (10, 2) NOT NULL,
    [Quantity] Int NOT NULL Default 1
)
