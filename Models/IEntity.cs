using System.ComponentModel.DataAnnotations;

namespace MVC.Models{
     public class IEntity
    {  
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;
    }
}