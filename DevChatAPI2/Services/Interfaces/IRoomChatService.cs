using DevChatAPI2.Models;
using DevChatAPI2.Responses;

namespace DevChatAPI2.Services.Interfaces
{
    public interface IRoomChatService
    {
        int AddRoomChat(RoomChat room);
        void AddUserRoom(string userId, int roomId, string name);
        List<RoomResponse> GetGroupChatsList();
        List<RoomResponse> GetPrivChatList(string id);
        //RoomResponse GetPrivChatMsg(string user1Id, string user2Id);
        RoomResponse GetGroupChatMsg(int id);
        
        
    }
}