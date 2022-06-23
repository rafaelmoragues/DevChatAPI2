using DevChatAPI2.Models;
using DevChatAPI2.Responses;

namespace DevChatAPI2.Services.Interfaces
{
    public interface IRoomChatService
    {
        int AddRoomChat(RoomChat room);
        List<RoomResponse> GetGroupChatsList();
        RoomResponse GetRoomChat(string user1Id, string user2Id);
        void AddUserRoom(string userId, int roomId);
        List<RoomResponse> GetPrivChatList(string id);
    }
}