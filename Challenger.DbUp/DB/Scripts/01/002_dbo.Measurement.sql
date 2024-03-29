IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Measurements'))
   PRINT 'Table Measurements exists';
ELSE
	BEGIN
		CREATE TABLE [dbo].[Measurements](
			[Id] [bigint] IDENTITY(1,1) NOT NULL,
	
			[UserId] [bigint] NOT NULL,
	
			[MeasurementDate] [DateTime] NOT NULL,
	
			[Weight] [float] NOT NULL,
	
			[Waist] [float] NULL,
	
			[Neck] [float] NULL,
	
			[Chest] [float] NULL,
	
			[Hips] [float] NULL,
	
			[Biceps] [float] NULL,
	
			[Tigh] [float] NULL,
	
			[Calf] [float] NULL,	
	
			[Fat] [float] NULL,
	
		 CONSTRAINT [PK_dbo.Measurements] PRIMARY KEY CLUSTERED 
		(
			[ID] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		
		ALTER TABLE [dbo].[Measurements]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Measurements_dbo.Users_Id] FOREIGN KEY([UserId])
		REFERENCES [dbo].[Users] ([Id])		

	END
GO