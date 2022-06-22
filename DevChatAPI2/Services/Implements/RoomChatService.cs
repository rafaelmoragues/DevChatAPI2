using DevChatAPI2.Models;
using DevChatAPI2.Services.Interfaces;
using DevChatAPI2.UOfWork;

namespace DevChatAPI2.Services.Implements
{
    public class RoomChatService : IRoomChatService
    {
        private readonly IUnitOfWork _uow;
        public RoomChatService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public List<RoomChat> GetGroupChats()
        {
            List<RoomChat> groupChats = new List<RoomChat>();
            groupChats = _uow.RoomChatRepository.find(x => x.CategoryId == 1).ToList();
            return groupChats;
        }
        //hacer un get que reciba el userid del emisor y del receptor y busque la sala
        //privada. que el resultado llame a getprivatechat y le pase el id del chat
        //
        public RoomChat GetRoomChat(string user1Id, string user2Id)
        {
            //hacer que el find tenga el include de los roomchats, en el userroomrepo
            RoomChat roomChat = new RoomChat();
            List<UserRoom> list1 = _uow.UserRoomRepository.GetRoomsFull(user1Id).ToList();
            List<UserRoom> list2 = _uow.UserRoomRepository.GetRoomsFull(user2Id).ToList();
            //List<UserRoom> aux = (from u in list1
            //                      where u.RoomChats.CategoryId == 2
            //                      select u).ToList();
            //list1 = aux;
            //aux = (from u in list2
            //       where u.RoomChats.CategoryId == 2
            //       select u).ToList();
            //list2 = aux;
            //UserRoom userRoom = (from i in list1
            //            where list2.Any(u => u.RoomChats.Id == i.RoomChats.Id)
            //            select i).FirstOrDefault();

            //Las 3 busquedas anteriores las simplifico en una sola
            UserRoom userRoom = (from x in list1
                   where list2.Any(y => y.RoomChats.Id == x.RoomChats.Id && y.RoomChats.CategoryId == 2 && x.RoomChats.CategoryId == 2)
                   select x).FirstOrDefault();
            
            roomChat = GetPrivateChat(userRoom.RoomChatId);
            return roomChat;
        }
        public RoomChat GetPrivateChat(int id)
        {
            var a = _uow.RoomChatRepository.GetRoomFull(id);
            return a;
        }
        //agregar userroom
        //public void AddUserRoom()
        public void AddUserRoom(string userId, int roomId)
        {
            UserRoom u = new UserRoom();
            u.RoomChatId = roomId;
            u.UserId = userId;
            _uow.UserRoomRepository.Insert(u);
            _uow.Save();
        }
        public int AddRoomChat(RoomChat room)
        {
            _uow.RoomChatRepository.Insert(room);            
            _uow.Save();
            var id = _uow.RoomChatRepository.GetAll().Last();
            return id.Id;
        }
    }
}
