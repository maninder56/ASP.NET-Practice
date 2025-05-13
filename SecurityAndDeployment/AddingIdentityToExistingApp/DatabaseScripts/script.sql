SELECT name
FROM master.sys.databases; 
GO

--USE dummy; 
--GO

--CREATE DATABASE AddingIdentityToExistingAppDatabase; 

--USE AddingIdentityToExistingAppDatabase; 
--GO 


SELECT T.TABLE_NAME, T.TABLE_TYPE
FROM INFORMATION_SCHEMA.TABLES T; 



CREATE TABLE Products(
	ProductID INT PRIMARY KEY, 
	Name VARCHAR(100), 
	Quantity INT 
);
GO 



INSERT INTO Products(ProductID, Name, Quantity) VALUES 
(1, 'Clock', 899), 
(2, 'Pencil', 23); 



SELECT *
FROM Products; 






