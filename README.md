# Clinic Appointment Management System (CAM)

A comprehensive web-based application for managing medical clinic appointments, built with ASP.NET Core MVC and Entity Framework Core.

## 🏥 Features

- **Multi-Doctor Clinic Support**: Manage multiple doctors across different clinics with specialized medical fields
- **Flexible Doctor Scheduling**: Configure individual doctor schedules with different working hours for each day of the week
- **Real-Time Appointment Booking**: Dynamic slot availability checking with AJAX-powered interface
- **Patient Management**: Automatic patient registration and deduplication based on contact information
- **Conflict Prevention**: Robust booking system that prevents double-booking and overlapping appointments
- **Responsive Web Interface**: Modern Bootstrap-based UI with mobile-friendly design
- **Database Transactions**: ACID-compliant appointment booking with rollback capabilities

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core MVC (.NET 9.0)
- **Database**: SQL Server with Entity Framework Core 9.0
- **Frontend**: Bootstrap 5, jQuery, JavaScript (ES6)
- **Architecture**: Clean Architecture with Service Layer pattern
- **ORM**: Entity Framework Core with Code-First migrations

## 📋 Prerequisites

- .NET 9.0 SDK
- SQL Server (LocalDB, Express, or Full)
- Visual Studio 2022 or VS Code with C# extension
- Windows OS (for Integrated Security authentication)

## 🚀 Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/clinic-appointment-management.git
cd clinic-appointment-management
```

### 2. Database Configuration
The application uses SQL Server with Integrated Security. Update the connection string in `appsettings.json` if needed:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ClinicDB;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;"
  }
}
```

### 3. Build and Run
```bash
# Restore packages
dotnet restore

# Run database migrations
dotnet ef database update

# Run the application
dotnet run
```

The application will be available at `https://localhost:5001` (or the configured port).

## 📖 Usage

### Secretary Dashboard
- **View Appointments**: Browse daily appointment schedules with patient and doctor details
- **Book Appointments**: Create new appointments through the intuitive web form
- **Real-Time Slot Selection**: Select doctor and date to view available time slots dynamically

### Key Workflows

1. **Patient Registration**: Enter patient details (name, phone, birth date)
2. **Doctor Selection**: Choose from available doctors by specialty
3. **Date & Time Selection**: Pick appointment date and view real-time available slots
4. **Duration Setting**: Configure appointment length (15-120 minutes)
5. **Booking Confirmation**: Secure booking with conflict checking

## 🏗️ Project Structure

```
CAM/
├── Controllers/           # MVC Controllers
│   ├── HomeController.cs
│   └── SecretaryController.cs
├── Models/               # Entity Framework Models
│   ├── Appointment.cs
│   ├── Doctor.cs
│   ├── DoctorSchedule.cs
│   ├── Patient.cs
│   └── ClinicDbContext.cs
├── Services/             # Business Logic Layer
│   └── AppointmentService.cs
├── ViewModels/           # Data Transfer Objects
│   └── AppointmentCreateViewModel.cs
├── Views/                # Razor Views
│   ├── Home/
│   └── Secretary/
├── Migrations/           # EF Core Migrations
└── wwwroot/              # Static Assets
```

## 🗄️ Database Schema

### Core Entities
- **Doctors**: Clinic affiliation, personal details, medical specialty
- **DoctorSchedules**: Flexible weekly schedules with start/end times per day
- **Patients**: Personal information and contact details
- **Appointments**: Scheduled visits with status tracking and conflict prevention

### Key Relationships
- Clinic → Doctors (1:N)
- Doctor → DoctorSchedules (1:N)
- Doctor → Appointments (1:N)
- Patient → Appointments (1:N, optional)

## 🔧 Configuration

### Environment Variables
Create `appsettings.Development.json` for development-specific settings:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Database Seeding
The application includes automatic database seeding with sample data including:
- 5 sample doctors across different specialties
- Pre-configured weekly schedules
- Sample patients and appointments

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Authors

- **Aya Ahmed Karama** - *Initial work* - [GitHub](https://github.com/AyaKarama7)

## 🙏 Acknowledgments

- ASP.NET Core documentation
- Entity Framework Core community
- Bootstrap framework contributors
- Medical clinic management best practices

---

**Note**: This application is designed for educational and demonstration purposes. For production use, additional security measures, authentication, and comprehensive testing should be implemented.
