CREATE PROCEDURE [dbo].[SP_Project_Update]
	@projectId UNIQUEIDENTIFIER,
	@description NVARCHAR(MAX)
AS
BEGIN
	UPDATE [Project]
		SET [Description] = @description
		WHERE [ProjectId] = @projectId
END