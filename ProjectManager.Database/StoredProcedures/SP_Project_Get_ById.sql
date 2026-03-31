CREATE PROCEDURE [dbo].[SP_Project_Get_ById]
	@projectId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[ProjectId],
			[Name],
			[Description],
			[CreationDate],
			[ProjectManagerId]
		FROM [Project]
		WHERE [ProjectId] = @projectId
END
