using System.ComponentModel.DataAnnotations;

namespace DevChatAPI2.Models
{
    public class CategoryChat
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
