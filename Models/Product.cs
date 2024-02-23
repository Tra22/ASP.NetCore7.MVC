using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class Product : IEntity
{
    [Required]
    public string? Name { get; set; }
    public bool IsDeleted {get;set;}
}
