using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Modles.DepartmentModels;

namespace SchoolAPI.Services.Implementations;

public class DepartmentDatabaseService : IDepartmentDatabaseService
{
    private SchoolForApiContext database; 

    public DepartmentDatabaseService(SchoolForApiContext database)
    {
        this.database = database;
    }

    // Helper Methods

    private int MaximumDepartmentID()
    {
        int? id = database.Departments?
            .AsNoTracking()
            .Max(d => d.DepartmentId);

        return id ?? 0; 
    }



    // CRUD Operations on Department Table

    // Read Operations 

    public List<DepartmentModel> GetAllDepartments()
    {
        List<DepartmentModel>? departments = database.Departments?
            .AsNoTracking()
            .Select(d => new DepartmentModel()
            {
                DepartmentId = d.DepartmentId,
                Name = d.Name,
                Budget = d.Budget, 
                StartDate = d.StartDate,
                Administrator = d.Administrator,
            }).ToList();

        return departments ?? new List<DepartmentModel>();
    }

    public DepartmentModel? GetDepartmentById(int id)
    {
        DepartmentModel? department = database.Departments?
            .AsNoTracking()
            .Where(d => d.DepartmentId == id)
            .Select(d => new DepartmentModel()
            {
                DepartmentId = d.DepartmentId,
                Name = d.Name,
                Budget = d.Budget,
                StartDate = d.StartDate,
                Administrator = d.Administrator,
            })
            .FirstOrDefault();

        return department;
    }

    public DepartmentModel? GetDepartmentByName(string name)
    {
        DepartmentModel? department = database.Departments?
            .AsNoTracking()
            .Where(d => d.Name == name) 
            .Select(d => new DepartmentModel()
            {
                DepartmentId = d.DepartmentId,
                Name = d.Name,
                Budget = d.Budget,
                StartDate = d.StartDate,
                Administrator = d.Administrator,
            })
            .FirstOrDefault();

        return department;
    }


    // Create  Operations 

    public DepartmentModel? CreateDepartment(DepartmentModel department)
    {
        DatabaseContext.Department entityModel = new DatabaseContext.Department()
        {
            DepartmentId = MaximumDepartmentID() + 1,
            Name = department.Name,
            Budget = department.Budget,
            StartDate = department.StartDate,
            Administrator = department.Administrator,
        };

        database.Departments?.Add(entityModel); 

        int entriesWritten = database.SaveChanges();

        if (entriesWritten > 0)
        {
            department.DepartmentId = entityModel.DepartmentId;
            return department; 
        }
        else
        {
            return null; 
        }
    }


    // Update Operations 

    public DepartmentModel? UpdateDepartmentByID(int id, DepartmentModel department)
    {
        bool entityExists = GetDepartmentById(id) != null;

        if (!entityExists)
        {
            return null; 
        }

        DatabaseContext.Department entityModel = new DatabaseContext.Department()
        {
            DepartmentId = id,
            Name = department.Name,
            Budget = department.Budget,
            StartDate = department.StartDate,
            Administrator = department.Administrator,
        };

        database.Departments?.Update(entityModel);

        int entriesWritten = database.SaveChanges();

        if (entriesWritten > 0)
        {
            return department;
        }
        else
        {
            return null;
        }
    }


    // Delete Operations 

    public bool DeleteDepartmentByID(int id)
    {
        DepartmentModel? department = GetDepartmentById(id);

        if (department == null)
        {
            return false;
        }

        DatabaseContext.Department entityModel = new DatabaseContext.Department()
        {
            DepartmentId = department.DepartmentId,
            Name = department.Name,
            Budget = department.Budget,
            StartDate = department.StartDate,
            Administrator = department.Administrator,
        };

        database.Departments?.Remove(entityModel);

        return database.SaveChanges() > 0; 
    }

    
}
