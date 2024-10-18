using System.Collections.Generic;

namespace CMCS.Models
{
    public class Lecturer
    {
        public int LecturerID { get; set; }
        public string Name { get; set; }
        public ICollection<Claim> Claims { get; set; } //not being used
    }
}
