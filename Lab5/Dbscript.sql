--Q1. Return customers and their orders, including customers who placed no orders (CustomerID, OrderID, OrderDate)

SELECT Customers.CustomerID, Orders.OrderID, Orders.OrderDate
FROM Customers LEFT OUTER JOIN Orders ON Customers.CustomerID = Orders.CustomerID

--Q2. Report only those customer IDs who never placed any order. (CustomerID, OrderID, OrderDate)

SELECT Customers.CustomerID, Orders.OrderID, Orders.OrderDate
FROM Customers LEFT OUTER JOIN Orders ON Customers.CustomerID = Orders.CustomerID
EXCEPT
SELECT Customers.CustomerID, Orders.OrderID, Orders.OrderDate
FROM Customers JOIN Orders ON Customers.CustomerID = Orders.CustomerID

SELECT C.CustomerID, O.OrderID, O.OrderDate
FROM Customers C
LEFT JOIN Orders O ON C.CustomerID = O.CustomerID
WHERE O.OrderID IS NULL


--Q3. Report those customers who placed orders on July,1997. (CustomerID, OrderID, OrderDate)

SELECT Customers.CustomerID, Orders.OrderID, Orders.OrderDate
FROM Customers INNER JOIN Orders ON Customers.CustomerID = Orders.CustomerID
WHERE MONTH(Orders.OrderDate) = 7 and YEAR(Orders.OrderDate) = 1997;

--Q4. Report the total orders of each customer. (customerID, totalorders)

SELECT C.CustomerID, COUNT(O.OrderID) AS totalorders
FROM Customers C
LEFT JOIN Orders O ON C.CustomerID = O.CustomerID
GROUP BY C.CustomerID;

--Q5. Write a query to generate a five copies of each employee. (EmployeeID, FirstName, LastName)

SELECT E1.EmployeeID,E1.FirstName,E1.LastName FROM Employees AS E1,Employees AS E2 WHERE E2.EmployeeID % 2 = 0 OR E2.EmployeeID = 3 ORDER BY EmployeeID;

--Q6. Write a query that returns a row for each employee and day in the range 04-07-1996 through 04-08-1997. (EmployeeID, Date)

SELECT EmployeeID, GETDATE() as Date FROM Employees

--Q7. Return US customers, and for each customer return the total number of orders and total quantities. (CustomerID, Totalorders, totalquantity)

SELECT C.CustomerID, COUNT(DISTINCT O.OrderID) AS totalorders, SUM(OD.Quantity) as  totalquantity
FROM Customers C
INNER JOIN Orders O ON C.CustomerID = O.CustomerID
INNER JOIN [Order Details] OD ON O.OrderID = OD.OrderID
WHERE C.Country = 'USA'
GROUP BY C.CustomerID;

--Q8. Write a query that returns all customers in the output, but matches them with their respective orders only if they were placed on July 04,1997. (CustomerID, CompanyName, OrderID, Orderdate)

SELECT Customers.CustomerID, Customers.CompanyName , Orders.OrderID, Orders.OrderDate
FROM Customers 
INNER JOIN Orders ON Customers.CustomerID = Orders.CustomerID
WHERE Orders.OrderDate = '1997-07-04 00:00:00.000'


SELECT Customers.CustomerID, Customers.CompanyName , Orders.OrderID, Orders.OrderDate
FROM Customers 
INNER JOIN Orders ON Customers.CustomerID = Orders.CustomerID AND Orders.OrderDate = '1997-07-04 00:00:00.000'

--Q9. Are there any employees who are older than their managers? 

SELECT E1.EmployeeID, E1.FirstName, E1.BirthDate, E1.ReportsTo, E2.EmployeeID as ManagerID, E2.BirthDate as ManagerDateofBirth
FROM Employees E1 JOIN Employees E2 ON E1.ReportsTo = E2.EmployeeID AND E1.BirthDate < E2.BirthDate

SELECT E1.*
FROM Employees E1 JOIN Employees E2 ON E1.ReportsTo = E2.EmployeeID AND E1.BirthDate < E2.BirthDate

--Q10. List that names of those employees and their ages. (EmployeeName, Age, Manager Age)SELECT E1.FirstName + ' ' + E1.LastName as EmployeeName, DATEDIFF(YEAR, E1.BirthDate, GETDATE()) AS Age, DATEDIFF(YEAR, E2.BirthDate, GETDATE()) AS [Manager Age]
FROM Employees E1 JOIN Employees E2 ON E1.ReportsTo = E2.EmployeeID;

--of those having age less than manager

SELECT E1.FirstName + ' ' + E1.LastName as EmployeeName, DATEDIFF(YEAR, E1.BirthDate, GETDATE()) AS Age, DATEDIFF(YEAR, E2.BirthDate, GETDATE()) AS [Manager Age]
FROM Employees E1 JOIN Employees E2 ON E1.ReportsTo = E2.EmployeeID AND E1.BirthDate < E2.BirthDate;

--Q11. List the names of products which were ordered on 8th August 1997. (ProductName, OrderDate)

SELECT P.ProductName, O.OrderDate
FROM 
Products P 
JOIN [Order Details] OD ON P.ProductID = OD.ProductID
JOIN Orders O ON OD.OrderID = O.OrderID
WHERE O.OrderDate = '1997-08-08 00:00:00.000'
ORDER BY P.ProductID

--Q12. List the addresses, cities, countries of all orders which were serviced by Anne and were shipped late. (Address, City, Country)

SELECT O.ShipAddress as Address, O.ShipCity as City, O.ShipCountry as Country
FROM Orders O JOIN Employees E ON O.EmployeeID = E.EmployeeID AND E.FirstName = 'Anne'
WHERE O.ShippedDate > O.RequiredDate

SELECT O.ShipAddress as Address, O.ShipCity as City, O.ShipCountry as Country
FROM Orders O JOIN Employees E ON O.EmployeeID = E.EmployeeID
WHERE O.ShippedDate > O.RequiredDate  AND E.FirstName = 'Anne'

--Q13. List all countries to which beverages have been shipped. (Country)

SELECT DISTINCT O.ShipCountry as Country
FROM 
Orders O JOIN [Order Details] OD ON O.OrderID = OD.OrderID
JOIN Products P ON OD.ProductID = P.ProductID
JOIN Categories C ON C.CategoryID = P.CategoryID
WHERE C.CategoryName = 'Beverages'
