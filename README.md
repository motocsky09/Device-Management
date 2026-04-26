# Device Management System

A full-stack web application for managing company-owned mobile devices. Built with ASP.NET Core, Angular, and MongoDB.

## Features

- **Device Management** – Create, view, edit, and delete devices
- **User Management** – Register and login with JWT authentication
- **Device Assignment** – Assign or unassign devices to logged-in users
- **AI Description Generator** – Automatically generate device descriptions using Ollama (gemma2:2b)
- **Free-text Search** – Search devices by name, manufacturer, processor, or RAM with relevance ranking

## Tech Stack

- **Backend:** ASP.NET Core (.NET 8), MongoDB
- **Frontend:** Angular 16
- **Database:** MongoDB
- **AI:** Ollama (gemma2:2b)

## Prerequisites

Make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (v18+) and Angular CLI (`npm install -g @angular/cli`)
- [MongoDB](https://www.mongodb.com/try/download/community) running on `localhost:27017`
- [Ollama](https://ollama.com/) with `gemma2:2b` model

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/motocsky09/Device-Management.git
cd Device-Management
```

### 2. Set up the database

Make sure MongoDB is running, then execute the seed scripts using mongosh:

```bash
mongosh Server/Scripts/01_create_collections.js
mongosh Server/Scripts/02_seed_data.js
```

### 3. Start Ollama

Make sure Ollama is running with the gemma2:2b model:

```bash
ollama serve
ollama pull gemma2:2b
```

### 4. Run the Backend

```bash
cd Server
dotnet run
```

The API will be available at `http://localhost:5012`  
Swagger UI: `http://localhost:5012/swagger/index.html`

### 5. Run the Frontend

```bash
cd Client
npm install
ng serve
```

The application will be available at `http://localhost:4200`

## Usage

1. Open `http://localhost:4200` in your browser
2. Register a new account or use the seed data credentials
3. Login to access the device list
4. Use the search bar to find devices by name, manufacturer, processor, or RAM
5. Create a new device and use **Generate Description** to auto-generate a description with AI
6. Assign or unassign devices to your account
