using Api.Contracts.Core.SmartCharging.Groups.v1;
using Api.Services.Core.SmartCharging.Dal.GroupDal;
using Api.Services.Core.SmartCharging.Services.Groups;
using FluentAssertions;
using Xunit;

namespace Api.Services.Core.SmartCharging.UnitTests.Groups
{
    public class GroupConverterTest
    {

        [Fact]
        public void Convert_CreateGroupRequest_To_Group()
        {
            var value = new CreateGroupRequest { Name = "test name", Capacity = 200 };
            var data = new Group { Name = "test name", Capacity = 200 };

            var result = value.Convert();

            result.Should().BeEquivalentTo(data);
        }

        [Fact]
        public void Convert_UpdateGroupRequest_To_Group()
        {
            int identifier = 123;
            var value = new UpdateGroupRequest { Name = "test name", Capacity = 200 };
            var data = new Group { Identifier = identifier, Name = "test name", Capacity = 200 };

            var result = value.Convert(identifier);

            result.Should().BeEquivalentTo(data);
        }
    }
}
