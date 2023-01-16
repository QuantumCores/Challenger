-- ADD AGGREGATION COLUMNS
ALTER TABLE [dbo].[Challenges]
ADD [AggregateFitFormula] bit NOT NULL,
	[AggregateGymFormula] bit NOT NULL,
	[AggregateMeasurementFormula] bit NOT NULL