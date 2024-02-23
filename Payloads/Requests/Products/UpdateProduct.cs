using System.ComponentModel.DataAnnotations;

namespace MVC.Payloads.Requests.Products{
    public class UpdateProduct{
        [Required]
        public int Id {get;set;}
        [Required]
        public string? Name { get; set; }
        public bool IsDeleted {get;set;} = false;
    }
}