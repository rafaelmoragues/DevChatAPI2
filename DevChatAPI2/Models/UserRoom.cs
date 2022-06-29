using System.ComponentModel.DataAnnotations;


namespace DevChatAPI2.Models
{
    public class UserRoom
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int RoomChatId { get; set; }
        public string UserId { get; set; }
        public RoomChat RoomChats { get; set; }

    }
}
