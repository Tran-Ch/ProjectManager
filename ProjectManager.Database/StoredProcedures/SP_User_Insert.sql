CREATE PROCEDURE [dbo].[SP_User_Insert]
	@employeeId UNIQUEIDENTIFIER,
	@email NVARCHAR(320),
	@password NVARCHAR(64)
AS
BEGIN
	DECLARE @salt UNIQUEIDENTIFIER = NEWID();
	INSERT INTO [User] (
			[EmployeeId],
			[Email],
			[Password],
			[Salt])
		OUTPUT [inserted].[UserId]
		VALUES (
			@employeeId, 
			@email, 
			[dbo].[SF_SaltAndHash](@password, @salt), 
			@salt);
END