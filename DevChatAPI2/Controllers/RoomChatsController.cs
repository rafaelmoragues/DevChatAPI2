using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevChatAPI2.Data;
using DevChatAPI2.Models;
using DevChatAPI2.Services.Interfaces;
using DevChatAPI2.UOfWork;
using DevChatAPI2.Responses;

namespace DevChatAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomChatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRoomChatService _roomChatService;
        private readonly IUnitOfWork _uow;

        public RoomChatsController(ApplicationDbContext context, IRoomChatService roomS, IUnitOfWork uow)
        {
            _context = context;
            _roomChatService = roomS;
            _uow = uow;
        }

        // GET: api/RoomChats
        //[HttpGet]
        //public ActionResult<IEnumerable<RoomChat>> GetRoomChats()
        //{
        //  if (_uow.RoomChatRepository == null)
        //  {
        //      return NotFound();
        //  }
        //    List<RoomChat> groupList = _roomChatService.GetGroupChatsList();
        //    return Ok(groupList);
        //}

        // GET: api/RoomChats/5
        [HttpGet("{id}")]
        public ActionResult<List<RoomResponse>> GetRoomChatsList(string id)
        {
          if (_uow.RoomChatRepository == null)
          {
              return NotFound();
          }
            List<RoomResponse> groupList = _roomChatService.GetGroupChatsList();
            var roomChat = _roomChatService.GetPrivChatList(id);
            groupList.AddRange(roomChat);

            if (roomChat == null)
            {
                return NotFound();
            }

            return groupList;
        }
        [HttpGet("/api/[controller]/priv")]
        public IActionResult GetPrivateChat([FromQuery] string idSender, [FromQuery] string idReceiver)
        {
            var room = _roomChatService.GetPrivChatMsg(idSender, idReceiver);
            return Ok(room);
        }

        [HttpGet("/api/[controller]/group/{id}")]
        public IActionResult GetGroupChat(int id)
        {
            var room = _roomChatService.GetGroupChatMsg(id);
            return Ok(room);
        }

        // PUT: api/RoomChats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomChat(int id, RoomChat roomChat)
        {
            if (id != roomChat.Id)
            {
                return BadRequest();
            }

            _context.Entry(roomChat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomChatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RoomChats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomChat>> PostPrivRoomChat([FromBody]RoomChat roomChat, [FromQuery] string idSender, [FromQuery] string idReceiver)
        {
          if (_context.RoomChats == null)
          {
              return Problem("Entity set 'ApplicationDbContext.RoomChats'  is null.");
          }
          int auxId =_roomChatService.AddRoomChat(roomChat);
            _roomChatService.AddUserRoom(idSender, auxId);
            _roomChatService.AddUserRoom(idReceiver, auxId);
            //_context.RoomChats.Add(roomChat);
            //await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/RoomChats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomChat(int id)
        {
            if (_context.RoomChats == null)
            {
                return NotFound();
            }
            var roomChat = await _context.RoomChats.FindAsync(id);
            if (roomChat == null)
            {
                return NotFound();
            }

            _context.RoomChats.Remove(roomChat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomChatExists(int id)
        {
            return (_context.RoomChats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
