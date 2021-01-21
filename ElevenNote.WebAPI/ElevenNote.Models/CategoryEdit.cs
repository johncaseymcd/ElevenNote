using ElevenNote.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryEdit
    {
        public Int16 CategoryID { get; set; }
        public string Name { get; set; }
        public DateTimeOffset ModifiedUTC { get; set; }
    }
}
