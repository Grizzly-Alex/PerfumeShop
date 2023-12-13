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
- Discount system

## Application functionality and operating principle

### Home page
The home page displays discounted products. An anonymous user can add a product to the cart and view product details.
Navbar have catalog link, brands link (in developing), login and basket.

![image](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/64b59bb1-3c70-4ae6-9a0f-d0fe5f0dc204)

### Catalog page
The user can change the number of products on the catalog page.
You can add a product to your cart and view a detailed product description.
Some product cards have a label. If a product is out of stock, it is marked as "Sold out".
If a product is on discount price, it is marked with the "Sale" tag.

- Filter functionality:
  - Automatic determination of minimum and maximum prices.
  - Show products which have discounted only.
  - Reset filter
  - Filter by brand, gender, aroma type, release form
    
![image](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/3feefab0-0683-4eb8-9bf8-ed0fa3df33c1)


### Product detail page
This page show detailed product information.
The user can add the required number of products to the cart, but the quantity is limited by the number of products available in the stock.

![image](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/0a1b4ff9-39d0-4dba-804d-1122566016e9)
![image](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/4dd30e56-31aa-4b67-9e16-12a6c56692f2)
![image](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/9f6123f3-f30e-4b3d-bb23-a5326567a902)


### Basket page
- Basket functionality:
  - Updating product quantity.
  - Deleting product.
  - Clear basket

Shopping cart will be saved in cookie for anonymous user but for authorized users shopping cart will be saved in data base.
Clicking on a product icon will take you to the product details page.

![image](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/a6d2d0df-92d6-445e-b317-440d43fb9ca4)

### Login page
The user can log in using his name and password if he was previously registered or used external authentication.

![image](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/c9d43669-6c11-4d9c-9a80-87e2cc69e99d)

### Register page
The user can register on the site or use external services.

![image](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/8a3dfce0-5f9e-4721-9ed9-847168413908)

### External registration
Demonstration of registration using Google but you can use Microsoft, Twitter or Facebook services. 

![1](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/8590e07b-4cab-4894-8e7c-8958fc5ef2dd)

After successful authentication through an external service, the user must associate his mailbox in the application.
If the email has not previously been registered on the site, the association will be successful.

![external4](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/8f0a33a5-c4fd-4fda-a66f-9db063510c47)

After successful registration, the user will be sent an email to confirm the account.

![external5](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/ca39a803-c402-4d5a-9263-540c9440167e)
![external6](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/7601bbd2-0e79-4074-aa47-f50e1b6b966f)
![external7](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/2f90e8d3-b90b-437f-a7d2-bef38f1ffae0)

### Checkout page
Demonstration of purchasing a product on the website. The user can select the payment method and delivery method.
If the user chooses delivery by courier, then the user needs to give his address, but if the delivery method is pickup,
then the user needs to select the store to which the order will be delivered.
The user can pay immediately or after delivery in cash. In this case, the order will be marked as unpaid.

![image](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/895662a3-7e5a-4d9e-8716-2d590a77c776)
![image](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/ae854850-0a24-46bd-a74e-e6f09eb72222)

### Payment page
The user can pay for the order via application. For Payments the application uses stripe API.
For tests I used a test card. 
[Link guid](https://stripe.com/docs/testing)

![image](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/33cdc214-f474-4964-945b-3e5b61ca4232)

Demonstration of a paid order via stripe:

![stripe](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/46f79b33-c456-4874-9484-2d8ef6ffee71)


### Orders success page
After placing an order, the application will send the user an email with order details.
User can view the order details in their account.

![ordersuccess](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/16fdd623-487f-4be9-ad86-0ef9e0c95b9e)
![mail](https://github.com/Grizzly-pride/PerfumeShop/assets/35379801/a2d1e525-af4c-43a2-9087-77499b683438)











