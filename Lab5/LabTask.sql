--Task1: Perform all JOIN queries on any table using Northwind Schema

SELECT *
FROM Orders CROSS JOIN [Order Details]

SELECT *
FROM Orders JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID

SELECT *
FROM Orders INNER JOIN [Order Details] ON Orders.OrderID < [Order Details].OrderID

SELECT *
FROM Orders LEFT JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID 

