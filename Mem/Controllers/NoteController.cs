﻿using Mem.Services;
using Mem.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;
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
            var notes = noteService
                .GetNotes(search)
                .Select(x => new
                {
                    Customer = HttpUtility.HtmlEncode(x.Customer),
                    Classification = Convert.ToInt16(x.Classification),
                    x.DateCreated,
                    x.DateUpdated,
                    x.ID,
                    Text = HttpUtility.HtmlEncode(x.Text.Ellipsis(50))
                });
            
            return new JsonResult(notes);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult GetNote(int id)
        {
            var x = noteService.GetNote(id);
            return new JsonResult(new
            {
                Customer = HttpUtility.HtmlEncode(x.Customer),
                Classification = Convert.ToInt16(x.Classification),
                x.DateCreated,
                x.DateUpdated,
                x.ID,
                Text = HttpUtility.HtmlEncode(x.Text)
            });
        }

        [HttpPut]
        [Route("")]
        public ActionResult UpdateNote(Models.NoteModel note)
        {
            return new JsonResult(noteService.SetNote(note));
        }

        [HttpGet]
        [Route("suggestions")]
        public ActionResult GetSuggestions(String search)
        {
            var sugs = noteService
                .GetSuggestions(search)
                .Select(x => new { tag = HttpUtility.HtmlEncode(x.Item1), tagCleaned = x.Item2 });
            return new JsonResult(sugs);
        }


        [HttpDelete]
        [Route("")]
        public ActionResult DeleteNote([FromBody]int id)
        {
            return new JsonResult(noteService.DeleteNote(id));
        }
    }
}
