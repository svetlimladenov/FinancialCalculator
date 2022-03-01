# Financial Calculator

## About the project

The financial calculator embraces the moder microservice architecture approach.
The calculator consists of few small services

- A User Interface build with Vue.js
- API Gateway build with .NET 5
- Microservices communicating with RabbitMQ as a message broker

## Setup

For easier setup, we run everything in containers so you can just run:

```bash
docker-compose up
```

Visit http://localhost:8080/credit - to access the Vue App, and the Credit Calculator

Visit http://localhost:5000/swagger/index.html - to access the API Swagget Documentation
