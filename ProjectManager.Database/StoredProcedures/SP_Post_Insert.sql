CREATE PROCEDURE [dbo].[SP_Post_Insert]
	@employeeId UNIQUEIDENTIFIER,
	@projectId UNIQUEIDENTIFIER,
	@subject NVARCHAR(256),
	@content NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO [Post] ([Subject],[Content],[ProjectId],[EmployeeId])
	OUTPUT [inserted].[PostId]
	VALUES (@subject, @content, @projectId, @employeeId)
END