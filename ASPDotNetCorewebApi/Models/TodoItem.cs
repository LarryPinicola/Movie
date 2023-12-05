namespace ASPDotNetCorewebApi.Models
{
    public class TodoItem
    {
        public long id { get; set; }

        public string? Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
