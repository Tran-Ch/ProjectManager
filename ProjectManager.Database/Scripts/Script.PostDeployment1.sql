BEGIN TRANSACTION

DECLARE @empId UNIQUEIDENTIFIER, @projId UNIQUEIDENTIFIER
DECLARE @empIds TABLE ([EmployeeId] UNIQUEIDENTIFIER NOT NULL)
DECLARE @projIds TABLE ([ProjectId] UNIQUEIDENTIFIER NOT NULL)

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Samuel', @lastname=N'Legrain', @hiredate='2016-03-21'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'samuel@project.be', @password=N'Test1234='

EXEC [SP_Employee_Set_IsProjectManager] @employeeId = @empId

INSERT INTO @projIds([ProjectId])
EXEC [SP_Project_Insert] @projectManagerId = @empId, @name=N'Project Alpha', @description=N'Projet destiné au test de la base de donnée fournie à Interface3 pour l''épreuve ASP MVC du groupe WAD25.'

SELECT @projId = [ProjectId] FROM @projIds
DELETE FROM @projIds

UPDATE [Project]
	SET [Creationdate] = '2019-07-08'
	WHERE [ProjectId] = @projId

EXEC [SP_Post_Insert] @employeeId = @empId, @projectId = @projId, @subject=N'Première réunion', @content=N'La première réunion concernant la définition des besoins et l''attribution des rôles dans l''équipe se déroulera à distance le lundi suivant. Merci de votre attention et de votre disponibilité.'

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Aude', @lastname=N'Beurive', @hiredate='2016-06-21'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'aude@project.be', @password=N'Test1234='

EXEC [SP_Employee_Set_IsProjectManager] @employeeId = @empId
INSERT INTO @empIds([EmployeeId])

EXEC [SP_Employee_Insert] @firstname=N'Aurélien', @lastname=N'Strimelle', @hiredate='2016-09-21'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'aurelien@project.be', @password=N'Test1234='

EXEC [SP_Employee_Set_IsProjectManager] @employeeId = @empId

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Alexandre', @lastname=N'Claes', @hiredate='2016-12-21'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'alexandre@project.be', @password=N'Test1234='

EXEC [SP_Employee_Set_IsProjectManager] @employeeId = @empId

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Julie', @lastname=N'Lefebvre', @hiredate='2018-03-14'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'julie@project.be', @password=N'Test1234='

EXEC [SP_TakePart_Insert] @employeeId = @empId, @projectId= @projId, @startdate = '2020-01-01'

EXEC [SP_Post_Insert] @employeeId = @empId, @projectId = @projId, @subject=N'Repositionnement des directives', @content=N'Suite aux nouvelles normes, ils nous est demandé d''adapter nos directives. Veuillez prendre note lors de la génération de vos éléments procéduraux.'

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Thomas', @lastname=N'Dubois', @hiredate='2019-07-22'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'thomas@project.be', @password=N'Test1234='

EXEC [SP_TakePart_Insert] @employeeId = @empId, @projectId= @projId, @startdate = '2020-01-01'

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Sarah', @lastname=N'Mertens', @hiredate='2020-11-05'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'sarah@project.be', @password=N'Test1234='

EXEC [SP_TakePart_Insert] @employeeId = @empId, @projectId= @projId, @startdate = '2020-01-01'

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Nicolas', @lastname=N'Petit', @hiredate='2017-01-30'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'nicolas@project.be', @password=N'Test1234='

EXEC [SP_TakePart_Insert] @employeeId = @empId, @projectId= @projId, @startdate = '2020-01-01'

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Emma', @lastname=N'Janssen', @hiredate='2021-09-12'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'emma@project.be', @password=N'Test1234='

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Maxime', @lastname=N'Renard', @hiredate='2015-05-18'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'maxime@project.be', @password=N'Test1234='

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Laura', @lastname=N'Peeters', @hiredate='2022-02-28'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'laura@project.be', @password=N'Test1234='

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'David', @lastname=N'Lambert', @hiredate='2016-10-10'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'david@project.be', @password=N'Test1234='

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Chloé', @lastname=N'Michel', @hiredate='2023-06-01'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'chloé@project.be', @password=N'Test1234='

INSERT INTO @empIds([EmployeeId])
EXEC [SP_Employee_Insert] @firstname=N'Antoine', @lastname=N'Leroy', @hiredate='2014-12-15'

SELECT @empId = [EmployeeId] FROM @empIds
DELETE FROM @empIds

EXEC [SP_User_Insert] @employeeId = @empId, @email=N'antoine@project.be', @password=N'Test1234='

COMMIT

GO