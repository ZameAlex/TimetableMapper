using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableMapper.RozkladModels
{
    public class Subject
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}";
        }

    }
}
