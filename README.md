# ClinicManagerAPI 🏥

A complete medical clinic management REST API built with **ASP.NET
Core**, following a layered architecture with JWT authentication,
AutoMapper mapping, entity-specific repositories, and industry-grade
development practices.

------------------------------------------------------------------------

## 🚀 Key Features

-   ✔️ Clean layered architecture: **Controllers**, **Services**,
    **Repositories**, **Data**, **Models**, **DTOs**
-   ✔️ Secure authentication and authorization via **JWT**
-   ✔️ Full management modules for:
    -   Patients
    -   Doctors
    -   Medical Appointments
    -   Medical Records
    -   Allergies
    -   Reports
    -   Users & Roles
-   ✔️ Entity mapping with **AutoMapper**
-   ✔️ Global error-handling middleware
-   ✔️ SOLID-friendly repository pattern
-   ✔️ Built‑in documentation using **Swagger / OpenAPI**

------------------------------------------------------------------------

## 🧱 Project Architecture

    /ClinicManagerAPI
     ├── Configurations/
     ├── Constants/
     ├── Controllers/
     ├── Data/
     ├── Filters/
     ├── Middlewares/
     ├── Migrations/
     ├── Models/
     ├── Repositories/
     │    ├── Interfaces/
     │    └── Implementations/
     ├── Services/
     │    ├── Interfaces/
     │    └── Implementations/
     ├── AutoMapper/
     └── Program.cs

------------------------------------------------------------------------

## ⚙️ Required Configuration

Before running the API, configure your **appsettings.json** with the
following values:

``` json
{
  "JWT:SUBJECT": "ClinicManagerAPI",
  "JWT:KEY": "ff3731d66112c576a41dfee70f434274bc116494afd404ce894016b12663527d",
  "JWT:ISSUER": "https://localhost:8081",
  "JWT:AUDIENCE": "https://localhost:8081",
  "ConnectionStrings:DefaultConnection": "Server=LAPTOP-TTM1MSDD;Database=ClinicManagerDB;Integrated Security=True;TrustServerCertificate=True;"
}
```

------------------------------------------------------------------------

## 🧪 Main Endpoints

<img width="1966" height="1402" alt="image" src="https://github.com/user-attachments/assets/75925cf8-d011-438e-9bc6-d2d0f3425e2c" />

------------------------------------------------------------------------

## 🔐 JWT Authentication

Features include:

-   User login\
-   JWT token generation\
-   Role‑based authorization\
-   Token validation

<img width="2167" height="1332" alt="image" src="https://github.com/user-attachments/assets/46643096-9d6c-4aeb-afd5-60d9966f8757" />

------------------------------------------------------------------------

## 📊 System Modules

  Module            Description
  ----------------- -------------------------------------
  Patients          Full CRUD with relationships
  Doctors           Professional profile & availability
  Appointments      Scheduling, updates, cancellations
  Medical Records   Detailed patient history
  Allergies         Management & associations
  Reports           Aggregated clinical/admin data

<img width="1932" height="1330" alt="image" src="https://github.com/user-attachments/assets/7dc62d5d-4ea2-466b-b044-1b8cca882b79" />

------------------------------------------------------------------------

## 🛠️ Installation & Execution

``` bash
git clone https://github.com/JordyMorenoArias/ClinicManagerAPI.git
cd ClinicManagerAPI
dotnet ef database update
dotnet run
```

Swagger UI will be available at:

    https://localhost:5001/swagger

------------------------------------------------------------------------

## 🤝 Frontend Integration

This backend integrates seamlessly with the MAUI client:

➡️ https://github.com/JordyMorenoArias/ClinicManagerMAUI
