# PerfumeShop (client-server application) 

### Description 

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

### What has been implemented?

- Database
  - Database connection
  - Database initializer
  - Model сonfigurations
  - Value comparers for property which mapped through a value converter
  - Value conversions
  - Enum as database table
  - Generic repository and unit of work
    
- Data storage in the cookies
- Data storage in the sessions
- Exception handling middleware
- Logger
- Automated mapping properties
- Notifications

- Identity
  - Authentication with external services
  - User authentication and registration
  - Using user roles

- Account management
  - Edit user information
  - Updating password

- Store administration
  - Management of products
  - Management of physical store information
  - Loading and deleting a picture

- Catalog
  - Pagination
  - Items per page
  - Catalog's filter
  - Product's details

- Basket
  - Basket for anonymous and authorization user
  - Transferring anonymous user's cart to authorized user
  - Basket management
  - Products limit

- Order
   - Different ordering options (by pickup, by courier)
   - Different payment options (cash, card)
     
- Payment through external API (Stripe)
- Sending messages to email







