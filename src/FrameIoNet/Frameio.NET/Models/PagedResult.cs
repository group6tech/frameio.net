using System.Collections.Generic;

namespace Frameio.NET.Models
{
    public class PagedResult<T>
    {
        public Paging Paging { get; set; }

        public IEnumerable<T> Results { get; set; }

    }
}
