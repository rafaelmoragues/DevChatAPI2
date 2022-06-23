namespace DevChatAPI2.Request
{
    public class MessageRequest
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int RoomChatId { get; set; }
        public string MessageBody { get; set; }
        public DateTime Date { get; set; }
    }
}
