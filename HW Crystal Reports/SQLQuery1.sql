create table Employee  
(  
Emp_ID int identity(1,1) constraint PK_Emp primary key,  
Emp_Name varchar(30),  
Emp_Contact nchar(15),  
Emp_Salary decimal(7,2)  
) 
insert into Employee values ('Rakesh','9924194054','15000.22');  
insert into Employee values ('Amit','9824194555','17000');  
insert into Employee values ('Ketan','9824198245','14000');  
insert into Employee values ('Ketan','9994198245','12000');  
insert into Employee values ('Chirag','9824398245','10000');  
insert into Employee values ('Naren','9824398005','10000'); 

create view vw_Employee  
as  
Select Emp_ID,Emp_Name,Emp_Contact,Emp_Salary  
from Employee  
WHERE Emp_Salary >= 14000
GO 