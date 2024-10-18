using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using e1.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace e1.Models
{
    [Table("students")]
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 6)]
        public string Fullname { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required, EmailAddress, StringLength(100, MinimumLength = 10)]
        public string Email { get; set; }

        [Required, StringLength(15, MinimumLength = 10)]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Level { get; set; }
        public string Class { get; set; }
        public string CreatedDate { get; set; } = DateTime.Now.ToString();
    }


public static class StudentEndpoints
{
	public static void MapStudentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Student").WithTags(nameof(Student));

        group.MapGet("/", async (StudentDbContext db) =>
        {
            return await db.Students.ToListAsync();
        })
        .WithName("GetAllStudents")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Student>, NotFound>> (int id, StudentDbContext db) =>
        {
            return await db.Students.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Student model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetStudentById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Student student, StudentDbContext db) =>
        {
            var affected = await db.Students
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, student.Id)
                  .SetProperty(m => m.Fullname, student.Fullname)
                  .SetProperty(m => m.Birthday, student.Birthday)
                  .SetProperty(m => m.Gender, student.Gender)
                  .SetProperty(m => m.Email, student.Email)
                  .SetProperty(m => m.Phone, student.Phone)
                  .SetProperty(m => m.Address, student.Address)
                  .SetProperty(m => m.Level, student.Level)
                  .SetProperty(m => m.Class, student.Class)
                  .SetProperty(m => m.CreatedDate, student.CreatedDate)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateStudent")
        .WithOpenApi();

        group.MapPost("/", async (Student student, StudentDbContext db) =>
        {
            db.Students.Add(student);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Student/{student.Id}",student);
        })
        .WithName("CreateStudent")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, StudentDbContext db) =>
        {
            var affected = await db.Students
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteStudent")
        .WithOpenApi();
    }
}}
