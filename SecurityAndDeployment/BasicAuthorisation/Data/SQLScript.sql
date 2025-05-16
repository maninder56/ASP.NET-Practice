SELECT name
FROM master.sys.databases; 

-- USE [aspnet-BasicAuthorisation-ac5b97de-c923-4058-8a6c-1f05bafd3fb0];  
-- GO


SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES; 

-- all user passwords : Ibara123#

SELECT *
FROM AspNetUsers U; 


SELECT *
FROM AspNetUserClaims UC; 

--INSERT INTO AspNetUserClaims(UserId, ClaimType, ClaimValue) VALUES 
--('4543b1b3-f846-4770-b922-f2e61df6af61', 'PrivateLoungeAccess', ''); 

SELECT 
	UC.Id AS ClaimID, 
	UC.ClaimType, 
	U.UserName
FROM AspNetUsers U
INNER JOIN AspNetUserClaims UC
	ON	U.Id = UC.UserId; 

