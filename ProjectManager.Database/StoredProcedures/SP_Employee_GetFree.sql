CREATE PROCEDURE [dbo].[SP_Employee_GetFree]
AS
BEGIN
	SELECT	[E].[EmployeeId],
			[Firstname],
			[Lastname],
			[Hiredate],
			[IsProjectManager],
			[Email]
		FROM [V_UserEmployee] AS [E]
			JOIN (
	SELECT	[EmployeeId]
		FROM	[Employee]
	EXCEPT
	SELECT	[EmployeeId]
		FROM [TakePart]
		WHERE	GETDATE() >= [StartDate] 
			AND ([EndDate] IS NULL OR [EndDate] > GETDATE())
			) AS [FREE]
				ON [E].[EmployeeId] = [FREE].[EmployeeId]
	WHERE [IsProjectManager] = 0
END