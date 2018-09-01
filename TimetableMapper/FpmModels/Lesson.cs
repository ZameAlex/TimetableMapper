using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableMapper.FpmModels
{
    public class Lesson
    {
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public string Room { get; set; }
        public bool Flasher { get; set; }
    }
}
