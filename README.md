# Currency-Convertor
Currency Converter API
Objective
Design and implement a robust, scalable, and maintainable currency conversion API using C# and
ASP.NET Core, ensuring high performance, security, and resilience.
Requirements
1. Endpoints
1.1 Retrieve Latest Exchange Rates
● Fetch the latest exchange rates for a specific base currency (e.g., EUR).
1.2 Currency Conversion
● Convert amounts between different currencies.
2. API Architecture & Design Considerations
2.1 Resilience & Performance
● Implement caching to minimize direct calls to the Frankfurter API.
● Use retry policies with exponential backoff to handle intermittent API failures.
● Introduce a circuit breaker to gracefully handle API outages.
2.2 Extensibility & Maintainability
● Implement dependency injection for service abstractions.
● Design a factory pattern to dynamically select the currency provider based on the request.
● Allow for future integration with multiple exchange rate providers.
2.3 Security & Access Control
● Implement JWT authentication.
● Enforce role-based access control (RBAC) for API endpoints.
● Implement API throttling to prevent abuse.
2.4 Logging & Monitoring
● Log the following details for each request:
○ Client IP
○ ClientId from the JWT token
○ HTTP Method & Target Endpoint
○ Response Code & Response Time
● Use structured logging (e.g., Serilog with Seq or ELK stack).
● Implement distributed tracing (e.g., OpenTelemetry).

