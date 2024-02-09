namespace Exam;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public CardDetails CardDetails { get; set; }
    public string PinCode { get; set; }
    public List<Transaction> TransactionHistory { get; set; }
}