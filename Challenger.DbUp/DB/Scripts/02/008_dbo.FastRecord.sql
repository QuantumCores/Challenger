CREATE TABLE [dbo].[FastRecords](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,	
	[MealRecordId] [bigint] NOT NULL,
	[Comment] [nvarchar](64) NULL,
	[Energy] [float] NOT NULL,
	[Fats] [float] NOT NULL,
	[Proteins] [float] NOT NULL,
	[Carbohydrates] [float] NOT NULL,
 CONSTRAINT [PK_dbo.FastRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FastRecords]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FastRecords_dbo.MealRecords_Id] FOREIGN KEY([MealRecordId])
REFERENCES [dbo].[MealRecords] ([Id])
GO

ALTER TABLE [dbo].[FastRecords] CHECK CONSTRAINT [FK_dbo.FastRecords_dbo.MealRecords_Id]
GO