# .NET Core

- **Startup.cs**: Configures services and the request pipeline.
- **Program.cs**: Entry point for the .NET application.
- **EventHub.cs**: SignalR hub for real-time communication.
- **ApplicationDbContext.cs**: Entity Framework Core DbContext.
- **Repositories/**: Data access layer.
- **Services/**: Business logic layer.

## .NET Core Setup

1. Restore Packages
2. Apply Migrations
3. Run the Application
4. Access the Application

## Common Issues

- **SignalR Connection Issues**: Ensure the SignalR server is running and accessible from the Node.js client. Verify CORS settings and transport configuration.
- **Database Connection Issues**: Check your connection strings and ensure the database server is reachable.
- **Port Conflicts**: Ensure that the ports used by Node.js and .NET Core are not conflicting with other services.

