using SchoolAPI.Modles.DepartmentModels;

namespace SchoolAPI.Services; 

public interface IDepartmentDatabaseService
{
    // Read Opeartions 
    public List<DepartmentModel> GetAllDepartments();
    public DepartmentModel? GetDepartmentById(int id);
    public DepartmentModel? GetDepartmentByName(string name);

    // Create Operattions 
    public DepartmentModel? CreateDepartment(DepartmentModel department);

    // Update Operations 
    public DepartmentModel? UpdateDepartmentByID(int id, DepartmentModel department);

    // Delete Operations 
    public bool DeleteDepartmentByID(int id);
}
