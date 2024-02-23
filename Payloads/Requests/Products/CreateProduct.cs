using System.ComponentModel.DataAnnotations;

namespace MVC.Payloads.Requests.Products{
    public class CreateProduct{
        [Required]
        public string? Name { get; set; }
        public bool IsDeleted {get;set;} = false;
    }
}