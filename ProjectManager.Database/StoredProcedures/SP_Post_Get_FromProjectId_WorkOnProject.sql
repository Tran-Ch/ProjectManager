CREATE PROCEDURE [dbo].[SP_Post_Get_FromProjectId_WorkOnProject]
	@projectId UNIQUEIDENTIFIER,
	@employeeId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[PostId],
			[Subject],
			[Content],
			[SendDate],
			[P].[EmployeeId],
			[P].[ProjectId]
		FROM	[Post] AS [P]
			JOIN [TakePart] AS [TP]
				ON	[P].[ProjectId] = [TP].[ProjectId]
					AND	[TP].[EmployeeId] = @employeeId
		WHERE	[P].[ProjectId] = @projectId
			AND ([TP].[EndDate] IS NULL OR [P].[SendDate] <= [TP].[EndDate])
		ORDER BY [P].[SendDate] DESC;
END
