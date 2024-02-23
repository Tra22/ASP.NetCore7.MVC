
namespace MVC.Payloads.Requests{
    public class DataTableParameters
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public SearchParameter search { get; set; }
   
        public OrderParameter[] order { get; set; }
     
        public ColumnParameter[] columns { get; set; } 
    }
}