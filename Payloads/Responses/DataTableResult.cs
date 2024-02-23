namespace MVC.Payloads.Responses{
    public class DataTableResult<T>
    {
        public int draw { get; set; }
        public long recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T>? data { get; set; }
        public string? error { get; set; }
    }
}