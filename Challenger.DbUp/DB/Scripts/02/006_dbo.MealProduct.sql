CREATE TABLE [dbo].[MealProducts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [bigint] NOT NULL,	
	[MealRecordId] [bigint] NOT NULL,	
	[Size] [int] NOT NULL,	
	[Energy] [int] NOT NULL,
	[Fats] [int] NOT NULL,
	[Proteins] [int] NOT NULL,
	[Carbohydrates] [int] NOT NULL,
 CONSTRAINT [PK_dbo.MealProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[MealProducts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MealProducts_dbo.Products_Id] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO

ALTER TABLE [dbo].[MealProducts] CHECK CONSTRAINT [FK_dbo.MealProducts_dbo.Products_Id]
GO

ALTER TABLE [dbo].[MealProducts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MealProducts_dbo.MealRecords_Id] FOREIGN KEY([MealRecordId])
REFERENCES [dbo].[MealRecords] ([Id])
GO

ALTER TABLE [dbo].[MealProducts] CHECK CONSTRAINT [FK_dbo.MealProducts_dbo.MealRecords_Id]
GO