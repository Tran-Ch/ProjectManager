CREATE VIEW [dbo].[V_UserEmployee]
	AS 
	SELECT	[E].[EmployeeId],
			[Firstname],
			[Lastname],
			[Hiredate],
			[IsProjectManager],
			[Email]
		FROM [Employee] AS [E]
			JOIN [User] AS [U]
				ON [E].[EmployeeId] = [U].[EmployeeId]
