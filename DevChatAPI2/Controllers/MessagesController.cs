using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevChatAPI2.Data;
using DevChatAPI2.Models;
using DevChatAPI2.UOfWork;
using DevChatAPI2.Request;
using AutoMapper;

namespace DevChatAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MessagesController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Messages
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        //{
        //  if (_uow.MessageRepository == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Messages.ToListAsync();
        //}

        // GET: api/Messages/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Message>> GetMessage(int id)
        //{
        //  if (_context.Messages == null)
        //  {
        //      return NotFound();
        //  }
        //    var message = await _context.Messages.FindAsync(id);

        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    return message;
        //}

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            _uow.MessageRepository.Update(message);            
            _uow.Save();            

            return NoContent();
        }

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Message> PostMessage(MessageRequest message)
        {
          if (_uow.MessageRepository == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Messages'  is null.");
          }
            Message mes = _mapper.Map<Message>(message);
            mes.MessageTypeId = 1;
            _uow.MessageRepository.Insert(mes);
            _uow.Save();

            //return CreatedAtAction("GetMessage", new { id = message.Id }, message);
            return Ok();
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            if (_uow.MessageRepository == null)
            {
                return NotFound();
            }
            var message = _uow.MessageRepository.GetById(id);
            if (message == null)
            {
                return NotFound();
            }

            _uow.MessageRepository.Delete(id);
            _uow.Save();

            return NoContent();
        }

    }
}
