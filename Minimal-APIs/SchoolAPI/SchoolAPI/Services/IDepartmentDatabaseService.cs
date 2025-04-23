using DatabaseContext; 

namespace SchoolAPI.Services; 

public interface IDepartmentDatabaseService
{
    // Read Opeartions 
    public List<Department> GetAllDepartments(bool getCourses);
    public Department? GetDepartmentById(int id, bool getCourses);
    public Department? GetDepartmentByName(string name, bool getCourses);

    // Create Operattions 
    public Department? CreateDepartment(Department department);

    // Update Operations 
    public Department? UpdateDepartmentByID(int id, Department department);

    // Delete Operations 
    public bool DeleteDepartmentByID(int id);
}
