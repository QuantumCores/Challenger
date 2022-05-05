CREATE TABLE [dbo].[MealDishes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DishId] [bigint] NOT NULL,	
	[MealRecordId] [bigint] NOT NULL,	
	[Servings] [float] NOT NULL,	
	[Energy] [float] NOT NULL,
	[Fats] [float] NOT NULL,
	[Proteins] [float] NOT NULL,
	[Carbohydrates] [float] NOT NULL,
 CONSTRAINT [PK_dbo.MealDishes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[MealDishes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MealDishes_dbo.Dishes_Id] FOREIGN KEY([DishId])
REFERENCES [dbo].[Dishes] ([Id])
GO

ALTER TABLE [dbo].[MealDishes] CHECK CONSTRAINT [FK_dbo.MealDishes_dbo.Dishes_Id]
GO

ALTER TABLE [dbo].[MealDishes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MealDishes_dbo.MealRecords_Id] FOREIGN KEY([MealRecordId])
REFERENCES [dbo].[MealRecords] ([Id])
GO

ALTER TABLE [dbo].[MealDishes] CHECK CONSTRAINT [FK_dbo.MealDishes_dbo.MealRecords_Id]
GO