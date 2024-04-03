--DB QUIZ

--Q1. 

SELECT ContactTitle, Region, Country
FROM Customers
GROUP BY ContactTitle,Region,Country
HAVING COUNT(*) > 1


--Q3

SELECT LEFT(ProductName, 1) AS FirstLetterOfProduct, COUNT(*) AS TotalProducts
FROM Products
GROUP BY LEFT(ProductName, 1)
ORDER BY LEFT(ProductName, 1);

--Q2

SELECT EmployeeID, Employees.Country,Employees.Region
FROM Employees
WHERE EmployeeID IS NOT NULL