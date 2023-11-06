# F-P

This project is simulation microservice which is used from a web app . Client writes a task to the queue and a task is consumed and simulated by the worker. The tasks processed by the worker are kept in a table in SQLite by updating their durations.


<img width="452" alt="image" src="https://github.com/mustafabingul/F-P/assets/15616065/65eccb20-5cb3-440c-933a-cbdb33e34d1c">

## Setup project

### Install and run RabbitMQ on your Docker

"docker pull rabbitmq:3-management"

"docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management"

### Clone this repo on your local

Open it with an IDE and do DB configs

Run it

### SQlite

Remove Migrations and tables and then create your migration and tables

"dotnet ef database drop"

"dotnet ef migrations add InitialCreate"

"dotnet ef database update"


