# CinemaApi Project

## The latest updates
- the filtering for upcoming showings and relevant classes were created.

## Description

CinemaApi is a small-scale, demonstrational RESTful web API project designed to manage a database of cinemas, movies, and showings. It enables Create, Read, Update, and Delete (CRUD) operations, allowing users to interact with essential cinema-related data. This project serves as an introductory example of how a web API might handle and present information typically associated with cinema listings.

## Getting started
To run the project locally, the following steps are required:
1. Install .NET 6.0 (https://dotnet.microsoft.com/download).
2. Install the required packages:
- Entity Framework Core;
- Pomelo.EntityFrameworkCore.MySql;
- Serilog for logging (logs are written to a text file and console).
3. Set up a MySQL database. For this project, the database is hosted on: https://www.smarterasp.net/.  
4. Update the `appsettings.json` file with your MySQL connection string.
5. Run the application.

## API Endpoints
The API supports the following endpoints for:

### Cinemas
- `GET /api/cinemas`: List all cinemas.
- `GET /api/cinemas/{id}`: Retrieve a cinema by ID.
- `POST /api/cinemas`: Create a new cinema record.
- `PUT /api/cinemas/{id}`: Update a cinema record by ID.
- `DELETE /api/cinemas/{id}`: Delete a cinema record by ID.

### Movies
- `GET /api/movies`: List all movies.
- `GET /api/movies/{id}`: Retrieve a movie by ID.
- `GET /api/movies/MoviesSearch`: Search for movies with created filters such as titles in LT and original language, release year, and genre.
- `POST /api/movies`: Create a new movie.
- `PUT /api/movies/{id}`: Update a movie by ID.
- `DELETE /api/movies/{id}`: Delete a movie by ID.

### Screenings
- `GET /api/screenings`: List all screenings.
- `GET /api/screenings/{id}`: Retrieve a screening by ID.
- `GET /api/screenings/Upcoming showings`: Filter upcoming showings based on criteria like city, show date, auditorium or genre.
- `POST /api/screenings`: Create a new screening record.
- `PUT /api/screenings/{id}`: Update a screening by ID.
- `DELETE /api/screenings/{id}`: Delete a screening record by ID.

## Service Configuration
The `ServiceCollectionExtensions` class was created for configuring services and repositories. It handles the registration of all necessary components for dependency injection, ensuring that the application runs smoothly. This includes setting up services for managing cinemas, movies, and screenings, as well as all the necessary data conversion tools.

## Error Handling
The created API uses standard HTTP status codes to indicate the success or failure of a request.
- `200 OK`: The request was successful.
- `400 Bad Request`: The request was invalid. Check your request format and parameters.
- `404 Not Found`: The requested resource was not found.
- `500 Internal Server Error`: An error occurred on the server side.

## Middleware
ErrorHandlerMiddleware validates JSON request formats and handles exceptions, ensuring that a proper HTTP response status codes and error messages are returned to the client.

## DTOs
DTOs for Cinema, Movie, and Screening have been implemented to reflect the business logic. They ensure accurate representation of time and date, facilitate conversion of Enum IDs or database IDs to readable values, and enhance overall data interaction and readability. Along with DTOs, the project includes converter classes like CinemaDtoConverter, MovieDtoConverter, ScreeningDtoConverter and others. These classes are created to transform and map entity data to DTOs and back, ensuring that the information is structured and presented consistently and clearly throughout the application.

## Decorators
In this project, decorator classes have been used to enhance the fundamental service capabilities for CRUD operations on movies, cinemas, and screenings. These decorators specifically introduce error handling and logging functionalities, enriching the base services without altering their original functionality and ensuring a streamlined, maintainable structure.

## Authors and acknowledgment
Creator: Agnė Grinevičienė (agnegrineviciene1@gmail.com).

## Project status
The project is under continuous development with the current version being 0.0.9. Updates will be added and described accordingly.
