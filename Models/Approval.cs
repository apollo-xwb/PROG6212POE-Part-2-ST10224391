namespace CMCS.Models
{
    public class Approval
    {
        public int ApprovalID { get; set; }
        public int ClaimID { get; set; }
        public string ApproverName { get; set; }
        public string ApprovalStatus { get; set; } // Approved, Rejected
        public string Comments { get; set; } // Optional comments from the approver of the application
        public Claim Claim { get; set; }
    }
}
