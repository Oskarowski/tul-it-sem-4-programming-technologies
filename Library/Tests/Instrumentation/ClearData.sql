-- Disable foreign key constraints
ALTER TABLE Events NOCHECK CONSTRAINT ALL;
ALTER TABLE States NOCHECK CONSTRAINT ALL;
ALTER TABLE Books NOCHECK CONSTRAINT ALL;

-- Delete data from child tables first
DELETE FROM Events;
DELETE FROM Books;
DELETE FROM States;

-- Delete data from parent tables
DELETE FROM Products;
DELETE FROM Users;

-- Re-enable foreign key constraints
ALTER TABLE Events CHECK CONSTRAINT ALL;
ALTER TABLE States CHECK CONSTRAINT ALL;
ALTER TABLE Books CHECK CONSTRAINT ALL;
