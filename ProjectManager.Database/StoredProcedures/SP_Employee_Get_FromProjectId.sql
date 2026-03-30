CREATE PROCEDURE [dbo].[SP_Employee_Get_FromProjectId]
	@projectId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[E].[EmployeeId],
			[Firstname],
			[Lastname],
			[Hiredate],
			[IsProjectManager],
			[Email]
		FROM [V_UserEmployee] AS [E]
			JOIN [TakePart] AS [TP]
				ON [E].[EmployeeId] = [TP].[EmployeeId]
		WHERE [TP].[ProjectId] = @projectId
END
