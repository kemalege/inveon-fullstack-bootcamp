# SmartShelf - Library Management System

SmartShelf is a .NET MVC-based library management system designed for administrators and authenticated users to manage books, user roles, and user information efficiently. The project uses **Entity Framework Core**, **ASP.NET Core Identity**, and **Docker** for database management.

---

## **Screenshots & Pages**

### **1. Home Page**
![Home Page](https://res.cloudinary.com/djhvhao4u/image/upload/v1734431531/inveonBootcamp-week-2/welcoem.png)  
**URL**: `/`  
The home page welcomes users. If logged in, the user's name is displayed. There are navigation options for "Kitap Listesi" (Book List) and Login/Register buttons.

---

### **2. Book List**
![Book List](https://res.cloudinary.com/djhvhao4u/image/upload/v1734431530/inveonBootcamp-week-2/book_list.png)  
**URL**: `/Book/Index`  
A list of all available books is displayed with the following information:
- **Başlık**: Book Title
- **Yazar**: Author
- **Yayın Yılı**: Publication Year
- **ISBN**: ISBN Number

Users can click the book title to view the details.

---

### **3. Book Details**
![Book Details](https://res.cloudinary.com/djhvhao4u/image/upload/v1734431530/inveonBootcamp-week-2/book_details.png)  
**URL**: `/Book/Details/{id}`  
Detailed information about the selected book, including:
- **Title**, **Author**, **Publication Year**, **ISBN**, **Genre**, **Publisher**, **Page Count**, **Language**, **Summary**, and **Available Copies**.

The "Geri" button navigates back to the book list.

---

### **4. Register Page**
![Register Page](https://res.cloudinary.com/djhvhao4u/image/upload/v1734431530/inveonBootcamp-week-2/register.png)  
**URL**: `/Account/Register`  
Allows new users to register with the system by providing:
- **Email**, **Password**, **Confirm Password**, and **City**.

---

### **5. Login Page**
![Login Page](https://res.cloudinary.com/djhvhao4u/image/upload/v1734431530/inveonBootcamp-week-2/login.png)  
**URL**: `/Account/Login`  
Allows existing users to log in using their **Email** and **Password**.

---

### **6. User List (Admin Only)**
![User List](https://res.cloudinary.com/djhvhao4u/image/upload/v1734431531/inveonBootcamp-week-2/user_list.png)  
**URL**: `/User/List`  
Only accessible to **admin users**.  
The list of registered users with actions:
- **Detaylar**: View user details.
- **Sil**: Delete the user.
- **Rolleri Yönet**: Manage user roles.
- **Güncelle**: Update user information.

---

### **7. User Details**
![User Details](https://res.cloudinary.com/djhvhao4u/image/upload/v1734431531/inveonBootcamp-week-2/user_details.png)  
**URL**: `/User/Details/{id}`  
Displays detailed information about a selected user, including:
- **UserName**, **Email**, **City**, **User Type**, and **Gender**.

---

### **8. Update User Information**
![Update User](https://res.cloudinary.com/djhvhao4u/image/upload/v1734431531/inveonBootcamp-week-2/update_user.png)  
**URL**: `/User/Edit/{id}`  
Allows admin users to update user information such as:
- **User Name**, **Email**, **City**, and **Gender**.

---

### **9. Manage User Roles**
![Manage Roles](https://res.cloudinary.com/djhvhao4u/image/upload/v1734431531/inveonBootcamp-week-2/role_management.png)  
**URL**: `/User/RoleManagement/{id}`  
Admin users can:
- Add a new role to the user.
- Remove existing roles.

---

### **10. Access Denied**
![Access Denied](https://res.cloudinary.com/djhvhao4u/image/upload/v1734431530/inveonBootcamp-week-2/access_denied.png)  
**URL**: `/Account/AccessDenied`  
Displayed when an unauthorized user tries to access admin-only pages.

---

## **How to Run the Project**

### **Prerequisites**
1. Install [Visual Studio](https://visualstudio.microsoft.com) or any C# compatible IDE.
2. Install [Docker Desktop](https://www.docker.com/products/docker-desktop).
3. Ensure `dotnet` CLI is installed (version 6.0 or later).

---

### **Steps to Run**

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/username/SmartShelf.git
   cd SmartShelf
   ```

2. **Run SQL Server Docker Container**

   Execute the following command to run a SQL Server instance in a Docker container:

   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPassword123!" -p 1433:1433 --name sql-server -d mcr.microsoft.com/mssql/server:2022-latest
   ```

---

3. **Apply Migrations**

   Use the following command to apply Entity Framework migrations:

   ```bash
   dotnet ef database update
   ```

---

4. **Run the Application**

   Run the .NET Core application using:

   ```bash
   dotnet run
   ```

---

5. **Credentials**

   For testing purposes, use the following credentials:

    - **Email:** `kemalege@gmail.com`
    - **Password:** `Pass*123`

---


### **Technologies Used**

- **.NET Core MVC**
- **Entity Framework Core**
- **ASP.NET Identity**
- **Docker Desktop (SQL Server)**
- **Bootstrap 5**

---

