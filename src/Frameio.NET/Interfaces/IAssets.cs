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
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PagedResult<Asset>> GetChildren(string assetId, int pageSize = 10, int page = 1);

        /// <summary>
        /// Creates a new Asset for the given parentId
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="assetRequest"></param>
        /// <returns></returns>
        Task<Asset> CreateAsset(string parentId, CreateAssetRequest assetRequest);

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bytes"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Task<string> UploadAsset(string url, byte[] bytes, string contentType);
        string UploadAsset(Asset asset, string fileName);
    }
}
