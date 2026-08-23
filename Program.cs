using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext> (opt => opt.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
*/

app.MapPost("/api/orders", async (AppDbContext context, Order order)=> 
{
    await context.Orders.AddAsync(order);
    await context.SaveChangesAsync();
    //return Results.Created ($"/api/orders{order.ID}", order);
    return Results.Created($"/api/orders/{order.ID}", order);
});

//app.MapGet("/api/orders/{id}", async (AppDbContext context, int id) =>
//app.MapGet("/api/orders/{id}", async (AppDbContext context, int id) =>
app.MapGet("/api/orders/{id}", async (AppDbContext context, int id) =>
{
    var order = await context.Orders.FirstOrDefaultAsync(o=> o.ID == id);
    if (order == null)
    {
        return Results.NotFound();

    }
    return Results.Ok(order); 
});



app.Run();

