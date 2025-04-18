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


-- Available Departmetns and associated cources 
SELECT *
FROM school.Department D; 

SELECT 
	C.CourseID, 
	C.Title, 
	D.Name AS DepartmentName
FROM school.Department D
INNER JOIN school.Course C
	ON D.DepartmentID = C.DepartmentID; 

-- Number of cources in each department 
SELECT 
	D.Name, 
	COUNT(D.Name) AS NumberOfCourses
FROM school.Department D
INNER JOIN school.Course C
	ON D.DepartmentID = C.DepartmentID
GROUP BY D.Name; 






