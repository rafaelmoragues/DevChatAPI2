using System.ComponentModel.DataAnnotations;

namespace DevChatAPI2.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int RoomChatId { get; set; }
        public string MessageBody { get; set; }
        public DateTime Date { get; set; }
        public int MessageTypeId { get; set; }
        public MessageType? MessageType { get; set; }
        public RoomChat? RoomChat { get; set; }


    }
}
