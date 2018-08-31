using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableMapper.RozkladModels.Enums;

namespace TimetableMapper.RozkladModels
{
    public class Lesson
    {
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public Day Day { get; set; }
        public LessonNumber LessonNumber { get; set; }
        public bool FirstWeek { get; set; }
    }
}
