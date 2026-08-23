namespace OrderAPI.Commands;
public record  CreateOrderCommand(
    string FirstName,
    string LastName,
    string Status,
    decimal TotalAmount
    );