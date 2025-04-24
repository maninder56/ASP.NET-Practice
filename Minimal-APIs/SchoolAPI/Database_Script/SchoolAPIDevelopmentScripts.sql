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


-- Department Table data 
SELECT *
FROM school.Department; 
GO

-- Course Table data 
SELECT *
FROM school.Course; 
GO

-- Onsite Course Table data 
SELECT *
FROM school.OnsiteCourse; 
GO 


-- All the cources available
SELECT 
	C.CourseID, 
	C.Title, 
	C.Credits, 
	(CASE 
		WHEN (SC.CourseID IS NULL) THEN 'Online'
		ELSE 'OnSite'
	END ) AS CourseType
FROM school.Course C
LEFT JOIN school.OnsiteCourse SC
	 ON C.CourseID = SC.CourseID
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

-- Inline table-valued function to get column Info
--CREATE FUNCTION dbo.ColumnInfoOfTable (@tableName VARCHAR(30))
--RETURNS TABLE 
--AS 
--RETURN (
--	SELECT C.COLUMN_NAME, C.IS_NULLABLE, C.DATA_TYPE, C.CHARACTER_MAXIMUM_LENGTH
--	FROM INFORMATION_SCHEMA.COLUMNS C
--	WHERE C.TABLE_NAME = @tableName
--); 
--GO



-- All the Table's Columns Info 

-- Department Table's Columns Info
SELECT * FROM dbo.ColumnInfoOfTable('Department'); 
GO 

-- Course Table's Columns Info
SELECT * FROM dbo.ColumnInfoOfTable('Course'); 
GO 

-- OnsiteCourse Table's Columns Info
SELECT * FROM dbo.ColumnInfoOfTable('OnsiteCourse'); 
GO 

-- OnlineCourse Table's Columns Info
SELECT * FROM dbo.ColumnInfoOfTable('OnlineCourse'); 
GO




BEGIN TRANSACTION; 

SELECT *
FROM school.OnsiteCourse; 

SELECT * 
FROM school.Course; 

INSERT INTO school.Course (CourseID, Title, Credits, DepartmentID)
VALUES (999999, 'Dummy Course', 4, 7);
GO

ROLLBACK; 
