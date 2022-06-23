using DevChatAPI2.Models;

namespace DevChatAPI2.Responses
{
    public class RoomResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MessageResponse>? Messages { get; set; }
    }
}
