﻿using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userID;

        public NoteService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateNote(NoteCreate model)
        {
            var entity = new Note()
            {
                OwnerID = _userID,
                Title = model.Title,
                Content = model.Content,
                CategoryID = model.CategoryID,
                CreatedUTC = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Notes
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                                new NoteListItem
                                {
                                    NoteID = e.NoteID,
                                    Title = e.Title,
                                    CategoryID = e.CategoryID,
                                    CreatedUTC = e.CreatedUTC
                                }
                        );

                return query.ToArray();
            }
        }

        public NoteDetail GetNoteByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteID == id && e.OwnerID == _userID);
                return
                    new NoteDetail
                    {
                        NoteID = entity.NoteID,
                        Title = entity.Title,
                        Content = entity.Content,
                        CategoryID = entity.CategoryID,
                        CreatedUTC = entity.CreatedUTC,
                        ModifiedUTC = entity.ModifiedUTC
                    };
            }
        }

        public IEnumerable<NoteListItem> GetNotesByCategory(int categoryID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Notes
                        .Where(e => e.OwnerID == _userID && e.CategoryID == categoryID)
                        .Select(
                            e =>
                               new NoteListItem
                                {
                                    NoteID = e.NoteID,
                                    Title = e.Title,
                                    CategoryID = e.CategoryID,
                                    CreatedUTC = e.CreatedUTC
                                }
                        );

                return query.ToArray();
            }
        }

        public bool UpdateNote(NoteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteID == model.NoteID && e.OwnerID == _userID);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.CategoryID = model.CategoryID;
                entity.ModifiedUTC = model.ModifiedUTC;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int noteID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteID == noteID && e.OwnerID == _userID);

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
