using System.Threading.Tasks;
using Frameio.NET.Models;

namespace Frameio.NET.Interfaces {
    public interface IUsers
    {
        Task<User> GetCurrentUser();
    }
}