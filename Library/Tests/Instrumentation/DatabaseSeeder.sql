-- Seed Users
INSERT INTO Users (FirstName, LastName, Email, Balance, PhoneNumber)
VALUES 
('John', 'Doe', 'john.doe@example.com', 100.00, '123-456-7890'),
('Jane', 'Smith', 'jane.smith@example.com', 150.50, '098-765-4321'),
('Alice', 'Johnson', 'alice.johnson@example.com', 200.75, '456-123-7890');

-- Seed Products
INSERT INTO Products (Name, Price)
VALUES 
('Book A', 29.99),
('Book B', 39.99),
('Book C', 49.99);

-- Seed States
INSERT INTO States (ProductID, Quantity)
SELECT ID, 10 FROM Products WHERE Name = 'Book A';

INSERT INTO States (ProductID, Quantity)
SELECT ID, 5 FROM Products WHERE Name = 'Book B';

INSERT INTO States (ProductID, Quantity)
SELECT ID, 20 FROM Products WHERE Name = 'Book C';

-- Seed Events
INSERT INTO Events (UserID, StateID, EventType, Amount)
SELECT 
(SELECT ID FROM Users WHERE Email = 'john.doe@example.com'),
(SELECT ID FROM States WHERE ProductID = (SELECT ID FROM Products WHERE Name = 'Book A')),
'Borrow', 2;

INSERT INTO Events (UserID, StateID, EventType, Amount)
SELECT 
(SELECT ID FROM Users WHERE Email = 'jane.smith@example.com'),
(SELECT ID FROM States WHERE ProductID = (SELECT ID FROM Products WHERE Name = 'Book B')),
'Return', 1;

INSERT INTO Events (UserID, StateID, EventType, Amount)
SELECT 
(SELECT ID FROM Users WHERE Email = 'alice.johnson@example.com'),
(SELECT ID FROM States WHERE ProductID = (SELECT ID FROM Products WHERE Name = 'Book C')),
'Delivery', 5;

-- Seed Books
INSERT INTO Books (ProductID, Author, Publisher, Pages, PublicationDate)
SELECT ID, 'Author A', 'Publisher A', 300, '2020-01-01'
FROM Products WHERE Name = 'Book A';

INSERT INTO Books (ProductID, Author, Publisher, Pages, PublicationDate)
SELECT ID, 'Author B', 'Publisher B', 250, '2021-05-15'
FROM Products WHERE Name = 'Book B';

INSERT INTO Books (ProductID, Author, Publisher, Pages, PublicationDate)
SELECT ID, 'Author C', 'Publisher C', 400, '2019-10-10'
FROM Products WHERE Name = 'Book C';
