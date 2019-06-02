namespace Frameio.NET.Models
{
    public class Paging
    {
        public string FirstLink { get; set; }

        public string LastLink { get; set; }

        public string NextLink { get; set; }

        public string PreviousLink { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }
    }
}
