using Mem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mem.Controllers
{
    [ApiController]
    [Route("api/v1/notes")]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly NoteService noteService;

        public NoteController(NoteService noteService)
        {
            this.noteService = noteService;
        }
        
        [HttpGet]
        [Route("")]
        public ActionResult Notes(String search)
        {
            var notes = noteService.GetNotes(search);
            foreach (var item in notes)
            {
                item.Text = item.Text.Length > 50 ? item.Text.Substring(0, 47) + "..." : item.Text;
            } 
            return new JsonResult(notes);
        }

    }
}
