# frameio.net
.NET library for the Frame.io API

[![Build Status](https://dev.azure.com/group6tech/frameio.net/_apis/build/status/group6tech.frameio.net.build?branchName=master)](https://dev.azure.com/group6tech/frameio.net/_build/latest?definitionId=14&branchName=master)



## Sample Usage

The examples below show how to use the FrameioClient.

`var frameioclient = new Frameio.NET.FrameioClient(myHttpClient);`

Intialzie with your authentication token. This token will be used with all calls requiring an authentication token.

`frameioclient.Initialize("your token");`



### Users

To get details about the current user

`var user = await frameioclient.Users.GetCurrentUser();`

Returns `Task<User>`.



### Teams

List all the Teams for the given AccountId

`var teams = await frameioclient.Teams.GetTeams(AccountId, 1);`

Parameters
- `string AccountId` - the Account Id for the teams
- `int page` - the page number to return

Optional Parameters
- `int pageSize` - the number of items per page to return

Returns `Task<PagedResult<Team>>`.



### Projects

Get all the projects for the given Team Id

`var projects = await frameioclient.Projects.GetProjects(teamId, 1);`

Parameters
- `string TeamId` - the Team Id for the Projects
- `int page` - the page number to return

Optional Parameters
- `int pageSize` - the number of items per page to return

Returns `Task<PagedResult<Project>>`.



### Project Assets

List all the assets for a Project

`var assets = await frameioclient.Assets.GetChildren(rootAssetId);`

Parameters
- `string RootAssetId` - the root Asset Id for the Project
- `int page` - the page number to return

Optional Parameters
- `int pageSize` - the number of items per page to return

Returns `Task<PagedResult<Asset>>`.



### Creating an Asset

```
var result = await frameioclient.Assets.CreateAsset("parentId",
    new CreateAssetRequest
    {
        Description = "My Video",
        FileSize = 2048,
        MimeType = "video/mp4",
        Name = "quicktime/mp4",
        Type = FileType.File
    });
```

Parameters
- `string ParentId` - This can be the Id of an existing Asset or the Root Asset Id for a Project.
- `CreateAssetRequest` - the request model containing the details of the Asset

Returns `Task<Asset>`.



### Uploading an Asset

Upload an asset

Assets are uploaded in byte chunks. Once you create an Asset you will be provide with the a list of urls used to upload your media.

`await frameioclient.Assets.UploadAsset(asset, @"C:\my\video.mp4");`

Parameters
- `Asset asset` - the asset created with `CreateAsset()`
- `string fileName` - the full path to the file to upload



## Getting help

If you've instead found a bug in the library or would like new features added, go ahead and open issues or pull requests against this repo!

