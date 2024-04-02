--DataBase Lab 6

--Problems Solved Using Subquery

--Q1. Return customers and their orders, including customers who placed no orders (CustomerID, OrderID, OrderDate)

SELECT (SELECT C.CustomerID FROM Customers C WHERE C.CustomerID = C1.CustomerID) as CustomerID, O.OrderID, O.OrderDate
FROM Customers C1 LEFT JOIN Orders O ON C1.CustomerID = O.CustomerID

--Q2. Report only those customer IDs who never placed any order. (CustomerID, OrderID, OrderDate)

SELECT C.CustomerID, (SELECT OrderID FROM Orders O WHERE O.CustomerID = C.CustomerID) AS OrderID
,(SELECT OrderID FROM Orders O WHERE O.CustomerID = C.CustomerID) AS OrderDate 
FROM Customers C
WHERE C.CustomerID NOT IN (SELECT CustomerID FROM Orders)

--Q3. Report those customers who placed orders on July,1997. (CustomerID, OrderID, OrderDate)

SELECT (SELECT CustomerID FROM Customers WHERE Orders.CustomerID=Customers.CustomerID) AS CustomerID,OrderID,OrderDate
FROM Orders
GROUP BY OrderID,OrderDate,CustomerID
HAVING Year(OrderDate)=1997 and MONTH(OrderDate) = 7
ORDER BY OrderID;

--Q4. Report the total orders of each customer. (customerID, totalorders)

SELECT C.CustomerID, (SELECT Count(*) FROM Orders O WHERE O.CustomerID = C.CustomerID ) AS totalorders
FROM Customers C

--Q5. Write a query to generate a five copies of each employee. (EmployeeID, FirstName, LastName)

SELECT E.EmployeeID, E.firstname, E.lastname
FROM Employees AS E
CROSS JOIN dbo.Products AS N
WHERE N.ProductID <= 5
ORDER BY EmployeeID asc;

--Q6. List all the products whose price is more than average price. 

SELECT ProductName
FROM Products
WHERE UnitPrice > (SELECT AVG(UnitPrice) FROM Products)

--Q7. Find the second highest price of product. 

SELECT TOP(1) ProductName, UnitPrice
FROM Products
WHERE UnitPrice  < (SELECT MAX(UnitPrice) FROM Products)
ORDER BY UnitPrice DESC

--Q8. Write a query that returns a row for each employee and day in the range 04-07-1996 through 04-08-1997. (EmployeeID, Date)SELECT EmployeeID, OrderDate 
FROM Orders 
WHERE OrderDate IN (
    SELECT OrderDate 
    FROM Orders 
    WHERE OrderDate BETWEEN '1996-07-04 00:00:00.000' AND '1997-08-04 00:00:00.000'
);

--Q9. Return US customers, and for each customer return the total number of orders and total quantities. (CustomerID, Totalorders, totalquantity)

SELECT CustomerID,
(SELECT COUNT(*) FROM Orders WHERE Customers.CustomerID = Orders.CustomerID) AS TotalOrders,
(SELECT SUM(Quantity) FROM [Order Details] WHERE [Order Details].OrderID IN (SELECT OrderID FROM Orders WHERE Orders.CustomerID = Customers.CustomerID)) AS TotalQuantity
FROM Customers 
WHERE Country = 'USA';

--Q10. Write a query that returns all customers in the output, but matches them with their respective orders only if they were placed on July 04,1997. (CustomerID, CompanyName, OrderID, Orderdate)

SELECT (SELECT CustomerID FROM Customers WHERE Orders.CustomerID=Customers.CustomerID) AS CustomerID,
(SELECT CompanyName FROM Customers WHERE Orders.CustomerID=Customers.CustomerID) AS CompanyName,
OrderID, OrderDate
FROM Orders
WHERE OrderDate = '1997-07-04 00:00:00.000'
GROUP BY OrderID, OrderDate,CustomerID
ORDER BY OrderID;

--Q11. Are there any employees who are older than their managers?

SELECT *
FROM Employees E
WHERE E.BirthDate < (SELECT BirthDate FROM Employees M WHERE E.ReportsTo = M.EmployeeID)

--Q12. List that names of those employees and their ages. (EmployeeName, Age, Manager Age)

SELECT FirstName + ' ' + LastName AS EmployeeName, DATEDIFF(YEAR, BirthDate, GETDATE()) AS Age,
(SELECT DATEDIFF(YEAR, BirthDate, GETDATE()) FROM Employees M WHERE E.ReportsTo = M.EmployeeID) AS  ManagerAge
FROM Employees E
WHERE E.BirthDate < (SELECT BirthDate FROM Employees M WHERE E.ReportsTo = M.EmployeeID)

--Q13. List the names of products which were ordered on 8th August 1997. (ProductName, OrderDate)

SELECT (SELECT P.ProductName FROM Products P WHERE P.ProductID = OD.ProductID) AS ProductName,
(SELECT O.OrderDate FROM Orders O WHERE O.OrderID = OD.OrderID) AS OrderDate
FROM [Order Details] OD
WHERE OD.OrderID = (SELECT OrderID FROM Orders WHERE OrderDate =  '1997-08-08 00:00:00.000')

--Q14. List the addresses, cities, countries of all orders which were serviced by Anne and were shipped late. (Address, City, Country)

SELECT ShipAddress AS Address, ShipCity AS City, ShipCountry AS Country
FROM Orders
WHERE EmployeeID IN (SELECT EmployeeID FROM Employees WHERE FirstName = 'Anne') AND ShippedDate > RequiredDate;

--Q15. List all countries to which beverages have been shipped. (Country)

SELECT DISTINCT o.ShipCountry
FROM Orders o
WHERE o.OrderID IN (
    SELECT od.OrderID
    FROM [Order Details] od
    JOIN Products p ON p.ProductID = od.ProductID
    JOIN Categories c ON c.CategoryID = p.CategoryID
    WHERE c.CategoryID = 1
);

