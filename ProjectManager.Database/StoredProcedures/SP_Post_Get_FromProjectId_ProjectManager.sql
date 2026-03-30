CREATE PROCEDURE [dbo].[SP_Post_Get_FromProjectId_ProjectManager]
	@projectId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[PostId],
			[Subject],
			[Content],
			[SendDate],
			[ProjectId],
			[EmployeeId]
		FROM [Post]
		WHERE [ProjectId] = @projectId
END