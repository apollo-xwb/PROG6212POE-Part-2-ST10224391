namespace CMCS.Models
{
    public class Approval
    {
        public int ApprovalID { get; set; }
        public int ClaimID { get; set; }
        public string ApproverName { get; set; }
        public string ApprovalStatus { get; set; }
        public Claim Claim { get; set; }
    }
}
