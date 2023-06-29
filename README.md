# UrbanWatch

> This pet project is an ASP.NET Core application that uses different technologies and frameworks to show real-time locations of public transportation on a map.

![Presentation Image](https://github.com/e-savca/UrbanWatch/blob/86b9745bd521628a83e6dfa43fbb8c22f4d8107a/docs/img/presentation.jpg)

## Description
The main purpose of this project is to demonstrate my software development skills and explore new technologies and concepts.

### **Inspiration**
I was inspired to create this project to test my abilities in ASP.NET Core and learn new technologies. The challenge was to integrate real-time data, like the positions of public transportation, into an application and display them to users. Additionally, this project helps me quickly find the real-time locations of public transportation, which is personally beneficial.

### **Technical Challenges**
During the development process, I faced several technical challenges. It was my first time working with AutoMapper, so I had to do additional research to successfully implement it. Additionally, I had to establish a connection to the database using Entity Framework, which was a new experience for me.

### **Reliability and Performance**
To ensure the application's reliability and performance, especially when dealing with real-time data, I implemented a class called **`DataSnapshot`**. This class temporarily stores the data until new data is obtained from the data providers. Every 10 seconds, it checks if the data source (**`TranzyDataProvider`**) has updated its data. It also verifies that the new data does not contain duplicates with the already saved data. Finally, the new data is added to the database.

## Features and Functionalities
The application offers the following key features and functionalities:

- Retrieving data from an HTTP Web API
- Displaying the acquired data on an OpenStreetMap using the LeafletJS library
- Showing the stations and route path of the selected route on the map
- Tracking the real-time positions of public transportation for the selected route

Moreover, the project can be configured to use an Entity Framework (EF) database or work solely with in-memory data.

## **Architecture**

The UrbanWatch project has a flexible architecture that supports different data storage options. It can be configured to use either an Entity Framework (EF) database or work exclusively with in-memory data.

### **With Database**

When set to use a database, the project utilizes Entity Framework (EF) to connect to a database backend. This enables efficient storage, retrieval, and querying of data. The database can store essential information like routes, stops, and real-time vehicle positions.

This approach offers benefits such as persistent data storage, scalability, and advanced querying capabilities. It ensures data integrity and allows for smooth integration with existing database systems.
![Type Dependencies Diagram for UseDatabase True](https://github.com/e-savca/UrbanWatch/blob/dfd820bf62e7b0879abd17a7af6f4ad2d900143c/docs/img/Type%20Dependencies%20Diagram%20for%20UseDatabase%20True.png)
### **In-Memory Data**

Alternatively, the project can be configured to work solely with in-memory data. In this mode, data is stored and managed within the application's memory during runtime. This option is suitable for scenarios where data persistence is not required, or when quick prototyping or testing is the main focus.

Using in-memory data provides faster access and eliminates the need for external database dependencies. However, please note that any changes made to the data will not persist beyond the application's runtime.

By offering the flexibility to choose between a database-driven approach and in-memory data, the UrbanWatch project empowers developers to adapt the architecture to their specific requirements and limitations.
![Type Dependencies Diagram for UseDatabase False](https://github.com/e-savca/UrbanWatch/blob/dfd820bf62e7b0879abd17a7af6f4ad2d900143c/docs/img/Type%20Dependencies%20Diagram%20for%20UseDatabase%20False.png)
## Screenshots

Include some representative screenshots of the application here.
Screen 1
![Screen 1](https://github.com/e-savca/UrbanWatch/blob/dfd820bf62e7b0879abd17a7af6f4ad2d900143c/docs/img/screen1.jpg)
Screen 2
![Screen 2](https://github.com/e-savca/UrbanWatch/blob/dfd820bf62e7b0879abd17a7af6f4ad2d900143c/docs/img/screen%202.jpg)



## Technologies Used

- C#/ASP.NET Core 
- HTML/CSS
- JavaScript

## Installation and Usage

1. Clone this repository: `git clone https://github.com/e-savca/UrbanWatch.git`
2. Navigate to the project directory: `cd UrbanWatch`
3. Install dependencies: `npm install`
4. Run the project: `npm start`

## License

This project is licensed under the [MIT License](LICENSE).
