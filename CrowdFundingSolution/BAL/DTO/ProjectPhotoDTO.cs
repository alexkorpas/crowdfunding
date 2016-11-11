using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BAL.DTO
{
    public class ProjectPhotoDTO
    {
        public int Photo_Id { get; set; }
        public int Project_Id { get; set; }
        public byte[] Photo { get; set; }
    }
}
