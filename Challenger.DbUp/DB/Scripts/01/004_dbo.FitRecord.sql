CREATE TABLE [dbo].[FitRecords](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[Excersize] [nvarchar](100) NOT NULL,
	[Duration] [int] NULL,
	[DurationUnit] [varchar](1) NULL,
	[Distance] [float] NULL,
	[Repetitions] [float] NULL,
	[BurntCalories] [float] NOT NULL,
 CONSTRAINT [PK_dbo.FitRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FitRecords]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FitRecords_dbo.Users_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[FitRecords] CHECK CONSTRAINT [FK_dbo.FitRecords_dbo.Users_Id]
GO