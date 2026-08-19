using System.Globalization;

public class Order
{
    public int ID {get; set;}

    public required string FirstName {get; set;}
    public required string  LastName {get; set;}

    public required string  Status {get; set;}
    public DateTime  CreatedAt {get; set;}

    public Decimal TotalCost {get; set;}
    


}