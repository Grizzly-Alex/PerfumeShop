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

### Auxiliary services and libraries

- Notifications (SweetAlert2, Toastr)
- Payment (Stripe)
- Databases (MSSQL)
- ORM (Entity Framework Core)
- Authorization and authentication (ASP.NET Core Identity)
- External authentication (Facebook, Google, Microsoft and Twitter NuGet Packages)
- Property mapping (AutoMapper)
- Mail sender (MailKit)
- Logger (Serilog)
- Guard clause help methods (Ardalis)
- HTML & CSS (bootstrap 5)
- Text editor (TinyMCE)
- Js tables (CloudTables)


