namespace OrderAPI.Dtos; 
public record OrderDto
(
    int OrderID, 
    string FirstName,
    string LastName,
    string Status,
    DateTime CreatedAt,
    decimal TotalAmount
);
