﻿using System.Threading.Tasks;
using Frameio.NET.Models;

namespace Frameio.NET.Interfaces
{
    public interface IProjects
    {
        /// <summary>
        /// Returns a project for the given projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<Project> GetProject(string projectId);

        /// <summary>
        /// Returns a paged list of projects for the given teamId
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedResult<Project>> GetProjects(string teamId, int page, int pageSize = 10);
    }
}
