
namespace MVC.Payloads.Requests{
    public class ColumnParameter
    {
        public string? data { get; set; }
        public string? name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }

        public SearchParameter? search { get; set; } 
    }
}