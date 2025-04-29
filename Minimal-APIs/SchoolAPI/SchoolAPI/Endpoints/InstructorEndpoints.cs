using DatabaseContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.EndpointFilters;
using SchoolAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Endpoints; 

public static class InstructorEndpoints
{
    private record InstructorRecord
    {
        public int InstructorId { get; init; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; init; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; init; } = null!; 

        public DateTime? HireDate { get; set; }

        public InstructorRecord() { }

        public InstructorRecord(Person person)
        {
            InstructorId = person.PersonId;
            FirstName = person.FirstName;
            LastName = person.LastName;
            HireDate = person.HireDate;
        }
    }

    public static WebApplication MapInstructorEndpoints(this WebApplication app)
    {
        RouteGroupBuilder endpoint = app.MapGroup("/instructor");

        RouteGroupBuilder endpointWithValidation = endpoint.MapGroup("/")
            .AddEndpointFilter<IdValidationFilter>()
            .AddEndpointFilter<InstructorExistsValidationFilter>();

        // GET Endpoints
        endpoint.MapGet("/", GetAllInstructors); 
        endpointWithValidation.MapGet("/{id:int}", GetInstructorByID)
            .WithName("GetInstructorByID");

        // POST Endpoints 
        endpoint.MapPost("/", CreateInstructor)
            .WithParameterValidation();

        // PUT Endpoints 
        endpointWithValidation.MapPut("/{id:int}", UpdateInstructorByID)
            .WithParameterValidation();

        // DELETE Endpoints 
        endpointWithValidation.MapDelete("/{id:int}", DeleteInstructorByID); 

        return app; 
    }

    // Handlers

    // GET Handlers
    private static Results<Ok<List<InstructorRecord>>, ProblemHttpResult> GetAllInstructors(
        [FromServices] IInstructorDatabaseService dbService)
    {
        List<InstructorRecord> instructorRecords = dbService.GetAllInstructors()
            .Select(p => new InstructorRecord(p))
            .ToList();

        return TypedResults.Ok(instructorRecords);
    }

    private static Results<Ok<InstructorRecord>, ProblemHttpResult> GetInstructorByID(
        int id, [FromServices] IInstructorDatabaseService dbService)
    {
        Person? instructor = dbService.GetInstructorByID(id);

        if (instructor == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to retrive Instructor details"); 
        }

        InstructorRecord record = new InstructorRecord(instructor);

        return TypedResults.Ok(record); 
    }


    // POST Handlers
    private static Results<Created<InstructorRecord>, ProblemHttpResult> CreateInstructor(
        InstructorRecord instructor, [FromServices] IInstructorDatabaseService dbService,
        [FromServices] LinkGenerator linkGenerator)
    {
        Person? created = dbService.CreateInstructor(new Person()
        {
            FirstName = instructor.FirstName,
            LastName = instructor.LastName,
        });

        if (created == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to created Instructor");
        }

        string? link = linkGenerator.GetPathByName("GetInstructorByID", new { id = created.PersonId }); 

        return TypedResults.Created(link, new InstructorRecord(created));
    }


    // PUT Handlers
    private static Results<NoContent, ProblemHttpResult> UpdateInstructorByID(
        int id, InstructorRecord instructor, [FromServices] IInstructorDatabaseService dbService)
    {
        Person personDetails = dbService.GetInstructorByID(id)!;

        personDetails.FirstName = instructor.FirstName;
        personDetails.LastName = instructor.LastName;

        if (instructor.HireDate != null)
        {
            personDetails.HireDate = instructor.HireDate;
        }

        bool updated = dbService.UpdateInstructorByID(id, personDetails); 

        if (!updated)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to update Instructor Details");
        }

        return TypedResults.NoContent();
    }


    // DELETE Handlers
    private static Results<NoContent, ProblemHttpResult, ValidationProblem> DeleteInstructorByID(
        int id, [FromServices] IInstructorDatabaseService dbService)
    {
        bool officeAssigned = dbService.OfficeAssignmentExists(id);

        if (officeAssigned)
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                { "OfficeAssinment", new string[] { $"Instructor with ID {id} can not be deleted because it is being referenced by Office Assignment" } }
            });
        }


        bool deleted = dbService.DeleteInstructorByID(id);

        if (!deleted)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to delete Instructor");
        }

        return TypedResults.NoContent();
    }
}
