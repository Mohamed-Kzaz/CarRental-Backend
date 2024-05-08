Car Rental ASP.NET RESTful API
Overview
The Car Rental ASP.NET RESTful API is a project designed to facilitate the rental process for users who can act both as car owners and clients within the same account. It allows users to list their cars for rent, make rental requests, and manage both aspects seamlessly.

Features
Car Listing: Users can list their cars for rent, providing details such as make, model, year, and rental price.
Rental Requests: Users can make rental requests for other cars, specifying the desired rental period.
Request Management: Users can view and manage rental requests they've made and received, including accepting or rejecting them.
Security: Utilizes JWT (JSON Web Tokens) for authentication and authorization, ensuring secure access to the system.
Architecture: Implemented using the Onion Architecture, providing a modular and scalable structure.
Repository Pattern: Utilizes the Generic Repository Pattern for data access, enhancing code maintainability and flexibility.
Unit of Work Pattern: Implements the Unit of Work Pattern to manage transactions and ensure data consistency.
Technologies Used
Framework: ASP.NET (RESTful API)
Version: .NET 7
Database: SQL Server
ORM: Entity Framework Core
Getting Started
To run the Car Rental ASP.NET RESTful API locally, follow these steps:

Clone Repository: git clone https://github.com/yourusername/car-rental-backend.git
Install .NET 7 SDK: Download and install .NET 7 SDK
Set Up Database: [Provide instructions for setting up the database schema and configurations]
Configure Environment: Set up environment variables for database connection, JWT secret, etc.
Run the Application: dotnet run (or equivalent command)
Usage
User (Car Owner/Client):
Sign up/login using the same account.
List your cars for rent.
Browse available cars and make rental requests.
Manage rental requests made and received.
