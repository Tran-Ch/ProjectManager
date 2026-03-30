CREATE PROCEDURE [dbo].[SP_Project_Get_FromEmployeeId]
	@employeeId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[P].[ProjectId],
			[Name],
			[Description],
			[CreationDate],
			[ProjectManagerId]
		FROM [Project] AS [P]
			JOIN [TakePart] AS [TP]
				ON [P].[ProjectId] = [TP].[ProjectId]
		WHERE	[TP].[EmployeeId] = @employeeId
END
