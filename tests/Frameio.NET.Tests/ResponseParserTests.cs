using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Frameio.NET.Models;
using Frameio.NET.Parsers;
using Xunit;

namespace Frameio.NET.Tests
{
    public class ResponseParserTests
    {
        [Theory]
        [InlineData(HttpStatusCode.Forbidden, "Forbidden")]
        [InlineData(HttpStatusCode.InternalServerError, "InternalServerError")]
        [InlineData(HttpStatusCode.NotFound, "NotFound")]
        [InlineData(HttpStatusCode.TooManyRequests, "TooManyRequests")]
        [InlineData(HttpStatusCode.Unauthorized, "Unauthorized")]
        [InlineData(HttpStatusCode.UnprocessableEntity, "{\"code\":422,\"errors\":[{\"code\":\"invalid\",\"detail\":\"type is invalid\",\"field\":\"type\",\"title\":\"is invalid\"}],\"message\":\"There was a problem processing your request\"}")]
        public void ParseResponse_Should_ThrowException_WhenNotSuccessCode(HttpStatusCode httpStatusCode, string content)
        {
            Assert.Throws<FrameioException>(() => ResponseParser.ParseJsonResponse<object>(httpStatusCode, content));
        }

        [Fact]
        public void ParseResponse_Should_Return_Asset()
        {
            Asset asset = ResponseParser.ParseJsonResponse<Asset>(HttpStatusCode.OK, "{\"id\":\"e96afe69-077a-4cf3-b807-024ccb3e06e7\",\"parent_id\":\"e48430cd-7be7-416d-87d4-0290e10c72ba\",\"project_id\":\"93e5be61-ef4c-46b7-bd1d-b36ff1e666e8\",\"name\":\"60 Second Cut.mp4\",\"type\":\"file\",\"upload_urls\":null}");

            Assert.Equal("e96afe69-077a-4cf3-b807-024ccb3e06e7", asset.Id);
            Assert.Equal("60 Second Cut.mp4", asset.Name);
            Assert.Equal("e48430cd-7be7-416d-87d4-0290e10c72ba", asset.ParentId);
            Assert.Equal("93e5be61-ef4c-46b7-bd1d-b36ff1e666e8", asset.ProjectId);
            Assert.Equal("file", asset.Type);
        }

        [Fact]
        public void ParseResponse_Should_Return_Project()
        {
            Project project = ResponseParser.ParseJsonResponse<Project>(HttpStatusCode.OK, "{\"id\":\"93e5be61-ef4c-46b7-bd1d-b36ff1e666e8\",\"name\":\"Demo Project\",\"owner_id\":\"86139123-b5be-4a8d-8ff9-8e49e58ed1f4\",\"private\":false,\"project_preferences\":{\"notify_on_updated_label\":true,\"notify_on_new_mention\":true,\"notify_on_new_comment\":true,\"notify_on_new_collaborator\":true,\"notify_on_new_asset\":true,\"collaborator_can_share\":true,\"collaborator_can_invite\":true,\"collaborator_can_download\":true},\"root_asset_id\":\"e48430cd-7be7-416d-87d4-0290e10c72ba\",\"team_id\":\"7495aacc-1952-41a0-84a4-05f5777ec337\"}");

            Assert.Equal("93e5be61-ef4c-46b7-bd1d-b36ff1e666e8", project.Id);
            Assert.Equal("Demo Project", project.Name);
            Assert.Equal("e48430cd-7be7-416d-87d4-0290e10c72ba", project.RootAssetId);
            Assert.Equal("86139123-b5be-4a8d-8ff9-8e49e58ed1f4", project.OwnerId);
            Assert.Equal("7495aacc-1952-41a0-84a4-05f5777ec337", project.TeamId);
            Assert.False(project.Private);
        }

        [Fact]
        public void ParseResponse_Should_Return_Team()
        {
            Team team = ResponseParser.ParseJsonResponse<Team>(HttpStatusCode.OK, "{\"id\":\"7495aacc-1952-41a0-84a4-05f5777ec337\",\"access\":\"private\",\"file_count\":0,\"collaborator_count\":1,\"name\":\"Cleveland\",\"project_count\":1,\"storage\":0}");

            Assert.Equal("7495aacc-1952-41a0-84a4-05f5777ec337", team.Id);
            Assert.Equal("Cleveland", team.Name);
            Assert.Equal("private", team.Access);
            Assert.Equal(1, team.CollaboratorCount);
            Assert.Equal(0, team.FileCount);
            Assert.Equal(1, team.ProjectCount);
            Assert.Equal(0, team.Storage);
        }

        [Fact]
        public void ParseResponse_Should_Return_User()
        {
            User user = ResponseParser.ParseJsonResponse<User>(HttpStatusCode.OK, "{\"id\":\"86139123-b5be-4a8d-8ff9-8e49e58ed1f4\",\"account_id\":\"e46f295e-71be-4e8a-b9be-621168e03bea\",\"email\":\"name@domain.com\",\"name\":\"Saigo Akecheta\"}");

            Assert.Equal("86139123-b5be-4a8d-8ff9-8e49e58ed1f4", user.Id);
            Assert.Equal("e46f295e-71be-4e8a-b9be-621168e03bea", user.AccountId);
            Assert.Equal("name@domain.com", user.Email);
            Assert.Equal("Saigo Akecheta", user.Name);
        }

        [Theory]
        [InlineData("<https://domain.com/first>; rel=\"first\", <https://domain.com/last>; rel=\"last\"", 100, 10, "https://domain.com/first", "https://domain.com/last", null, null)]
        [InlineData("<https://domain.com/next>; rel=\"next\", <https://domain.com/previous>; rel=\"prev\"", 1000, 100, null, null, "https://domain.com/next", "https://domain.com/previous")]
        public void ParseHeader_Should_Return_Paging(string headerLinks, int total, int totalPages, string firstLink, string lastLink, string nextLink, string previousLink)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("link", new List<string> { headerLinks });
            response.Headers.Add("total", new List<string> { $"{total}" });
            response.Headers.Add("total-pages", new List<string> { $"{totalPages}" });

            Paging paging = ResponseParser.ParseHeader(response.Headers);

            Assert.Equal(total, paging.TotalRecords);
            Assert.Equal(totalPages, paging.TotalPages);

            Assert.Equal(firstLink, paging.FirstLink);
            Assert.Equal(lastLink, paging.LastLink);
            Assert.Equal(nextLink, paging.NextLink);
            Assert.Equal(previousLink, paging.PreviousLink);
        }
    }
}
