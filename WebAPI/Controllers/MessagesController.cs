using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public MessagesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser(
            [FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUserName();

            var messages = await _unitOfWork.MessageRepository.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(
                messages.CurrentPages, messages.PageSize, messages.TotalCount,  messages.TotalPages);

            return messages;
        }

        // DELETE: api/messages/:id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = User.GetUserName();

            var message = await _unitOfWork.MessageRepository.GetMessage(id);

            if (message.Sender.UserName != username && message.Recipient.UserName != username)
                return Unauthorized();
            
            if (message.Sender.UserName == username) message.SenderDeleted = true;

            if (message.Recipient.UserName == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted) 
                _unitOfWork.MessageRepository.DeleteMessage(message);
            
            if (await _unitOfWork.Complete()) return Ok();
            return BadRequest("Problem deleting the message");
        }
    }
}