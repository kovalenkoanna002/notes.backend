using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Backend.Domain.Dtos;
using Notes.Backend.Domain.Interfaces.Services;
using Skreet2k.Common.Models;
using System.Security.Claims;

namespace Notes.Backend.Api.Controllers
{
    [Route("api/secure/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _service;

        public NotesController(INotesService notesService)
        {
            _service = notesService;
        }

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<ResultList<NoteDto>>> GetAllNotes(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _service.GetAllNotesAsync(userId, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Result<NoteDto>>> GetNoteById(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetNoteByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpPost()]
        [Authorize]
        public async Task<ActionResult<Result<NoteDto>>> CreateNote([FromBody] NoteUpsertDto noteDto, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _service.CreateNoteAsync(userId, noteDto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Result<NoteDto>>> UpdateNote(int id, [FromBody] NoteUpsertDto noteDto, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateNoteAsync(id, noteDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Result>> DeleteNote(int id, CancellationToken cancellationToken)
        {
            var result = await _service.RemoveNoteAsync(id, cancellationToken);
            return Ok(result);
        }
    }
}
