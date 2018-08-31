using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableMapper.RozkladModels.Enums
{
    public enum LessonType
    {
        [Description("Лаб")]
        Lab,
        [Description("Лек.")]
        Lection,
        [Description("Прак.")]
        Practice
    } 
}
