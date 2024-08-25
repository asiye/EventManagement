# .NET Core

- **Startup.cs**: Configures services and the request pipeline.
- **Program.cs**: Entry point for the .NET application.
- **EventHub.cs**: SignalR hub for real-time communication.
- **ApplicationDbContext.cs**: Entity Framework Core DbContext.
- **Repositories/**: Data access layer.
- **Services/**: Business logic layer.
- **BlockchainHub.cs**: SignalR hub for blockchain-related real-time updates.

## Blockchain Explorer Integration

- **BlockchainHub.cs**: A SignalR hub designed to handle real-time communication for blockchain-related events, such as new blocks and transactions. This hub pushes updates to all connected clients whenever a new block or transaction is detected.

### How to Use BlockchainHub

1. **Configure SignalR**: Ensure SignalR is properly set up in your application.
2. **Trigger Events**: Use the `BlockchainHub` service to notify clients of new blockchain events.
3. **Client-Side Handling**: Connect to the `BlockchainHub` from your client applications and handle incoming events.

## .NET Core Setup

1. Restore Packages
2. Apply Migrations
3. Run the Application
4. Access the Application

## Common Issues

- **SignalR Connection Issues**: Ensure the SignalR server is running and accessible from the client. Verify CORS settings and transport configuration.
- **Database Connection Issues**: Check your connection strings and ensure the database server is reachable.
- **Port Conflicts**: Ensure that the ports used by Node.js and .NET Core are not conflicting with other services.
