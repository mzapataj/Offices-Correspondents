# Offices Correspondents

This project consists in two part, one is a web services which is consumed by a Razor web site project.

## Solution
### Web server
![image](https://user-images.githubusercontent.com/16918921/203468127-e90c5cde-cd95-42f7-bbe6-852e5b8ef7c2.png)

It has direct conection with a SQL Server database using ASP Core 6 and Entity Core 6 with a basic CRUD for two entities, Offices and Correspondents. the following image represents relational model database

![image](https://user-images.githubusercontent.com/16918921/203468404-51a9a2ec-548b-434b-b0ab-2363505ad20d.png)


### Web Site
![image](https://user-images.githubusercontent.com/16918921/203468583-35c4fa34-cbf6-4f33-8805-0dcba0cdb7f6.png)
*Home Page*

This has a front-end built with Razor and Bootstrap 5 with three modules:

* **Corresponsales:** List with Correspondent entities and CRUD operations
![image](https://user-images.githubusercontent.com/16918921/203469406-b319bc14-a124-48e3-aad5-700cfc005877.png)

* **Oficinas:** List with Office entities and CRUD operations
![image](https://user-images.githubusercontent.com/16918921/203469419-f81c120b-d41f-4bfd-9bf6-ccc569d76003.png)

* **Prueba:** Has a special functionality which consists in:

1. The Dropdown List has Correspondent name (COR_NOMBRE) followed by a offices count which is related with each Correspondent.
2. In right side, it has a list according selected option in Dropdown list with each character counts.

![image](https://user-images.githubusercontent.com/16918921/203469599-0f8d26b9-1da4-404d-a55e-ee661e78c39c.png)
