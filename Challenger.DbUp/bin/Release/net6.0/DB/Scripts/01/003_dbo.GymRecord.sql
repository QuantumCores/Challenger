CREATE TABLE [dbo].[GymRecords](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	
	[UserId] [bigint] NOT NULL,

	[RecordDate] [DateTime] NOT NULL,

    [Excersize] [nvarchar] (100) NOT NULL,

    [Weight] [double] NOT NULL,

    [Repetitions] [int] NOT NULL,

    [MuscleGroup] [nvarchar] (100) NOT NULL,	
	
 CONSTRAINT [PK_dbo.GymRecords] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GymRecords]  WITH CHECK ADD  CONSTRAINT [FK_dbo.GymRecords_dbo.User_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO