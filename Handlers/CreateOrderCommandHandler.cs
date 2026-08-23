using System;
using System.Threading.Tasks;
using OrderAPI.Dtos;      // Explicitly tells the compiler where OrderDto is 
using OrderAPI.Commands;

namespace OrderAPI.Handlers;
public class createOrderCommandHandler: ICommandHandler<CreateOrderCommand, OrderDto>
{ 
    private readonly AppDbContext _context;
    public createOrderCommandHandler(AppDbContext context)
    {
        _context = context;
    }
    public async Task<OrderDto>HandleAsync(CreateOrderCommand command)
    {
          var order = new Order
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Status = command.Status,
            CreatedAt = DateTime.Now,
            TotalCost = command.TotalAmount
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        
        return new OrderDto
        (
            order.ID,
            order.FirstName,
            order.LastName,
            order.Status,
            order.CreatedAt,
            order.TotalCost
        );
    }

}

 /*
    public static async Task<Order> Handle(AppDbContext context, CreateOrderCommand command)
    {
        var order = new Order
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Status = command.Status,
            TotalAmount = command.TotalAmount
        };

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return order;
    }
    */