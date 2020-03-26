using System.Threading.Tasks;
using Frameio.NET.Models;

namespace Frameio.NET.Interfaces
{
    public interface IAssets
    {
        /// <summary>
        /// Returns a paged list of child Assets for the given assetId
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedResult<Asset>> GetChildren(string assetId, int page, int pageSize = 10);

        /// <summary>
        /// Creates a new Asset for the given parentId
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="assetRequest"></param>
        /// <returns></returns>
        Task<Asset> CreateAsset(string parentId, CreateAssetRequest assetRequest);

        /// <summary>
        /// Upload file an asset using the specified file.
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string UploadAsset(Asset asset, string fileName);
    }
}
