using Microsoft.EntityFrameworkCore;
using OrderAPI.Dtos;
using OrderAPI.Queries;

public class GetOrderByIdQueryHandler: IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly AppDbContext _context;

    public GetOrderByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDto?> HandleAsync(GetOrderByIdQuery query)
    {
        //throw new NotImplementedException();

        var order = await _context.Orders.FirstOrDefaultAsync(o => o.ID == query.OrderID);
        if (order == null)
        {
            return null;
        }
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


  /*public static async Task<OrderDto?> Handle(AppDbContext context, GetOrderByIdQuery query)
    {
        return await context.Orders.FirstOrDefaultAsync(o => o.OrderID == query.OrderID);
    }
    */