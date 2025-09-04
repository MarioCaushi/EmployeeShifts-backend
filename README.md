# EmployeeShift Backend

![.NET](https://img.shields.io/badge/.NET-Backend-purple?logo=dotnet&logoColor=white)
![CSharp](https://img.shields.io/badge/C%23-Backend-green?logo=csharp&logoColor=white)
![SQL](https://img.shields.io/badge/SQL-Database-orange?logo=postgresql&logoColor=white)
![REST API](https://img.shields.io/badge/API-REST-blueviolet)
![Status](https://img.shields.io/badge/Status-Completed-success)
![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)


Welcome to the backend repository for EmployeeShift, a robust ASP.NET Core Web API designed to manage employee shifts and administrative functions efficiently. This backend interfaces with a ReactJS frontend, providing seamless data flow and comprehensive management system capabilities.

## Technologies Used

- **Back-End**: ASP.NET Core Web API
- **Database**: MySQL (Configured using MySQL Workbench)

## Project Overview

The Employee Shift Management system backend supports the management of employee schedules and work hours for companies. It features dedicated interfaces for both managers and employees, facilitating efficient shift management and attendance tracking.

## Key Features and Functionalities

### Manager Interface:
- **Secure Login**: Authentication for managers to access the system.
- **Employee Registration**: Functionality to register new employees, including personal details, workdays, shifts, and wage settings.
- **Employee Overview**: Allows managers to view all employees and detailed information like shifts, personal info, and attendance records.
- **Profile Management**: Managers can view and edit their personal details.
- **Data Management**: Ability to directly edit employee details from their profiles.

### Employee Interface:
- **Login Functionality**: Simple login mechanism where employees can access their shift details.
- **Attendance Tracking**: Employees can register their arrival, log working hours, and the system ensures accuracy against the scheduled shifts.
- **Work Status Monitoring**: Displays ongoing work status and allows updates or sign-offs.

## Technical Stack

- **ASP.NET Core Web API**: Manages business logic, database operations, and server-side data processing.
- **MySQL**: Database is set up and managed using MySQL Workbench, allowing for visual database design and management.

## Security Features

- **Authentication**: Secure process for manager and employee logins to safeguard data integrity and privacy.
- **Data Validation**: Implements server-side validation to prevent SQL injection and ensure data accuracy.

## Installation and Setup

Ensure you have .NET 5.0 SDK or later and MySQL Server installed before proceeding.
This project was built based on >NET 9.0

### Steps to Setup:

1. **Clone the repository**:
2. **Navigate to the project directory**:
3. **Restore dependencies**:
4. **Configure Database Connection**:
- Configure your MySQL database using MySQL Workbench and update the connection string in the `Program.cs` file, utilizing environment variables (`MYSQL_USER` and `MYSQL_PASSWORD`) for security.
- You can also delete this approach and implement your own without environmental variables.
5. **Apply database migrations**:
6. **Start the server**:
7. **API Availability**:
- Check where the API will be available by referring to the console output.

### Using .txt Files for Database Setup

If you prefer to set up the database manually or connect to an external database, SQL scripts are provided as `.txt` files in the project directory.

1. **Locate SQL Scripts**:
- Navigate to the `database-schema.txt` file.

2. **Run SQL Scripts**:
- Open the file, copy its content, and execute it in MySQL Workbench or a MySQL command-line client to set up the database schema.

3. **Update Configuration**:
- Modify the connection string in the `Program.cs` file to match your external database setup.

## Directory Structure

- **Services**: Contains service implementation files and an interfaces folder for defining service contracts.

- **Models**: Contains all entity models used by Entity Framework for database operations.

- **Migrations**: Contains Entity Framework migrations files for database schema management.

- **Errors**: Contains custom error handling implementations (e.g., global error handling).

- **Dto**: Data Transfer Objects used to encapsulate data and send it from the server to clients.

- **Data**: Includes the database context class for Entity Framework and other data management classes.

- **Controllers**: Contains API controllers that handle HTTP requests and responses.

## Swagger for API Testing

Swagger is configured for testing and interacting with the API. Navigate to the project's URL and port number (e.g., `http://localhost:5000/swagger`) to access the Swagger UI, where you can test API endpoints directly.

## Required Packages

Ensure the following packages are installed:

- `Microsoft.EntityFrameworkCore`
- `Pomelo.EntityFrameworkCore.MySql`
- `Microsoft.EntityFrameworkCore.Design`
- `Swashbuckle.AspNetCore` for Swagger integration

These packages are necessary for database operations, migrations, and API documentation.

## Contributing

We encourage contributions from the community. Whether it's fixing bugs, improving documentation, or suggesting new features, your input is highly valued!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Contact

Mario Caushi – [caushimario321@gmail.com](mailto:caushimario321@gmail.com)

Project Link: [https://github.com/MarioCaushi/EmployeeShifts-backend](https://github.com/MarioCaushi/EmployeeShifts-backend)
