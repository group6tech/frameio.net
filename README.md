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

Returns a User

### Teams
List all the Teams for the given AccountId

`var teams = await frameioclient.Teams.GetTeams(AccountId);`

Parameters
- string AccountId - the Account Id for the teams

Optional Parameters

- int pageSize - the number of items per page to return

- int page - the page number to return

returns a PagedResult of Teams

### Projects
Get all the projects for the given Team Id

`var projects = await frameioclient.Projects.GetProjects(teamId);`

Parameters
- string TeamId - the Team Id for the Projects

Optional Parameters

- int pageSize - the number of items per page to return

- int page - the page number to return

returns a PagedResult of Projects


### Project Assets
List all the assets for a Project

`var assets = await frameioclient.Assets.GetChildren(rootAssetId);`

Parameters
- string RootAssetId - the root Asset Id for the Project

Optional Parameters

- int pageSize - the number of items per page to return

- int page - the page number to return

returns a PagedResult of Assets

### Creating an Asset

`var result = await frameioclient.Assets.CreateAsset("parentId",
    new CreateAssetRequest
    {
        Description = "Gravatar",
        FileSize = 2048,
        MimeType = "image/png",
        Name = "gravatar.png",
        Type = FileType.File
    });`

Parameters
- string ParentId - This can be the Id of an existing Asset or the Root Asset Id for a Project.
- CreateAssetRequest - the request model containing the details of the Asset

Returns an Asset

### Uploading an Asset
Upload an asset

Assets are uploaded in byte chunks. Once you create an Asset you will be provide with the a list of urls used to upload your media.

`await frameioclient.Assets.UploadAsset(url, buffer, "image/png");`

Parameters
- string url - the upload url provided by the CreateAsset result.
- byte[] - a byte array containing a chunk of your media to be uploaded
- contentType - the content type of the file, this used as part of the http request headers

## Getting help

If you've instead found a bug in the library or would like new features added, go ahead and open issues or pull requests against this repo!

