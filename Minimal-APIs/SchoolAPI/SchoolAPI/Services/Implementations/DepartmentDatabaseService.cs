using DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace SchoolAPI.Services.Implementations;

public class DepartmentDatabaseService : BaseDatabaseService, IDepartmentDatabaseService
{
    public DepartmentDatabaseService(SchoolForApiContext database)
        : base(database) { } 
  
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
            departments = database.Departments?.AsNoTracking()
                .ToList();
        }
        
        return departments ?? new List<Department>();
    }

    public Department? GetDepartmentById(int id, bool getCourses)
    {
        Department? department = null;  

        if (getCourses)
        {
            department = database.Departments?.AsNoTracking()
            .Where(d => d.DepartmentId == id)
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
            .FirstOrDefault();

            return department;
        }

        department = database.Departments?.AsNoTracking()
            .Where(d => d.DepartmentId == id)
            .FirstOrDefault();

        return department; 
    }

    public Department? GetDepartmentByName(string name, bool getCourses)
    {
        Department? department = null;

        if (getCourses)
        {
            department = database.Departments?.AsNoTracking()
            .Where(d => d.Name == name)
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
            .FirstOrDefault();

            return department;
        }

        department = database.Departments?.AsNoTracking()
            .Where(d => d.Name == name)
            .FirstOrDefault();

        return department;
    }


    // Create  Operations 

    public Department? CreateDepartment(Department department)
    {
        department.DepartmentId = MaximumDepartmentID() + 1; 

        database.Departments?.Add(department); 

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


    // Update Operations 

    public Department? UpdateDepartmentByID(int id, Department department)
    {
        department.DepartmentId = id; 

        database.Departments?.Update(department);

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
        Department? department = GetDepartmentById(id, getCourses: false);

        if (department == null)
        {
            return false;
        }

        database.Departments?.Remove(department);

        return database.SaveChanges() > 0; 
    }

    
}
