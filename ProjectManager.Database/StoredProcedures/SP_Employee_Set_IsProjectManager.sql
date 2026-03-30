CREATE PROCEDURE [dbo].[SP_Employee_Set_IsProjectManager]
	@employeeId UNIQUEIDENTIFIER
AS
BEGIN
	UPDATE [Employee]
		SET [IsProjectManager] = 1
		WHERE [EmployeeId] = @employeeId
END