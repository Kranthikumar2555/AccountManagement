# Project Name

## Overview

**Project Name** is an ASP.NET Core MVC application that allows users to submit feedback for a mobile app. The feedback is stored securely in a SQL Server database, with the email address encrypted for privacy. The project also integrates with external bug tracking systems like GitHub Issues to create bug reports from user feedback.

## Features

- Feedback submission with form validation.
- Secure storage of sensitive information (email encryption using Data Protection API).
- Integration with external bug tracking systems (e.g., GitHub Issues).
- Responsive UI with Bootstrap 4 for a smooth user experience.

## Technologies Used

- ASP.NET Core 6.0+
- Entity Framework Core
- SQL Server
- Data Protection API for encryption
- jQuery for client-side validation
- Bootstrap 4 for responsive UI
- GitHub Issues API integration

---

## Setup and Configuration

### Prerequisites

Before you can run this project, ensure that you have the following installed:

- **.NET 6.0 SDK** or later
- **SQL Server** (LocalDB or any SQL Server instance)
- **GitHub Personal Access Token** (if using GitHub Issues integration)
- **GitHub Issues Enabled** (for the repository)

### Installation Steps

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/your-username/your-repo.git
    cd your-repo
    ```

2. **Install Dependencies**:
    Ensure you have all the necessary .NET and NuGet packages installed:
    ```bash
    dotnet restore
    ```

3. **Configure the Database Connection**:
   You need to set up the connection string for the SQL Server in the `appsettings.json` file. This connection string will allow Entity Framework Core to connect to your database.

    - Open the `appsettings.json` file and update the `DefaultConnection` with your SQL Server credentials.
    
    **appsettings.json**:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;MultipleActiveResultSets=true"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      }
    }
    ```

    - Replace `YOUR_SERVER_NAME` with your SQL Server name (for LocalDB, use `(localdb)\\MSSQLLocalDB`).
    - Replace `YOUR_DATABASE_NAME` with your desired database name.

4. **Run the Database Migrations**:
    After configuring the connection string, you need to apply the migrations to create the database schema.

    Run the following command:
    ```bash
    dotnet ef database update
    ```

5. **Configure Email Encryption (Optional)**:
   The project uses the **Data Protection API** to encrypt email addresses before storing them in the database. No special configuration is needed, but ensure the machine key is properly protected in production environments for secure data encryption.

6. **Enable GitHub Issues for Your Repository**:
    To integrate with **GitHub Issues**, make sure that **Issues** are enabled for your GitHub repository.

    - Go to the repository on GitHub.
    - Click on the **Settings** tab.
    - Scroll down to the **Features** section.
    - Check the **Issues** box to enable GitHub Issues if it isn't already enabled.

7. **Set Up GitHub Issues Integration**:
    If you want to integrate with **GitHub Issues** to create bug reports from user feedback, you need to configure your **GitHub Personal Access Token (PAT)**:

    - Generate a PAT from GitHub with the `repo` scope enabled.
    - In `appsettings.json`, add the following configuration:

    **appsettings.json**:
    ```json
    {
      "GitHub": {
        "Token": "YOUR_GITHUB_PERSONAL_ACCESS_TOKEN",
        "RepositoryOwner": "your-username",
        "RepositoryName": "your-repo"
      }
    }
    ```

    - Replace `YOUR_GITHUB_PERSONAL_ACCESS_TOKEN` with your actual GitHub token.
    - Replace `your-username` and `your-repo` with your GitHub username and repository name.

### Running the Application

Once everything is configured, you can run the application using the following command:

```bash
dotnet run
