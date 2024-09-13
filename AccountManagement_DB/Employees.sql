CREATE TABLE Employees (
    EmployeeId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Department NVARCHAR(100) NOT NULL,
    JobTitle NVARCHAR(100) NOT NULL,
    Salary DECIMAL(18,2) NOT NULL CHECK (Salary > 0),
    RemoteWorkStatus NVARCHAR(10) NOT NULL
);
