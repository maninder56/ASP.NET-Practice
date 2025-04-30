
using DatabaseContext;
using SchoolAPI.Services;

using static SchoolAPI.Data.SchoolRecords.StudentGradeRecords;

namespace SchoolAPI.EndpointFilters; 

public class StudentGradeValidationFilters
{
    // For updating and deleting validation 
    public class StudentGradeExistsValidationFilter : IEndpointFilter
    {
        private IStudentGradeDatabaseService dbService; 

        public StudentGradeExistsValidationFilter(IStudentGradeDatabaseService dbService)
        {
            this.dbService = dbService;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            int id = context.GetArgument<int>(0);

            if (!dbService.StudentGradeExists(id))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "EnrollmentID", new string[] { $"Student Grade with id {id} does not exists" } }
                });
            }

            return await next(context);
        }
    }

    // For when Creating 
    public class StudentGradeModelValidaitonFilter : IEndpointFilter
    {
        private IStudentGradeDatabaseService dbService;

        public StudentGradeModelValidaitonFilter(IStudentGradeDatabaseService dbService)
        {
            this.dbService = dbService;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            StudentGradeRecord studentGrade = context.GetArgument<StudentGradeRecord>(0);

            if (!dbService.CourseExists(studentGrade.CourseId))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "CourseID", new string[] { $"Can not assign student grade if course does not exists" } }
                });
            }

            if (!dbService.StudentExists(studentGrade.StudentId))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "StudentID", new string[] { $"Can not assign student grade if Student does not exists" } }
                });
            }

            return await next(context);
        }
    }

}
