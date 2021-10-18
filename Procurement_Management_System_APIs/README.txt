DB First Command
Scaffold-DbContext "server=localhost;uid=root;pwd=root;database=procurement_managment_system" Pomelo.EntityFrameworkCore.MySql -OutputDir Models

Update Context
Scaffold-DbContext "server=localhost;uid=root;pwd=root;database=procurement_managment_system" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -context ProcurementManagmentContext -Project ProcurementManagmentSystemAPIs -force