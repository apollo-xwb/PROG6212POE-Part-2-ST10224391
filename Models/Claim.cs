using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace CMCS.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public int LecturerID { get; set; }
        public decimal Amount { get; set; }
        public DateTime ClaimDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected
        public string Description { get; set; }
        public Lecturer Lecturer { get; set; }
        public ICollection<SupportingDocument> SupportingDocuments { get; set; }
        public ICollection<Approval> Approvals { get; set; }

        [NotMapped]
        public IFormFile Document { get; set; }

        // New stuff
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
