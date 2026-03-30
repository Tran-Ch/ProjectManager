CREATE TABLE [dbo].[TakePart]
(
	[EmployeeId] UNIQUEIDENTIFIER NOT NULL,
	[ProjectId] UNIQUEIDENTIFIER NOT NULL,
	[StartDate] DATETIME2 NOT NULL,
	[EndDate] DATETIME2,
	CONSTRAINT [PK_TakePart] PRIMARY KEY ([EmployeeId], [ProjectId]),
	CONSTRAINT [FK_TakePart_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee] ([EmployeeId]),
	CONSTRAINT [FK_TakePart_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([ProjectId]),
	CONSTRAINT [CK_TakePart_EndDate] CHECK ([EndDate] IS NULL OR [EndDate] > [StartDate])
)
