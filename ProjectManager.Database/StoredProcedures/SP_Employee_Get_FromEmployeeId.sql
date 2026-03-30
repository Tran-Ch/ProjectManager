CREATE PROCEDURE [dbo].[SP_Employee_Get_FromEmployeeId]
	@employeeId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[EmployeeId],
			[Firstname],
			[Lastname],
			[Hiredate],
			[IsProjectManager],
			[Email]
		FROM	[V_UserEmployee] 
		WHERE	[EmployeeId] = @employeeId
END