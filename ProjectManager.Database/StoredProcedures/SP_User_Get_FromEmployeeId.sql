CREATE PROCEDURE [dbo].[SP_User_Get_FromEmployeeId]
	@employeeId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[UserId],
			[Email],
			'********' AS [Password],
			[EmployeeId]
		FROM	[User]
		WHERE [EmployeeId] = @employeeId
END
