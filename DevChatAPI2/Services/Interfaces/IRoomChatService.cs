using DevChatAPI2.Models;

namespace DevChatAPI2.Services.Interfaces
{
    public interface IRoomChatService
    {
        int AddRoomChat(RoomChat room);
        List<RoomChat> GetGroupChats();
        RoomChat GetPrivateChat(int id);
        RoomChat GetRoomChat(string user1Id, string user2Id);
        void AddUserRoom(string userId, int roomId);
    }
}