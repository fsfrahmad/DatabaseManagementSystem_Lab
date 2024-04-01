--1. Write a query to report orders which were delayed shipped.

SELECT *
FROM Orders 
WHERE RequiredDate < ShippedDate

--2. Our employees belong to how many countries. List the names.

SELECT DISTINCT Country 
FROM Employees

--3. Is there any employee who is not accountable?

SELECT *
FROM Employees
WHERE ReportsTo IS NULL

--4. List the names of products which have been discontinued.

SELECT ProductName
FROM Products
WHERE Discontinued = 1

--5. List the IDs the orders on which discount was not provided.

SELECT OrderID
FROM [Order Details]
WHERE Discount = 0

--6. Enlist the names of customers who have not specified their region

SELECT CompanyName
FROM Customers
WHERE Region IS NULL

--7. Enlist the names of customers along with contact number who either belongs to UK or USA.

SELECT CompanyName, ContactName
FROM Customers
WHERE Country = 'UK' OR Country = 'USA'

--8. Report the names of companies who have provided their web page.
SELECT CompanyName
FROM Suppliers
WHERE HomePage IS NOT NULL

--9. In which countries, products were sold in year 1997.

SELECT DISTINCT ShipCountry
FROM Orders
WHERE YEAR(ShippedDate) =1997 

--10. List the ids of customers whose orders were never shipped.

SELECT CustomerID
FROM Orders
WHERE ShippedDate IS NULL

--11. Write a query to report suppliers with their id, company name and city. 

SELECT SupplierID,CompanyName,City
FROM Suppliers

--12. Our employees belong to how many countries. List them who are used to live in London. 

SELECT COUNT(DISTINCT Country) AS NoOfCountries
FROM Employees

SELECT *
FROM Employees
WHERE City = 'London'

--13. List the names of products which have not been discontinued.

SELECT ProductName
FROM Products
WHERE Discontinued = 0

--14. List the IDs the orders on which discount was 0.1 or less. 

SELECT OrderID
FROM [Order Details]
WHERE Discount <= 0.1 AND Discount > 0

--15. Enlist the IDS, names of employees and their contact number with extensions who have not specified theirregion. 

SELECT EmployeeID, FirstName + ' ' + LastName as Name, HomePhone
FROM Employees
WHERE Region IS NULL