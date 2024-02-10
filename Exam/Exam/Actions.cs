public class Actions
{
    public Dictionary<int, Func<object>> KeyValuePairs { get; set; }
    
    public Actions()
    {
        KeyValuePairs = new Dictionary<int, Func<object>>
        {
            { 1, () => ChoosenAction() },
            { 2, () => ChoosenAction() },
            { 3, () => ChoosenAction() }
        };
    }
    
    public object ChoosenAction()
    {
        // Define your method implementation here
        return null; // Replace null with actual implementation
    }
}