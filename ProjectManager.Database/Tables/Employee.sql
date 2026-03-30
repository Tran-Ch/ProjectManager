CREATE TABLE [dbo].[Employee]
(
	[EmployeeId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	[Firstname] NVARCHAR(64) NOT NULL,
	[Lastname] NVARCHAR(64) NOT NULL,
	[Hiredate] DATE NOT NULL,
	[IsProjectManager] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [PK_Employee] PRIMARY KEY ([EmployeeId])
)
