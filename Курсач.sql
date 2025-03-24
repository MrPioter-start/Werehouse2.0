CREATE DATABASE WarehouseDB

CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    RoleID INT NOT NULL,
    AccessCode NVARCHAR(50),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

CREATE TABLE Warehouses (
    WarehouseID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    ManagerID INT,
    FOREIGN KEY (ManagerID) REFERENCES Users(UserID)
);

CREATE TABLE Stock (
    StockID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    WarehouseID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity >= 0),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (WarehouseID) REFERENCES Warehouses(WarehouseID)
);

CREATE TABLE AccessCodes (
    CodeID INT PRIMARY KEY IDENTITY(1,1),
    Code NVARCHAR(50) NOT NULL UNIQUE,
    RoleID INT NOT NULL,
    CreatedBy NVARCHAR(50),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

CREATE TABLE Returns (
    ReturnID INT PRIMARY KEY IDENTITY(1,1),
    SaleID INT NOT NULL, 
    ProductName NVARCHAR(100) NOT NULL,
    ReturnedQuantity INT NOT NULL,
    ReturnTime DATETIME DEFAULT GETDATE(),
    AdminUsername NVARCHAR(50) NOT NULL,
    FOREIGN KEY (SaleID) REFERENCES Sales(SaleID)
);


CREATE TABLE Sales (
    SaleID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    SoldBy NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    SaleDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalPrice DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

CREATE TABLE SaleDetails (
    DetailID INT PRIMARY KEY IDENTITY(1,1),
    SaleID INT NOT NULL,
    ProductName NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (SaleID) REFERENCES Sales(SaleID)
);

CREATE TABLE CashRegister (
    CashID INT PRIMARY KEY IDENTITY(1,1),
    Amount DECIMAL(18,2) NOT NULL DEFAULT 0,
    LastUpdate DATETIME DEFAULT GETDATE()
);

CREATE TABLE CashTransactions (
    TransactionID INT PRIMARY KEY IDENTITY(1,1),
    Amount DECIMAL(18,2) NOT NULL,
    OperationType NVARCHAR(50) NOT NULL,
    Timestamp DATETIME DEFAULT GETDATE(),
    AdminUsername NVARCHAR(50) NOT NULL,
    FOREIGN KEY (AdminUsername) REFERENCES Users(Username)
);


CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,          
    CategoryID INT NOT NULL,               
    Price DECIMAL(18, 2) NOT NULL,          
    Quantity INT NOT NULL,                
    CreatedBy NVARCHAR(50) NOT NULL,      
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID), 
    FOREIGN KEY (CreatedBy) REFERENCES Users(Username)   
);


INSERT INTO Roles (RoleName) VALUES ('Администратор');
INSERT INTO Roles (RoleName) VALUES ('Менеджер');
INSERT INTO Roles (RoleName) VALUES ('Пользователь');

INSERT INTO Users (Username, PasswordHash, RoleID)
VALUES ('admin2', '52575821', 1);

INSERT INTO Users (Username, PasswordHash, RoleID, AccessCode)
VALUES ('manager1', 'hashed_password_here', 2, '1234');

ALTER TABLE Users ADD AccessCodeID INT NULL;
ALTER TABLE Users ADD CONSTRAINT FK_Users_AccessCodes FOREIGN KEY (AccessCodeID) REFERENCES AccessCodes(CodeID);

SELECT COLUMN_NAME 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Products';

ALTER TABLE Products ADD Category NVARCHAR(100);
ALTER TABLE Products ADD Quantity INT;
ALTER TABLE Products ADD CreatedBy NVARCHAR(100);

DROP TABLE Warehouses;

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL
);

drop table Sales
drop table Products

SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'Username';

SELECT * FROM Returns WHERE AdminUsername = 'admin';

ALTER TABLE Categories ADD CreatedBy NVARCHAR(50);
ALTER TABLE Categories
ADD CONSTRAINT FK_Categories_Users
FOREIGN KEY (CreatedBy) REFERENCES Users(Username);

SELECT Username, AccessCode FROM Users WHERE Username = 'sasha';
ALTER TABLE Users DROP COLUMN AccessCode;

ALTER TABLE Sales 
DROP COLUMN AdminUsername; 

ALTER TABLE Sales 
ADD UserUsername NVARCHAR(50) NOT NULL;

ALTER TABLE Sales 
ADD CONSTRAINT FK_Sales_User 
FOREIGN KEY (UserUsername) REFERENCES Users(Username); 

ALTER TABLE Sales 
ADD UserUsername NVARCHAR(50) NOT NULL;

ALTER TABLE Sales 
ADD CONSTRAINT DF_UserUsername DEFAULT 'default_user' FOR UserUsername;

ALTER TABLE CashRegister 
ADD UserUsername NVARCHAR(50) NOT NULL; 

ALTER TABLE CashRegister 
ADD CONSTRAINT FK_CashRegister_User 
FOREIGN KEY (UserUsername) REFERENCES Users(Username); 

ALTER TABLE CashTransactions 
ADD CONSTRAINT FK_CashTransactions_User 
FOREIGN KEY (AdminUsername) REFERENCES Users(Username); 

SELECT * FROM CashRegister WHERE UserUsername = 'admin';

SELECT * FROM CashTransactions WHERE AdminUsername = 'admin';

INSERT INTO Products (Name, CategoryID, Price, Quantity, CreatedBy) VALUES
('Офисный стул', 6, 150, 15, 'admin'),
('Кухонный стол', 7, 560, 8, 'admin'),
('Шкаф-купе', 8, 1200, 5, 'admin'), 
('Угловой диван', 9, 760, 3, 'admin'), 
('Детская кровать', 10, 430, 10, 'admin');

INSERT INTO Products (Name, CategoryID, Price, Quantity, CreatedBy) VALUES
('Кресло-качалка', 6, 2450.00, 7, 'admin'),
('Стеллаж для книг', 7, 3200.50, 12, 'admin'),
('Тумба под TV', 8, 4500.00, 4, 'admin'),
('Кресло-мешок', 9, 1800.00, 20, 'admin'),
('Пуфик круглый', 10, 950.00, 18, 'admin'),
('Стул барный', 6, 1200.00, 10, 'admin'),
('Обеденная группа (стол + 4 стула)', 7, 15000.00, 2, 'admin'),
('Комод с зеркалом', 8, 6800.00, 6, 'admin'),
('Диван-кровать', 9, 23500.00, 4, 'admin'),
('Полка навесная', 10, 1200.00, 25, 'admin'),
('Кресло офисное эргономичное', 6, 8999.99, 9, 'admin'),
('Журнальный столик', 7, 2800.00, 14, 'admin'),
('Шкаф для посуды', 8, 9500.00, 3, 'admin'),
('Модульный диван', 9, 42000.00, 2, 'admin'),
('Чехол для дивана', 10, 1500.00, 30, 'admin');

DELETE FROM CashTransactions 
DELETE FROM CashRegister
DELETE FROM Returns
DELETE FROM Sales
DELETE FROM SaleDetails
DELETE FROM Products
DELETE FROM Categories
DELETE FROM Returns