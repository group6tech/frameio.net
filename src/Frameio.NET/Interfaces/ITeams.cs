using System.Threading.Tasks;
using Frameio.NET.Models;

namespace Frameio.NET.Interfaces
{
    public interface ITeams
    {
        /// <summary>
        /// Returns a list list of Teams for the current user
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PagedResult<Team>> GetTeams(int page, int pageSize = 10);

        /// <summary>
        /// Returns a list list of Teams for the given accountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PagedResult<Team>> GetTeams(string accountId, int page, int pageSize = 10);
    }
}
