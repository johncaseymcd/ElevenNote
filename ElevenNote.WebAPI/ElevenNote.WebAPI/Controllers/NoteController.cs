﻿using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    [Authorize]
    public class NoteController : ApiController
    {
        [HttpPost]
        private NoteService CreateNoteService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var noteService = new NoteService(userID);
            return noteService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            NoteService noteService = CreateNoteService();
            var notes = noteService.GetNotes();
            return Ok(notes);
        }

        [HttpGet]
        public IHttpActionResult GetByNoteID([FromUri] int id)
        {
            NoteService noteService = CreateNoteService();
            var note = noteService.GetNoteByID(id);
            return Ok(note);
        }

        [HttpGet]
        public IHttpActionResult GetByCategoryID([FromUri] int catID)
        {
            NoteService noteService = CreateNoteService();
            var notes = noteService.GetNotesByCategory(catID);
            return Ok(notes);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] NoteCreate note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateNoteService();

            if (!service.CreateNote(note))
                return InternalServerError();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] NoteEdit note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateNoteService();

            if (!service.UpdateNote(note))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var service = CreateNoteService();

            if (!service.DeleteNote(id))
                return InternalServerError();

            return Ok();
        }

    }
}
