DROP TABLE IF EXISTS OrderProduct;
DROP TABLE IF EXISTS ComputerEmployee;
DROP TABLE IF EXISTS EmployeeTraining;
DROP TABLE IF EXISTS Employee;
DROP TABLE IF EXISTS TrainingProgram;
DROP TABLE IF EXISTS Computer;
DROP TABLE IF EXISTS Department;
DROP TABLE IF EXISTS [Order];
DROP TABLE IF EXISTS PaymentType;
DROP TABLE IF EXISTS Product;
DROP TABLE IF EXISTS ProductType;
DROP TABLE IF EXISTS Customer;
CREATE TABLE Department (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
[Name] VARCHAR(55) NOT NULL,
Budget INTEGER NOT NULL
);
CREATE TABLE Employee (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
FirstName VARCHAR(55) NOT NULL,
LastName VARCHAR(55) NOT NULL,
DepartmentId INTEGER NOT NULL,
IsSuperVisor BIT NOT NULL DEFAULT(0),
CONSTRAINT FK_EmployeeDepartment FOREIGN KEY(DepartmentId) REFERENCES Department(Id)
);
CREATE TABLE Computer (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
PurchaseDate DATETIME NOT NULL,
DecomissionDate DATETIME,
Make VARCHAR(55) NOT NULL,
Manufacturer VARCHAR(55) NOT NULL
);
CREATE TABLE ComputerEmployee (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
EmployeeId INTEGER NOT NULL,
ComputerId INTEGER NOT NULL,
AssignDate DATETIME NOT NULL,
UnassignDate DATETIME,
CONSTRAINT FK_ComputerEmployee_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(Id),
CONSTRAINT FK_ComputerEmployee_Computer FOREIGN KEY(ComputerId) REFERENCES Computer(Id)
);
CREATE TABLE TrainingProgram (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
[Name] VARCHAR(255) NOT NULL,
StartDate DATETIME NOT NULL,
EndDate DATETIME NOT NULL,
MaxAttendees INTEGER NOT NULL
);
CREATE TABLE EmployeeTraining (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
EmployeeId INTEGER NOT NULL,
TrainingProgramId INTEGER NOT NULL,
CONSTRAINT FK_EmployeeTraining_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(Id),
CONSTRAINT FK_EmployeeTraining_Training FOREIGN KEY(TrainingProgramId) REFERENCES TrainingProgram(Id)
);
CREATE TABLE ProductType (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
[Name] VARCHAR(55) NOT NULL
);
CREATE TABLE Customer (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
FirstName VARCHAR(55) NOT NULL,
LastName VARCHAR(55) NOT NULL,
CreationDate DATETIME NOT NULL,
LastActiveDate DATETIME NOT NULL
);
CREATE TABLE Product (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
ProductTypeId INTEGER NOT NULL,
CustomerId INTEGER NOT NULL,
Price MONEY NOT NULL,
Title VARCHAR(255) NOT NULL,
[Description] VARCHAR(255) NOT NULL,
Quantity INTEGER NOT NULL,
CONSTRAINT FK_Product_ProductType FOREIGN KEY(ProductTypeId) REFERENCES ProductType(Id),
CONSTRAINT FK_Product_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id)
);
CREATE TABLE PaymentType (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
AcctNumber VARCHAR(55) NOT NULL,
[Name] VARCHAR(55) NOT NULL,
CustomerId INTEGER NOT NULL,
CONSTRAINT FK_PaymentType_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id)
);
CREATE TABLE [Order] (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
CustomerId INTEGER NOT NULL,
PaymentTypeId INTEGER,
CONSTRAINT FK_Order_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id),
CONSTRAINT FK_Order_Payment FOREIGN KEY(PaymentTypeId) REFERENCES PaymentType(Id)
);
CREATE TABLE OrderProduct (
Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
OrderId INTEGER NOT NULL,
ProductId INTEGER NOT NULL,
CONSTRAINT FK_OrderProduct_Product FOREIGN KEY(ProductId) REFERENCES Product(Id),
CONSTRAINT FK_OrderProduct_Order FOREIGN KEY(OrderId) REFERENCES [Order]
);
INSERT INTO Department ([Name], Budget) VALUES ('HR', 100)
INSERT INTO Department ([Name], Budget) VALUES ('IT', 80)
INSERT INTO Department ([Name], Budget) VALUES ('Sales', 1000)
INSERT INTO Department ([Name], Budget) VALUES ('Development', 5)
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Anakin', 'Skywalker', 1, 'TRUE')
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Luke', 'Skywalker', 2, 'FALSE')
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Han', 'Solo', 3, 'FALSE')
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Leia', 'Organa', 4, 'FALSE')
INSERT INTO TrainingProgram ([Name], StartDate, EndDate, MaxAttendees) VALUES ('DennisMethod', '10/01/2019', '07/22/2020',15)
INSERT INTO TrainingProgram ([Name], StartDate, EndDate, MaxAttendees) VALUES ('PrintWorld', '12/31/2019', '01/11/2020',15)
INSERT INTO TrainingProgram ([Name], StartDate, EndDate, MaxAttendees) VALUES ('StackOverflow', '11/14/2019', '03/16/2020',15)
INSERT INTO TrainingProgram ([Name], StartDate, EndDate, MaxAttendees) VALUES ('Javascript', '04/20/2020', '08/16/2020',15)
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (1,1)
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (2,1)
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (3,2)
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (4,3)
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('12/31/2016', 'Surface Pro', 'Microsoft')
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('12/31/2015', 'Macbook Pro', 'Apple')
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('11/15/2016', 'Inspiron 15 3000', 'Dell')
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('12/31/2017', 'Elite Dragonfly', 'HP')
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (1, 3, '02/15/2020')
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (2, 2, '03/15/2020')
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (3, 1, '04/15/2020')
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (4, 4, '05/15/2020')
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (1, 4, '02/15/2019', '12/15/2019')
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (2, 1, '03/15/2019', '12/15/2019')
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (3, 2, '04/15/2019', '12/15/2019')
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (4, 3, '05/15/2019', '12/15/2019')

INSERT INTO Computer (PurchaseDate, Make, Manufacturer)  VALUES ('07/03/2020', 'Surface Pro 3', 'Microsoft');
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('09/03/2020', 'Surface Pro 5', 'Microsoft');

INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (1,5,'07/04/2020','07/28/2020');