using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        public Guid SessionID { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Note> Notes { get; set; }

        public Category() { }

        public Category(string name)
        {
            CategoryID++;
            Name = name;
        }
    }
}
