CREATE TABLE [dbo].[FitRecords](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	
	[UserId] [bigint] NOT NULL,

	[RecordDate] [DateTime] NOT NULL,

    [Excersize] [nvarchar] (100) NOT NULL,	

    [Duration] [int] NULL,
	
	[Distance] [float] NULL,

    [Repetitions] [float] NULL,
	
	
 CONSTRAINT [PK_dbo.FitRecords] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[FitRecords]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FitRecords_dbo.Users_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO