SELECT name
FROM master.sys.databases; 
GO

--USE dummy; 
--GO


--USE [aspnet-BasicRazorAppWithIdentity-cfa044bf-c5c7-4b41-8bda-e0a529e73572]; 
--GO 


SELECT T.TABLE_NAME, T.TABLE_TYPE
FROM INFORMATION_SCHEMA.TABLES T; 



SELECT *
FROM AspNetRoles; 

SELECT *
FROM AspNetRoleClaims;

SELECT *
FROM AspNetUserRoles; 



SELECT *
FROM AspNetUsers; 



SELECT *
FROM AspNetUserClaims; 


-- For Third-party logins like google 
SELECT *
FROM AspNetUserLogins; 

SELECT *
FROM AspNetUserTokens; 


--BEGIN TRANSACTION; 

--UPDATE AspNetUsers
--SET EmailConfirmed = 1
--WHERE USERNAME = 'Ibara@gamil.com';





