CREATE TABLE [dbo].[Ingridients](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DishId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[Size] [int] NOT NULL,

 CONSTRAINT [PK_dbo.Ingridients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Ingridients]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Ingridients_dbo.Dishes_Id] FOREIGN KEY([DishId])
REFERENCES [dbo].[Dishes] ([Id])
GO

ALTER TABLE [dbo].[Ingridients] CHECK CONSTRAINT [FK_dbo.Ingridients_dbo.Dishes_Id]
GO

ALTER TABLE [dbo].[Ingridients]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Ingridients_dbo.Products_Id] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO

ALTER TABLE [dbo].[Ingridients] CHECK CONSTRAINT [FK_dbo.Ingridients_dbo.Products_Id]
GO