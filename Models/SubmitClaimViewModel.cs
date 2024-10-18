public class SubmitClaimViewModel
{
    public string LecturerId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public IFormFile Document { get; set; }
}
