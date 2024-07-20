# BooksAPICoreEF

## Overview

The BooksAPICoreEF project is a .NET Core API designed for managing a collection of books. It supports CRUD operations and filtering by genre and author, leveraging Entity Framework Core for database interactions. Swagger is integrated for interactive API documentation.

## Features

- **CRUD Operations**: Create, Read, Update, Delete book records.
- **Filtering**: Retrieve books based on genre and author.
- **Pagination**: Efficiently manage large datasets.
- **Swagger Integration**: Interactive documentation for easy testing and exploration of API endpoints.

## Getting Started

### Prerequisites

- .NET Core SDK (version 6.0 or later)
- SQL Server or a compatible SQL Server instance
- Visual Studio or another compatible IDE

### Clone the Repository

```bash
git clone https://github.com/sagar-design/BooksAPICoreEF.git
cd BooksAPICoreEF
Configuration
Update Connection String: Open appsettings.json and update the DefaultConnection string with your SQL Server instance and database name.
json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server_name;Database=your_database_name;Trusted_Connection=True;"
}
Create the Database: Use SQL Server Management Studio (SSMS) or another SQL tool to run the SQL scripts provided in the scripts folder (if available).
Scaffold Command
To generate the DbContext and entity classes for your database, use the following scaffold command:

bash
Copy code
dotnet ef dbcontext scaffold "Server=your_server_name;Database=your_database_name;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
This command creates the BookContext and Book class files in the Models directory, allowing Entity Framework Core to interact with your database.

Running the Project Locally
Open the Project: Open the project in Visual Studio (or your preferred IDE).
Build the Project: Restore dependencies and build the project.
Run the Project: Start the project. It will typically run on https://localhost:5001.
Running the Project on Another System
Clone the Repository: Clone the repository as described in the "Clone the Repository" section.
Configure the Connection String: Ensure the appsettings.json file has the correct connection string for the local SQL Server instance.
Run the Project: Open the project in Visual Studio, build it, and run it. It will start on the default port (usually https://localhost:5001).
Testing the API
Navigate to https://localhost:5001/swagger in your web browser to access the Swagger UI and interact with the API endpoints.

Endpoints
GET /WebApi/Books/GetAllRecordsFromDatabase: Retrieve all books with pagination.
GET /WebApi/Books/SearchRecordById/{id}: Retrieve a book by its ID.
POST /WebApi/Books/CreateNewBook: Create a new book record.
PUT /WebApi/Books/Put/UpdateBooksRecord/{id}: Update an existing book record.
DELETE /WebApi/Books/Delete/DeleteBookRecord/{id}: Delete a book record.
GET /WebApi/Books/GetBookWithFilter: Retrieve books filtered by genre and/or author.
Contributing
If you'd like to contribute to this project, please fork the repository and submit a pull request.

License
This project is licensed under the MIT License - see the LICENSE file for details.

Acknowledgements
Thanks to the .NET and EF Core communities for their tools and libraries. Special thanks to Swagger for providing interactive API documentation.

### Key Updates:
- **Scaffold Command**: Added details on how to use the scaffold command to create the `DbContext` and entity classes.
- **Running Locally**: Added instructions on how to set up and run the project on another system.
