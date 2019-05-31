using System.Threading.Tasks;
using Frameio.NET.Models;

namespace Frameio.NET.Interfaces {
    public interface IProjects {

        /// <summary>
        /// Returns a paged list of projects for the given teamId
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PagedResult<Project>> GetProjects(string teamId, int pageSize = 10, int page = 1);
    }
}
