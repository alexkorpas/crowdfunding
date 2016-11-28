using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class ProjectUpdateDTO
    {

        public int? Id { get; set; }
        public int? ProjectFK { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

    }
}