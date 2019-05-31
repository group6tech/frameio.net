namespace Frameio.NET.Interfaces
{
    interface IFrameIoClient
    {
        IAssets Assets { get; }

        IProjects Projects { get; }

        ITeams Teams { get; }

        IUsers Users { get; }
    }

}
