﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Note
    {
        [Key]
        public Int32 NoteID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public Category Category { get; set; }

        [Required]
        public DateTimeOffset CreatedUTC { get; set; }
        
        public DateTimeOffset ModifiedUTC { get; set; }
    }
}
