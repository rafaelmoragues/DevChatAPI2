using AutoMapper;
using DevChatAPI2.Models;
using DevChatAPI2.Responses;
using DevChatAPI2.Services.Interfaces;
using DevChatAPI2.UOfWork;

namespace DevChatAPI2.Services.Implements
{
    public class RoomChatService : IRoomChatService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public RoomChatService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public List<RoomResponse> GetGroupChatsList()
        {
            List<RoomChat> groupChats = new List<RoomChat>();
            groupChats = _uow.RoomChatRepository.find(x => x.CategoryId == 1).ToList();
            List<RoomResponse> resGroup = MapearResponse(groupChats);
            return resGroup;
        }
        public List<RoomResponse> GetPrivChatList(string id)
        {
            List<RoomResponse> res = new List<RoomResponse>();
            List<RoomChat> rchat = new List<RoomChat>();
            RoomChat rChatAux = new RoomChat();
            List<UserRoom> userRooms = _uow.UserRoomRepository.GetRoomsFull(id).ToList();
            foreach (UserRoom userRoom in userRooms)
            {
                rChatAux = userRoom.RoomChats;
                rChatAux.Name = userRoom.Name;
                rchat.Add(rChatAux);
            }
            res= _mapper.Map<List<RoomResponse>>(rchat);
            return res;
        }
        public RoomResponse GetPrivChatMsg(string user1Id, string user1Name, string user2Name,string user2Id)
        {
            RoomChat roomChat = new RoomChat();
            List<UserRoom> list1 = _uow.UserRoomRepository.GetRoomsFull(user1Id).ToList();
            List<UserRoom> list2 = _uow.UserRoomRepository.GetRoomsFull(user2Id).ToList();

            UserRoom userRoom = (from x in list1
                                 where list2.Any(y => y.RoomChats.Id == x.RoomChats.Id && y.RoomChats.CategoryId == 2 && x.RoomChats.CategoryId == 2)
                                 select x).FirstOrDefault();

            //si no existe una sala privada entre los dos users creo una
            if (userRoom == null)
            {
                roomChat.Name = "priv";
                roomChat.CategoryId = 2;
                int aux = AddRoomChat(roomChat);
                AddUserRoom(user1Id, aux,user2Name);
                AddUserRoom(user2Id, aux, user1Name);
                userRoom = new UserRoom();
                userRoom.RoomChatId = aux;
            }

            roomChat = _uow.RoomChatRepository.GetRoomFull(userRoom.RoomChatId);
            RoomResponse roomResponse = _mapper.Map<RoomResponse>(roomChat);
            return roomResponse;
        }
        public RoomResponse GetGroupChatMsg(int id)
        {
            RoomChat aux = _uow.RoomChatRepository.GetRoomFull(id);
            return _mapper.Map<RoomResponse>(aux);
        }
        public void AddUserRoom(string userId, int roomId, string name)
        {
            UserRoom u = new UserRoom();
            u.RoomChatId = roomId;
            u.UserId = userId;
            u.Name = name;
            _uow.UserRoomRepository.Insert(u);
            _uow.Save();
        }
        public int AddRoomChat(RoomChat room)
        {
            room.CategoryId = 2;
            _uow.RoomChatRepository.Insert(room);            
            _uow.Save();
            var id = _uow.RoomChatRepository.GetAll().Last();
            return id.Id;
        }
        private List<RoomResponse> MapearResponse(List<RoomChat> li)
        {
            List<RoomResponse> roomResponses = new List<RoomResponse>();
            List<MessageResponse> mlist = new List<MessageResponse>();
            RoomResponse aux;
            MessageResponse mAux;
            foreach (RoomChat roomChat in li)
            {
                if (roomChat.Messages != null)
                {
                    foreach (var m in roomChat.Messages)
                    {
                        mAux = _mapper.Map<MessageResponse>(m);
                        mlist.Add(mAux);
                    }
                }
                aux = _mapper.Map<RoomResponse>(roomChat);
                aux.Messages = mlist;
                roomResponses.Add(aux);
            }
            return roomResponses;
        }
    }
}
