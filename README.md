Inventory Management System
Welcome to the Inventory Management System repository! This system is designed to help businesses manage their inventory efficiently 
using modern architectural patterns and technologies. 
Built with .NET Core 9, this project is entirely API-based and incorporates advanced features like CQRS, Vertical Slicing, Autofac, Hangfire,
and RabbitMQ. Below, you'll find setup instructions, features, and other relevant information to get you started.
Features
1. User Authentication
Secure JWT-based authentication and authorization.

Role-based access control (Admin, Manager, Employee).

1. Inventory Management
Add, update, and delete products.

Track product quantities and categories.

Low stock alerts.

2. Order Management
Create and manage customer orders.

Track order status (Pending, Shipped, Delivered).

3. Reporting and Analytics
Generate sales reports.

View inventory trends and insights.

4. Background Processing with Hangfire
Archive stock transactions automatically.

Schedule recurring tasks for inventory updates.

5. Event-Driven Communication with RabbitMQ
Send email notifications when product quantities decrease.

Decouple services for better scalability.

6. Search and Filter
Easily search for products or orders.

Filter by category, date, or status.

Technologies Used
Backend: .NET Core 9


Message Queue: RabbitMQ

Background Job Processing: Hangfire

Dependency Injection: Autofac

Authentication: JWT (JSON Web Tokens)

API Documentation: Swagger/OpenAPI

Architecture and Design Patterns
1. Vertical Slicing
The project is organized around features rather than layers, ensuring better modularity and maintainability.

2. CQRS (Command Query Responsibility Segregation)
Separates read and write operations for better performance and scalability.

Commands handle write operations (e.g., adding a product), while queries handle read operations (e.g., fetching product details).

3. Event-Driven Architecture
RabbitMQ is used to handle asynchronous communication between services, such as sending email notifications when product quantities decrease.

4. Background Job Processing
Hangfire is used to schedule and manage background tasks, such as archiving stock transactions.

5. Dependency Injection
Autofac is used for dependency injection, ensuring loose coupling and testability.
Contact
If you have any questions or need further assistance, feel free to reach out:

Mohamed Shaqrani

Email: mohamed.shaqrani@example.com

GitHub: mohamed-shaqrani
Thank you for using the Inventory Management System! We hope it helps streamline your inventory processes. 🚀

