using System.Threading.Tasks;
using Frameio.NET.Models;

namespace Frameio.NET.Interfaces {
    public interface IProjects {
        Task<PagedResult<Project>> GetProjects(string teamId, int pageSize = 10, int page = 1);
    }
}