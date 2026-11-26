using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTestingNote.Data;
using ProjectTestingNote.DTO;
using ProjectTestingNote.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectTestingNote.Controllers
{
    public class NoteController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IMapper _maper;


        public NoteController(AppDbContext db, IMapper maper)
        {
            _db = db;
            _maper = maper;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Get

        [HttpGet("{Guid.Id}")]
        public async Task<IActionResult> GetById(Guid Id , CancellationToken ct)
        {
            var item = await _db.Notes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Id, ct);

            if (item == null) { return NotFound(); }

            return Ok(_maper.Map<NoteReadDTO>(item));
        }

        //Create Note
        [HttpPost("create-Note")]
        public async Task<ActionResult<NoteReadDTO>> NoteCreate([FromBody] NoteCreateDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var entity = _maper.Map<Note>(dto);

            entity.Id = Guid.NewGuid();
            _db.Notes.Add(entity);
            await _db.SaveChangesAsync(ct);

            var result = _maper.Map<NoteReadDTO>(entity);
            return CreatedAtAction(nameof(GetById), new { Id = entity.Id }, result);

        }

        //Create Notes in List
        [HttpPost("create-List-Note")]
        public async Task<ActionResult<List<NoteReadDTO>>> NoteCreateList([FromBody] List<NoteCreateDto> dtos, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var entities = _maper.Map<List<Note>>(dtos);

            foreach (var entity in entities)
            {
                if (entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();
            }

            await _db.Notes.AddRangeAsync(entities, ct);
            await _db.SaveChangesAsync(ct);

            var result = _maper.Map<List<NoteReadDTO>>(entities);

            return Ok(result);

        }
    }
}
