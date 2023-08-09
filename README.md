# PerfumeShop - Web Application 

### General Overview 

The main idea of the project is to develop a perfume shop.
The store has an admin area, a user's personal account and store area.
The project was developed using the ASP.NET Core (.NET 7) framework.
I used MVC pattern and N-layer architecture for my web application.

Layers of architecture: 
- Core *(entity models are stored here, also extension methods, constants, interfaces and value types)*
- Infrastructure *(everything related to business logic and data access)*
- Web *(controllers, view models and page html are located here, it's top level of application)*

I used relational database with EF Core for storage information about products, users and orders,
also Identity for authorization and authentication and used Serilog for to collect logs.
