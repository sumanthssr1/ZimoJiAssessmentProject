using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using ZimojiAssessmentProject.DataAccessLayer;

namespace ZimojiAssessmentProject.Models
{
    public class UserTasks
    {
        [Key]
        public int UserTask_Id{get; set;}
        [Required]
       
        public int User_ID { get; set;}
        [Required]
        
        public int Task_Id {  get; set;}
        [ForeignKey("User_ID")]
        public virtual Users User { get; set; }

        [ForeignKey("Task_Id")]
        public virtual AvailableTasks Task { get; set; }

    }


public static class UserTasksEndpoints
{
	public static void MapUserTasksEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/UserTasks").WithTags(nameof(UserTasks));

        group.MapGet("/", async (UserTaskContext db) =>
        {
            return await db.UserTasks.ToListAsync();
        })
        .WithName("GetAllUserTasks")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<UserTasks>, NotFound>> (int usertask_id, UserTaskContext db) =>
        {
            return await db.UserTasks.AsNoTracking()
                .FirstOrDefaultAsync(model => model.UserTask_Id == usertask_id)
                is UserTasks model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUserTasksById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int usertask_id, UserTasks userTasks, UserTaskContext db) =>
        {
            var affected = await db.UserTasks
                .Where(model => model.UserTask_Id == usertask_id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.UserTask_Id, userTasks.UserTask_Id)
                  .SetProperty(m => m.User_ID, userTasks.User_ID)
                  .SetProperty(m => m.Task_Id, userTasks.Task_Id)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUserTasks")
        .WithOpenApi();

        group.MapPost("/", async (UserTasks userTasks, UserTaskContext db) =>
        {
            db.UserTasks.Add(userTasks);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/UserTasks/{userTasks.UserTask_Id}",userTasks);
        })
        .WithName("CreateUserTasks")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int usertask_id, UserTaskContext db) =>
        {
            var affected = await db.UserTasks
                .Where(model => model.UserTask_Id == usertask_id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUserTasks")
        .WithOpenApi();
    }
}}
