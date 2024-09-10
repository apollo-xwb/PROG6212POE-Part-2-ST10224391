using System;
using System.Collections.Generic;

namespace CMCS.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public int LecturerID { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal ClaimAmount { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public Lecturer Lecturer { get; set; }
        public ICollection<SupportingDocument> SupportingDocuments { get; set; }
        public ICollection<Approval> Approvals { get; set; }
    }
}
