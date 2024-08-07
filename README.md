# Safary Application

## Overview
This project is an ASP.NET Core API and React Next.js application that allows users to choose a trip, select an appropriate tour guide, and make payments. The backend is built using ASP.NET Core, while the frontend uses React with Next.js. The application implements several advanced features to ensure security, maintainability, and performance.

## Features
- **Identity Management**: User authentication and authorization.
- **Onion Architecture**: Decouples the core business logic from the infrastructure and presentation layers.
- **Repository Pattern**: Provides a clean abstraction for data access.
- **Health Checks**: Monitors the health and availability of the application.
- **Rate Limiting**: Controls the rate of requests to prevent abuse.
- **JWT Authentication**: Secure authentication mechanism using JSON Web Tokens.
- **Email Notifications**: Sends emails to users for various actions.
- **Trip Management**: Allows users to choose trips, select tour guides, and make payments.

## Technologies Used
- **Backend**: ASP.NET Core
- **Frontend**: React with Next.js
- **Database**: [SQL Server]
- **Authentication**: Identity and JWT
- **Email Service**: [EmailServices]
- **Deployment**: [Monster Hosting]

## Getting Started

### Prerequisites
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or any preferred database

### Setup

1. **Clone the repository:**
    ```sh
    git clone https://github.com/yourusername/yourproject.git
    cd yourproject
    ```

2. **Backend Setup:**
    - Navigate to the `backend` directory:
        ```sh
        cd backend
        ```
    - Update `appsettings.json` with your database connection string and email service configuration.
    - Apply database migrations:
        ```sh
        dotnet ef database update
        ```
    - Run the backend server:
        ```sh
        dotnet run
        ```

3. **Frontend Setup:**
    - Navigate to the `frontend` directory:
        ```sh
        cd ../frontend
        ```
    - Install dependencies:
        ```sh
        npm install
        ```
    - Run the frontend development server:
        ```sh
        npm run dev
        ```

## Contributing
- Contributions are welcome! Please submit a pull request or open an issue to discuss any changes.

