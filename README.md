# Fruityvice

The Fruityvice API is a web application that provides information about various fruits. It allows users to retrieve a list of all fruits or filter fruits by their family. The API consumes data from the Fruityvice external API and presents it in a structured format.

## Features

- Retrieve all fruits: Users can make a GET request to `api/fruit/all` to retrieve a list of all fruits available.

- Filter fruits by family: Users can make a POST request to `api/fruit/family` with a JSON payload specifying the `fruitFamily` property to filter fruits by their family.

## Design Pattern and Techniques

The Fruityvice API follows the following design pattern and techniques:

- Dependency Injection: The API uses dependency injection to decouple components and improve testability. The `IFruitService` interface is injected into the `FruitController`, allowing the controller to depend on an abstraction rather than a concrete implementation.

- SOLID Principles: The API adheres to SOLID principles, such as the Single Responsibility Principle (SRP) and the Dependency Inversion Principle (DIP). The code is structured in a modular and maintainable way, promoting separation of concerns and loose coupling between components.

- Exception Handling: The API handles exceptions gracefully by catching and logging specific exceptions and returning appropriate HTTP status codes and error messages. This ensures that users receive meaningful error responses in case of failures.

- Logging: The API utilizes the ILogger interface from ASP.NET Core to log errors, warnings, and information about the application's behavior. Logs help in troubleshooting and identifying issues during development and production.

- Unit Testing: The codebase includes unit tests that verify the behavior of individual components, ensuring their correctness and expected functionality. The tests use mocking frameworks, such as Moq, to isolate dependencies and focus on the specific component being tested.

- Integration Testing: Integration tests are included to validate the behavior of the API endpoints and their interactions with the external API. These tests cover the entire request/response cycle and verify the integration between components.

## Technologies Used

- ASP.NET Core: The web application is built using the ASP.NET Core framework, which provides a robust and flexible platform for developing web APIs.

- HttpClient: The HttpClient class is used to make HTTP requests to the Fruityvice external API to retrieve fruit data.

- Swagger: Swagger is integrated into the project to provide interactive API documentation. It allows users to explore the available endpoints, view request/response models, and test the API directly from the Swagger UI.

- Unit Testing: NUnit is used for unit testing the application's components, ensuring their correctness and expected behavior.

- Integration Testing: Integration tests are included to validate the functionality of the API endpoints and their interactions with the dependencies.

## Setup

1. Clone the repository to your local machine.

2. Build the solution using Visual Studio or the .NET CLI.

3. Run the application.

4. By default user will be redirected to the Swagger UI.

This project is licensed under the [MIT License](LICENSE).
