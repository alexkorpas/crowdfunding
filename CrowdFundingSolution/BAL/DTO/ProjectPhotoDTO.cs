using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BAL
{
    public class ProjectPhotoDTO
    {
        public int? Id { get; set; }
        public int? ProjectFK { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoString { get; set; }
        public int? Pointer { get; set; }
    }
}
