using Api.Contracts.Core.SmartCharging.Common;
using Api.Contracts.Core.SmartCharging.Groups.v1;
using Api.Services.Core.SmartCharging.Controllers.SmartChargings.v1;
using FakeItEasy;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Api.Services.Core.SmartCharging.UnitTests.Groups
{
    public  class GroupsControllerUnitTest
    {
        private readonly IGroup _groupService;
        private readonly GroupsController _groupController;
        public GroupsControllerUnitTest()
        {
            _groupService = A.Fake<IGroup>();
            _groupController = new GroupsController(_groupService);
        }

        [Fact]
        public async Task Create_Group_Data()
        {
            var value = new CreateGroupRequest { Name = "test name", Capacity = 200 };
            var data = new ItemResult<int> { Item = 123};

            A.CallTo(() => _groupService.CreateAsync(A<CreateGroupRequest>._)).Returns(Task.FromResult(data));
            var result = await _groupController.CreateAsync(value);

            result.Should().Be(data);
        }

        [Fact]
        public async Task Update_Group_Data()
        {
            var identifier = 11;
            var value = new UpdateGroupRequest { Name = "test name", Capacity = 200 };
            var data = new ItemResult<bool> { Item = true };

            A.CallTo(() => _groupService.UpdateAsync(A<int>.That.IsEqualTo(identifier), A<UpdateGroupRequest>._)).Returns(Task.FromResult(data));
            var result = await _groupController.UpdateAsync(identifier, value);

            result.Should().Be(data);
        }

        [Fact]
        public async Task Delete_Group_Data()
        {
            var identifier = 11;
            var data = new ItemResult<bool> { Item = true };

            A.CallTo(() => _groupService.DeleteAsync(A<int>.That.IsEqualTo(identifier))).Returns(Task.FromResult(data));
            var result = await _groupController.DeleteAsync(identifier);

            result.Should().Be(data);
        }
    }
}
