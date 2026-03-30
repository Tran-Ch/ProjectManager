CREATE PROCEDURE [dbo].[SP_Employee_Check_IsProjectManager]
	@employeeId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[IsProjectManager]
		FROM [Employee]
		WHERE [EmployeeId] = @employeeId
END
