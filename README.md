# 🎓 Schooly

A full-featured **School Management Platform** developed as a graduation project. It’s built using **ASP.NET Core Web API** with **Clean Architecture**, **CQRS**, **Entity Framework Core**, and integrates **AI-based facial recognition** for authentication.

---

## 📅 Project Duration

**October 2024 – July 2025**

---

## 🚀 Live Demos

- 🔗 **Backend API Docs**: [[https://schoolly.runasp.net/swagger/index.html](https://schoolly.runasp.net/swagger/index.html)
- 🔗 **Frontend Demo**: [https://schooly.vercel.app/](https://schooly.vercel.app/)
- 🧠 **Face Recognition Repo**: [https://github.com/abdsaber000/Face-Recognition](https://github.com/abdsaber000/Face-Recognition)

---

## 🔐 Test Accounts

### 👨‍🏫 Teacher Account
```json
"email": "teacher@example.com",
"password": "string"
```

### 👨‍🎓 Student Account
```json
"email": "maher@example.com",
"password": "string",
```
---

## 🧠 Key Features

### ✅ AI-Based Facial Recognition
- Facial recognition is required when **joining a lesson** to verify the student's identity.
- Uses `RegisterFace` (during registration) and `VerifyFace` (before lesson join) endpoints.
- Integrated with an AI model for real-time identity validation during class participation.

### 🔐 Authentication
- Secure login and registration using email and password credentials.
- JSON Web Token (JWT)-based session management.

### 👥 Role-Based Access Control
- Access levels and UI functionalities tailored to:
  - **Admin**
  - **Teacher**
  - **Student**

### 📚 Modules
- Full CRUD operations for:
  - Classrooms
  - Lessons
  - Homework
  - Posts
  - Comments
- User management (student and teacher)
- Password reset workflow via email
- Real-time video communication powered by Agora SDK

### 🛠️ Services
- 📧 **Email Notifications**: Automated emails for registration, password recovery, and other events.
- 📂 **File and Document Upload**: Local file system or optional cloud storage integration.
- 🌍 **Localization Support**: Multilingual responses based on request headers.
- ⏱️ **Automated Background Jobs**: Scheduled tasks to track and mark lesson completion status.

---

## 🧱 Architecture

- **Clean Architecture** principles with separation of concerns.
- **CQRS (Command Query Responsibility Segregation)** pattern implemented using MediatR.
- Data access through **Entity Framework Core** with support for LINQ.
- Use of **Repository and Generic Repository** patterns.
- API response handling standardized using the **Result Pattern** and centralized **ResponseService**.

---

## 📦 Tech Stack

| Layer            | Technologies                            |
|------------------|-----------------------------------------|
| Backend          | ASP.NET Core Web API, CQRS, MediatR     |
| Database         | SQL Server, Entity Framework Core, LINQ |
| Authentication   | JWT, Facial Recognition (AI)            |
| File Management  | Local File System                       |
| Video Streaming  | Agora SDK                               |
| Background Tasks | Hosted Services                         |
| Notifications    | SMTP Email Service                      |

---

## 🧪 How to Run Locally

### ✅ Prerequisites
- [.NET SDK 8+](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)

### 🔧 Steps

```bash
# Clone the repository
git clone (https://github.com/abdsaber000/Schooly)
cd Schooly

# Configure appsettings.json with your database and email credentials

# Apply EF Core migrations to set up the database
dotnet ef database update

# Run the application
dotnet run


