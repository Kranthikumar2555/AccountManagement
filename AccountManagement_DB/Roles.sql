CREATE TABLE Roles (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(50), 
    [NormalizedName] NVARCHAR(256) NULL, 
    [ConcurrencyStamp] NVARCHAR(MAX) NULL
);