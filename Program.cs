using Microsoft.EntityFrameworkCore;
using FluentValidation;


using OrderAPI.Commands;
using OrderAPI.Queries;
using OrderAPI.Handlers;
using OrderAPI.Validators;
using OrderAPI.Dtos; 
//using YourProjectNamespace.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext> (opt => opt.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));
builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, OrderDto>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDto>, GetOrderByIdQueryHandler>();
builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();  
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
*/

app.MapPost("/api/orders", async (ICommandHandler<CreateOrderCommand, OrderDto> commandHandler, CreateOrderCommand command)=> 
{
   // var createdOrder = await CreateOrderCommandHandler.Handle(context, command);
   
   try
   {
        var createdOrder = await commandHandler.HandleAsync(command);
        if (createdOrder == null)
        {
            return Results.BadRequest("Failed to create order. Please check the input data and try again.");
        }
        return Results.Created($"/api/orders/{createdOrder.OrderID}", createdOrder);
    }
    catch (ValidationException ex)
    {
        var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
        return Results.BadRequest(new { Errors = errors });
    }
    catch (Exception ex)
    {
        // Log the exception (you can use a logging framework here)
        Console.WriteLine($"An error occurred: {ex.Message}");
        return Results.Problem("An unexpected error occurred. Please try again later.", statusCode: 500);
    }
    /*var createdOrder = await commandHandler.HandleAsync(command);
    if (createdOrder == null)
    {
        return Results.BadRequest("Failed to create order. Please check the input data and try again.");
    }
    return Results.Created($"/api/orders/{createdOrder.OrderID}", createdOrder);
    */

});

//app.MapGet("/api/orders/{id}", async (AppDbContext context, int id) =>
//app.MapGet("/api/orders/{id}", async (AppDbContext context, int id) =>
app.MapGet("/api/orders/{id}", async (IQueryHandler<GetOrderByIdQuery, OrderDto> queryHandler, int id) =>
{
    //var order = await context.Orders.FirstOrDefaultAsync(o=> o.ID == id);
    //var order = await GetOrderByIdQueryHandler.Handle(new GetOrderByIdQuery(id), context);
    var order = await queryHandler.HandleAsync(new GetOrderByIdQuery(id));
    if (order == null)
    {
        return Results.NotFound();

    }
    return Results.Ok(order); 
});



app.Run();

