using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class TestModel
{
    public int Id {get;set;}
    [Required]
    public string? Name { get; set; }
}
