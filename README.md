# Task Management Web Application

This is a **Task Management Web Application** built using **ASP.NET Core MVC** with **Identity-based Authentication and Authorization**. It features an **Admin Dashboard** and **User Panel** to allow both admins and users to manage tasks. The application has been deployed on Somee.

## Features

### 1. Role-Based Access Control
- **Admin**: Can manage (CRUD) all users and view or manage all tasks.
- **User**: Can only view and manage their own tasks.

### 2. Admin Dashboard
- Perform CRUD operations on user accounts.
- Manage all tasks across users.

### 3. User Panel
- Users can create, view, update, and delete their own tasks.

### 4. Authentication & Authorization
- Secured with ASP.NET Core Identity to enforce role-based access control.
  
### 5. Responsive Design
- Styled using Bootstrap for a clean, user-friendly interface that works across devices.

### Usage

- **Admin Login**:
  - **Email**: `admin@example.com`
  - **Password**: `AdminPassword123!`

  *(Note: Modify credentials as required)*

### Deployment on Somee

This application has been successfully deployed on [Somee](https://somee.com/). Follow their documentation for specific deployment steps if deploying from scratch.

## Technologies Used

- **Backend**: ASP.NET Core MVC, ASP.NET Core Identity
- **Frontend**: Bootstrap, HTML, CSS
- **Database**: SQL Server
- **Hosting**: Somee

