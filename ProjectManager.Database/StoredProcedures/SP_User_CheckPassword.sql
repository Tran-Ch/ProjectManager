CREATE PROCEDURE [dbo].[SP_User_CheckPassword]
	@email NVARCHAR(320),
	@password NVARCHAR(64)
AS
BEGIN
	SELECT	[EmployeeId]
		FROM	[User]
		WHERE	[Password] = [dbo].[SF_SaltAndHash](@password, [Salt])
			AND	[Email] = @email
END
