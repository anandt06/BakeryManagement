# BakeryManagement
Coding Challenge: ASP.NET 8 REST Service with LiteDB for a San Francisco Sourdough Bakery

Objective:
Develop a RESTful service using ASP.NET 8 that utilizes LiteDB as the backend database. The service will manage the operations of a sourdough bakery, including the handling of ingredients, baking schedules, and customer orders.

Requirements:
1. Use the provided base project as your starting point
2. Integrate LiteDB, a lightweight NoSQL database, to store and manage data.
3. Create REST endpoints to perform CRUD operations on bakery items, ingredients, and orders. (You can be flexible on how in-depth you want to make this, but implement at least two types of objects)
4. Ensure the service can handle concurrent customer order requests without data conflicts.
5. Include endpoint security to protect sensitive data and operations (This can be implemented with psuedo code comments)

Deliverables:
- A fully functional REST service with the specified features.
- Documentation

Evaluation Criteria:
- Code organization and readability.
- Correct implementation of REST principles.
- Effective use of LiteDB for data management.
- Security measures implemented for the service (even if they are psuedo)
- Clarity and completeness of documentation.

This challenge is designed to assess your skills in backend development, database integration, and API design. It will also test your ability to create a system that can handle real-world operational demands of a bakery business. 

Good luck, and may your code be as resilient and well-crafted as the finest sourdough!



Solution Implementation:

The Implementation has been done using Clean Architecture. Folder based seggregation has been done instead of project based seggregation
Token Authentication Implemenatation has been implemented for Authorization for which key has been given in app.json as "Anand"
The code has been well documented to increase readability.
Startup.cs class has been introduced along with Program.cs and Program.cs sole responsibility is used to start the application
Two Controllers are introduced to manager order and bakeryitem respectively
File database will be created within project with name SFSourdoughBakery.db in same location as of project