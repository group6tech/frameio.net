namespace Frameio.NET.Interfaces
{
    interface IFrameIoClient
    {
        /// <summary>
        /// Assets <see cref="IAssets"/>
        /// </summary>
        IAssets Assets { get; }

        /// <summary>
        /// Projects <see cref="IProjects"/>
        /// </summary>
        IProjects Projects { get; }

        /// <summary>
        /// Teams <see cref="ITeams"/>
        /// </summary>
        ITeams Teams { get; }

        /// <summary>
        /// Users <see cref="IUsers"/>
        /// </summary>
        IUsers Users { get; }

    }

}
