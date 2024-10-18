namespace CMCS.Models
{
    public class SupportingDocument
    {
        public int SupportingDocumentID { get; set; } // Primary key
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; } 
        public int ClaimID { get; set; } // Foreign key
        public Claim Claim { get; set; } // Navigation
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Content { get; set; }
    }
}
