SELECT d.database_id, d.name
FROM master.sys.databases d;

-- CREATE DATABASE RecipeData;

USE RecipeData;

--CREATE TABLE Recipies (
--	RecipeID INT PRIMARY KEY, 
--	RecipeName VARCHAR(100) NOT NULL, 
--	DateCreated DATE 
--		DEFAULT CAST(CURRENT_TIMESTAMP AS DATE)
--	);

SELECT CAST(CURRENT_TIMESTAMP AS DATE);


--INSERT INTO Recipies (RecipeID, RecipeName) VALUES 
--(1, 'Hot honey chicken'), 
--(2, 'Baked sweet potato with roasted vegetables and bulgur wheat'); 

SELECT *
FROM Recipies;

--CREATE TABLE Ingredient(
--	IngredientID INT PRIMARY KEY, 
--	IngredientName VARCHAR(100) NOT NULL, 
--	Quantity DECIMAL(10,2) NOT NULL, 
--	Unit VARCHAR(20) NOT NULL, 
--	RecipeID INT FOREIGN KEY 
--		REFERENCES Recipies (RecipeID)
--	);

--INSERT INTO Ingredient(
--	IngredientID, IngredientName, Quantity, Unit, RecipeID) 
--VALUES
--	(1, 'Whole Chicken', 1, 'Whole', 1), 
--	(2, 'Chilli flakes', 1, 'tbsp', 1),
--	(3, 'Sea salt', 2, 'tbsp', 1), 
--	(4, 'Sugar', 1, 'tsp', 1), 
--	(5, 'Large garlic cloves, grated', 1, 'Whole', 1), 
--	(6, 'Medium Sweet potato', 1, 'Whole', 2), 
--	(7, 'olive oil', 2, 'tbsp', 2), 
--	(8, 'fennel bulb, thinly sliced', 0.5, 'Whole', 2), 
--	(9, 'Red pepper', 0.5, 'cut into cubes', 2), 
--	(10, 'Red Onion', 0.5, 'cut into wedges', 2);

--ALTER TABLE Ingredient
--ALTER COLUMN Quantity DECIMAL(10,2);

SELECT *
FROM Ingredient; 


SELECT
	I.IngredientName, 
	I.Quantity, 
	I.Unit
FROM Recipies R
INNER JOIN Ingredient I 
	ON	R.RecipeID = I.RecipeID
WHERE R.RecipeName = 'Hot honey chicken';


SELECT *
FROM INFORMATION_SCHEMA.TABLES T; 


