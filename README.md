# Keycloak.NET Client Library

[![NuGet](https://img.shields.io/nuget/v/Keycloak.NET.Client)](https://www.nuget.org/packages/Keycloak.NET.Client/)
[![NuGet](https://img.shields.io/nuget/dt/Keycloak.NET.Client)](https://www.nuget.org/packages/Keycloak.NET.Client/)
[![GitHub](https://img.shields.io/github/license/karlobencic/keycloak-net-client)](./LICENSE)

A modern and lightweight C# library designed for seamless integration with the Keycloak REST API, leveraging the latest .NET features for a clean and efficient developer experience. While other Keycloak libraries may offer a broader feature set, Keycloak.NET focuses on providing a streamlined and highly performant solution for common Keycloak interactions, making it ideal for projects where speed and simplicity are paramount.

## Features

* Supports [Keycloak Admin REST API](https://www.keycloak.org/docs-api/latest/rest-api/index.html) versions 17+
* **HttpClientFactory Integration:**  Leverages `HttpClientFactory` for efficient and reliable HTTP communication.
* **Separate Clients:**  Provides dedicated client classes for different Keycloak features (e.g., Users, Roles) for
  better organization and SOLID principles.

## Getting Started

### Installation

Install the library via NuGet:

```
Install-Package Keycloak.NET.Client
```

### Usage

1. **Register `KeycloakClientUtility` in your DI container:**

   ```csharp
   // In your DI setup (e.g., Startup.cs in ASP.NET Core)
   services.AddHttpClient();
   services.AddTransient<IKeycloakClientUtility, KeycloakClientUtility>();
   ```

2. **Use the client methods:**

   ```csharp
   // Authenticate client and get a protection API token
   var pat = await keycloakClient.GetProtectionApiToken(
       new GetProtectionApiTokenRequest(
           EndpointAddress: "https://your-keycloak-server",
           RealmName: "your-realm",
           ClientId: "your-client-id",
           ClientSecret: "your-client-secret"
       )
   );
   // Example: Get a user by ID
   var user = await keycloakClient.Users.GetUserById(
       new GetUserByIdRequest(
           EndpointAddress: "https://your-keycloak-server",
           RealmName: "your-realm",
           ProtectionApiToken: pat.Token,
           UserId: "00000000-0000-0000-0000-000000000000"
       )
   );
   ```
