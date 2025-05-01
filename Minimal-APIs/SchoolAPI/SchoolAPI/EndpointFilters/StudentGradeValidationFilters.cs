
using DatabaseContext;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SchoolAPI.Services;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using static SchoolAPI.Data.SchoolRecords.StudentGradeRecords;

namespace SchoolAPI.EndpointFilters; 

public static class StudentGradeValidationFilters
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

    // For when Creating and updating
    public class StudentGradeModelValidaitonFilter : IEndpointFilter
    {
        private IStudentGradeDatabaseService dbService;

        public StudentGradeModelValidaitonFilter(IStudentGradeDatabaseService dbService)
        {
            this.dbService = dbService;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            object methodArgument = context.Arguments.FirstOrDefault(a => a is StudentGradeRecord)
                ?? throw new ArgumentException("StudentGradeRecord Type not provided");

            StudentGradeRecord studentGrade = (StudentGradeRecord)methodArgument; 

            decimal? grade = studentGrade.Grade;

            if (grade != null && !isGradeCorrect((decimal)grade))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "Grade", new string[] { $"Invalid value" } }
                });
            }

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

        private bool isGradeCorrect(decimal grade)
        {
            string gradeString = grade.ToString();
            bool containsDecimal = gradeString.Contains('.');

            if (containsDecimal)
            {
                if (gradeString.Length > 4)
                {
                    return false;
                }

                string afterDecimalNumbers = gradeString.Split('.')[1];

                if (afterDecimalNumbers.Length > 2 || afterDecimalNumbers.Length < 2)
                {
                    return false;
                }
            }

            if (!containsDecimal && gradeString.Length > 1)
            {
                return false;
            }

            return true;
        }
    }

}
