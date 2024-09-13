CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(255),
    RoleId INT FOREIGN KEY REFERENCES Roles([Id]),
    EmployeeId INT FOREIGN KEY REFERENCES Employees(EmployeeId) -- If the user is an employee
);