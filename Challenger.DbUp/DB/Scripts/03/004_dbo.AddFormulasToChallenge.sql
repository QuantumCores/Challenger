
-- DROP OLD CHALLENGE COLUMN
ALTER TABLE [dbo].[Challenges]
DROP COLUMN Formula;

-- ADD NEW FORMULA COLUMNS
ALTER TABLE [dbo].[Challenges]
ADD [IsUsingFitDefaultFormula] bit NOT NULL,
	[FitFormula] nvarchar(300) NOT NULL,
	[IsUsingGymDefaultFormula] bit NOT NULL,
	[GymFormula] nvarchar(300) NOT NULL,
	[IsUsingMeasurementDefaultFormula] bit NOT NULL,
	[MeasurementFormula] nvarchar(300) NOT NULL