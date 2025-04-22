using DatabaseContext;
using Microsoft.EntityFrameworkCore;

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

    public List<Department> GetAllDepartments(bool getCourses)
    {
        List<Department>? departments = null; 

        if (getCourses)
        {
            departments = database.Departments?.AsNoTracking()
                .Select(d => new Department()
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name,
                    Budget = d.Budget,
                    StartDate = d.StartDate,
                    Administrator = d.Administrator,
                    Courses = d.Courses
                        .Select(c => new Course()
                        {
                            CourseId = c.CourseId,
                            Title = c.Title,
                            Credits = c.Credits,
                            DepartmentId = c.DepartmentId
                        })
                    .ToList()
                })
                .ToList();
        }
        else
        {
            departments = database.Departments?
                .AsNoTracking()
                .ToList();
        }
        
        return departments ?? new List<Department>();
    }

    public Department? GetDepartmentById(int id)
    {
        Department? department = database.Departments?
            .AsNoTracking()
            .Where(d => d.DepartmentId == id)
            .FirstOrDefault();

        return department;
    }

    public Department? GetDepartmentByName(string name)
    {
        Department? department = database.Departments?
            .AsNoTracking()
            .Where(d => d.Name == name) 
            .Select(d => new Department()
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

    public Department? CreateDepartment(Department department)
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

    public Department? UpdateDepartmentByID(int id, Department department)
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
        Department? department = GetDepartmentById(id);

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
