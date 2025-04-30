SELECT D.database_id, D.name
FROM master.sys.databases D;
GO 

USE SchoolForAPI; 
GO

-- All The tables in database 
SELECT T.TABLE_SCHEMA, T.TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES T;	
GO

-- Create new schema and transfer tables to it
--CREATE SCHEMA school; 

--ALTER SCHEMA school TRANSFER dbo.Department; 
--GO 

--ALTER SCHEMA school TRANSFER dbo.Person; 
--GO 

--ALTER SCHEMA school TRANSFER dbo.OnsiteCourse; 
--GO 

--ALTER SCHEMA school TRANSFER dbo.OnlineCourse; 
--GO 

--ALTER SCHEMA school TRANSFER dbo.StudentGrade; 
--GO 

--ALTER SCHEMA school TRANSFER dbo.CourseInstructor; 
--GO 

--ALTER SCHEMA school TRANSFER dbo.Course; 
--GO 

--ALTER SCHEMA school TRANSFER dbo.OfficeAssignment; 
--GO



-- School tables 
-- All The tables in database 
SELECT T.TABLE_SCHEMA, T.TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES T
WHERE T.TABLE_SCHEMA = 'school'; 
GO


-- Department data 
SELECT *
FROM school.Department; 
GO

-- Course data 
SELECT *
FROM school.Course; 
GO

-- Onsite Course data 
SELECT *
FROM school.OnsiteCourse; 
GO 

-- Online Course data 
SELECT *
FROM school.OnlineCourse; 
GO 


-- Course Instructor data 
SELECT *
FROM school.CourseInstructor; 

-- Person data 
SELECT *
FROM school.Person; 

-- Office Assignment data 
SELECT *
FROM school.OfficeAssignment; 

-- Student Grade data 
SELECT *
FROM school.StudentGrade; 





-- All the cources available
SELECT 
	C.CourseID, 
	C.Title, 
	C.Credits, 
	(CASE 
		WHEN (SC.CourseID IS NOT NULL) THEN 'OnSite'
		WHEN (OC.CourseID IS NOT NULL) THEN 'Online'
		ELSE NULL
	END ) AS CourseType
FROM school.Course C
LEFT JOIN school.OnsiteCourse SC
	 ON C.CourseID = SC.CourseID
LEFT JOIN school.OnlineCourse OC
	ON C.CourseID = OC.CourseID
ORDER BY CourseType; 
GO

-- Available Departmetns and associated cources 
SELECT 
	C.CourseID, 
	C.Title, 
	D.Name AS DepartmentName
FROM school.Department D
INNER JOIN school.Course C
	ON D.DepartmentID = C.DepartmentID; 
GO

-- Number of cources in each department 
SELECT 
	D.Name, 
	COUNT(D.Name) AS NumberOfCourses
FROM school.Department D
INNER JOIN school.Course C
	ON D.DepartmentID = C.DepartmentID
GROUP BY D.Name; 
GO


-- All Instructors 
SELECT 
	P.PersonID, 
	P.FirstName, 
	P.LastName, 
	P.HireDate
FROM school.Person P
WHERE P.Discriminator = 'Instructor';


-- All Students
SELECT 
	P.PersonID, 
	P.FirstName, 
	P.LastName, 
	P.EnrollmentDate
FROM school.Person P
WHERE P.Discriminator = 'Student';







-- Inline table-valued function to get column Info

--CREATE FUNCTION school.ColumnInfoOfTable (@tableName VARCHAR(30))
--RETURNS TABLE 
--AS 
--RETURN (
--	SELECT C.COLUMN_NAME, C.IS_NULLABLE, C.DATA_TYPE, C.CHARACTER_MAXIMUM_LENGTH, C.COLUMN_DEFAULT
--	FROM INFORMATION_SCHEMA.COLUMNS C
--	WHERE C.TABLE_NAME = @tableName
--); 
--GO

--DROP FUNCTION dbo.ColumnInfoOfTable; 

-- All the Table's Columns Info 

-- Department Table's Columns Info
SELECT * FROM school.ColumnInfoOfTable('Department'); 
GO 

-- Course Table's Columns Info
SELECT * FROM school.ColumnInfoOfTable('Course'); 
GO 

-- OnsiteCourse Table's Columns Info
SELECT * FROM school.ColumnInfoOfTable('OnsiteCourse'); 
GO 

-- OnlineCourse Table's Columns Info
SELECT * FROM school.ColumnInfoOfTable('OnlineCourse'); 
GO


--  OfficeAssignment Columns Info 
SELECT * FROM school.ColumnInfoOfTable('OfficeAssignment'); 
GO

-- Person Columsn Info 
SELECT * FROM school.ColumnInfoOfTable('Person');
GO 

-- Student Grade Info 
SELECT * FROM school.ColumnInfoOfTable('StudentGrade'); 
GO 


-- Add course for testing 

--BEGIN TRANSACTION; 

--SELECT *
--FROM school.OnsiteCourse; 

--SELECT * 
--FROM school.Course; 

--INSERT INTO school.Course (CourseID, Title, Credits, DepartmentID)
--VALUES (999999, 'Dummy Course', 4, 7);
--GO

--ROLLBACK; 

--COMMIT; 




-- Add Instructor for testing

--BEGIN TRANSACTION; 

--SELECT *
--FROM school.Person; 

--INSERT INTO school.Person (LastName, FirstName, Discriminator) 
--	OUTPUT inserted.PersonID, inserted.LastName, inserted.FirstName, inserted.Discriminator
--VALUES ('oooooo', 'Dummy Instructor', 'Instructor');

--ROLLBACK;

--COMMIT; 



--SET IDENTITY_INSERT school.Person ON;
--GO



-- Testing for constraints in person and studentgrade tabel

--BEGIN TRANSACTION; 

--INSERT INTO school.Person (LastName, FirstName, Discriminator) 
--	OUTPUT inserted.PersonID, inserted.LastName, inserted.FirstName, inserted.Discriminator
--VALUES ('oooooo', 'Dummy Student', 'Student');

--INSERT INTO school.StudentGrade(CourseID, StudentID, Grade)
--	OUTPUT inserted.EnrollmentID, inserted.CourseID, inserted.StudentID, inserted.Grade
--VALUES (4061, 8892, 4.1); 


--SELECT *
--FROM school.Person
--WHERE PersonID > 8000; 

--DELETE FROM school.Person
--WHERE PersonID > ; 

--DELETE FROM school.StudentGrade
--WHERE EnrollmentID = ; 

--ROLLBACK;

