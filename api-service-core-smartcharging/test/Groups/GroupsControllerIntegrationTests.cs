using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Contracts.Core.SmartCharging.Common;
using Api.Contracts.Core.SmartCharging.Groups.v1;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Api.Services.Core.SmartCharging.UnitTests.Groups
{
    public class GroupsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Startup> _factory;

        public GroupsControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task Create_Group()
        {
            int insertedIdentifier = 0;
            try
            {
                // Arrange
                var newGroup = new { Name = "Group X", Capacity = 360 };
                var updateGroup = new { Name = "Group X", Capacity = 380 };

                // Insert Group
                var jsonInsert = JsonConvert.SerializeObject(newGroup);
                var contentInsert = new StringContent(jsonInsert, Encoding.UTF8, "application/json");
                var responseInsert = await _httpClient.PostAsync("/api/v1/groups", contentInsert);

                // Assert
                responseInsert.StatusCode.Should().Be(HttpStatusCode.OK);
                
                var responseContent = await responseInsert.Content.ReadAsStringAsync();
                var groupInsert = JsonConvert.DeserializeObject<ItemResult<int>>(responseContent);

                groupInsert.Item.Should().BeGreaterThan(0);
                insertedIdentifier = groupInsert.Item;

                // Update Group
                var jsonUpdate = JsonConvert.SerializeObject(updateGroup);
                var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");
                await _httpClient.PutAsync($"/api/v1/groups/{groupInsert.Item}", contentUpdate);

                // Get Group
                var responseGet = await _httpClient.GetAsync($"/api/v1/groups?identifier={groupInsert.Item}");
                var responseContentGet = await responseGet.Content.ReadAsStringAsync();
                var groupGet = JsonConvert.DeserializeObject<ItemResult<GroupResponse>>(responseContentGet);

                groupGet.Item.Name.Should().Be(updateGroup.Name);
                groupGet.Item.Capacity.Should().Be(updateGroup.Capacity);
            }
            finally
            {
                if(insertedIdentifier != 0)
                {
                    await _httpClient.DeleteAsync($"/api/v1/groups?identifier={insertedIdentifier}");
                }
            }            
        }
    }
}