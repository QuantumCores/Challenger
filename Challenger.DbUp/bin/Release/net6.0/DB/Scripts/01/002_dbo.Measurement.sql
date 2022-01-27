CREATE TABLE [dbo].[Measurements](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	
	[UserId] [bigint] NOT NULL,
	
	[MeasurementDate] [DateTime]
	
	[Weight] [double] NOT NULL,
	
	[Waist] [double] NULL,
	
	[Neck] [double] NULL,
	
	[Chest] [double] NULL,
	
	[Hips] [double] NULL,
	
	[Biceps] [double] NULL,
	
	[Tigh] [double] NULL,
	
	[Calf] [double] NULL,	
	
	
 CONSTRAINT [PK_dbo.Measurements] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Measurements]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Measurements_dbo.User_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO