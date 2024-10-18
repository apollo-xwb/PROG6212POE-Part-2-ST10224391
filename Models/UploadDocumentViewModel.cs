using Microsoft.AspNetCore.Http; // For IFormFile

namespace CMCS.Models
{
    public class UploadDocumentViewModel
    {
        public string LecturerId { get; set; }
        public IFormFile Document { get; set; }
    }
}
