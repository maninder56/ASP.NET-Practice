using DatabaseContext; 

namespace SchoolAPI.Services; 

public interface IDepartmentDatabaseService
{
    // Read Opeartions 
    public List<Department> GetAllDepartments(bool courses);
    public Department? GetDepartmentById(int id);
    public Department? GetDepartmentByName(string name);

    // Create Operattions 
    public Department? CreateDepartment(Department department);

    // Update Operations 
    public Department? UpdateDepartmentByID(int id, Department department);

    // Delete Operations 
    public bool DeleteDepartmentByID(int id);
}
