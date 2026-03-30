CREATE PROCEDURE [dbo].[SP_Employee_Insert]
	@firstname NVARCHAR(64),
	@lastname NVARCHAR(64),
	@hiredate DATE
AS
BEGIN
	INSERT INTO [Employee]([Firstname],[Lastname],[Hiredate])
		OUTPUT [inserted].[EmployeeId]
		VALUES (@firstname, @lastname,@hiredate);
END
