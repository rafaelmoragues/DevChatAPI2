using System.ComponentModel.DataAnnotations;

namespace DevChatAPI2.Models
{
    public class MessageType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
