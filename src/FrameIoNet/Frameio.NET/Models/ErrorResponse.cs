namespace Frameio.NET.Models {
    public class ErrorResponse
    {
        public int Code { get; set; }

        public Error[] Errors { get; set; }

        public string Message { get; set; }

    }
}