CREATE PROCEDURE [dbo].[SP_Post_Update]
	@postId UNIQUEIDENTIFIER,
	@employeeId UNIQUEIDENTIFIER,
	@content NVARCHAR(MAX)
AS
BEGIN
	UPDATE [Post]
		SET [Content] = @content
		WHERE	[EmployeeId] = @employeeId
			AND [PostId] = @postId
END
