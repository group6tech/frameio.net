using System.Threading.Tasks;
using Frameio.NET.Models;

namespace Frameio.NET.Interfaces
{
    public interface IUsers
    {
        /// <summary>
        /// Returns detail about the current user
        /// </summary>
        /// <returns></returns>
        Task<User> GetCurrentUser();
    }
}
