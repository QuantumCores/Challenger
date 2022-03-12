CREATE TABLE [dbo].[MealRecords](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DiaryRecordId] [bigint] NOT NULL,
	[MealName] [nvarchar](64) NOT NULL,
	[IsNextDay] [bit] DEFAULT 0,
	[RecordTime] [int] NULL,	
 CONSTRAINT [PK_dbo.MealRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MealRecords]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MealRecords_dbo.DiaryRecords_Id] FOREIGN KEY([DiaryRecordId])
REFERENCES [dbo].[DiaryRecords] ([Id])
GO

ALTER TABLE [dbo].[MealRecords] CHECK CONSTRAINT [FK_dbo.MealRecords_dbo.DiaryRecords_Id]
GO