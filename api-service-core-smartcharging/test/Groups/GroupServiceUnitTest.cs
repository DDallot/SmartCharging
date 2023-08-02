using Api.Contracts.Core.SmartCharging.Groups.v1;
using Api.Services.Core.SmartCharging.Dal.Common;
using Api.Services.Core.SmartCharging.Dal.GroupDal;
using Api.Services.Core.SmartCharging.Services.Groups;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Api.Services.Core.SmartCharging.UnitTests.Groups
{
    public class GroupServiceUnitTest
    {
        private readonly IGroupRepository _groupDal;
        private readonly IDalSession _dalSession;
        private readonly ILogger<GroupService> _logger;
        private readonly IValidator<Group> _groupValidator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroup _groupService;
        public GroupServiceUnitTest()
        {
            _groupDal = A.Fake<IGroupRepository>();
            _dalSession = A.Fake<IDalSession>();
            _unitOfWork = A.Fake<IUnitOfWork>();
            _logger = A.Fake<ILogger<GroupService>>();
            _groupValidator = new GroupValidator();
            _groupService = new GroupService(_groupDal, _dalSession, _groupValidator, _logger);
        }

        [Fact]
        public async Task Insert_Group_Data()
        {
            var value = new CreateGroupRequest { Name = "test name", Capacity = 200};
            var data = 123;

            A.CallTo(() => _groupDal.InsertAsync(A<Group>._)).Returns(data);
            var result = await _groupService.CreateAsync(value);

            result.Item.Should().Be(data);
        }

        [Fact]
        public async Task Insert_Group_Missing_Fields_Data()
        {
            var value = new CreateGroupRequest();
            var data = 123;

            A.CallTo(() => _groupDal.InsertAsync(A<Group>._)).Returns(data);
            var result = await _groupService.CreateAsync(value);

            result.HasError.Should().Be(true);
            result.Errors.Count.Should().Be(2);
        }

        [Fact]
        public async Task Update_Group_Data()
        {
            var identifier = 11;
            var value = new UpdateGroupRequest { Name = "test name", Capacity = 200 };
            var data = true;

            A.CallTo(() => _dalSession.UnitOfWork).Returns(_unitOfWork);

            A.CallTo(() => _groupDal.UpdateAsync(A<Group>._)).Returns(data);
            var result = await _groupService.UpdateAsync(identifier, value);


            A.CallTo(() => _unitOfWork.Begin()).MustHaveHappened();
            A.CallTo(() => _unitOfWork.Commit()).MustHaveHappened();
            result.Item.Should().Be(data);
        }

        [Fact]
        public async Task Update_Group_Over_Capacity_Data()
        {
            var identifier = 11;
            var value = new UpdateGroupRequest { Name = "test name", Capacity = 200 };
            var data = true;

            A.CallTo(() => _dalSession.UnitOfWork).Returns(_unitOfWork);

            A.CallTo(() => _groupDal.GetMaxCurrentSumAsync(A<int>.That.IsEqualTo(identifier))).Returns(Task.FromResult(300));
            A.CallTo(() => _groupDal.UpdateAsync(A<Group>._)).Returns(data);
            var result = await _groupService.UpdateAsync(identifier, value);


            A.CallTo(() => _unitOfWork.Begin()).MustHaveHappened();
            A.CallTo(() => _unitOfWork.Commit()).MustHaveHappened();
            result.HasError.Should().Be(true);
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Delete_Group_Data()
        {
            var identifier = 11;
            var value = new UpdateGroupRequest { Name = "test name", Capacity = 200 };
            var data = true;

            A.CallTo(() => _groupDal.DeleteAsync(A<Group>._)).Returns(data);
            var result = await _groupService.DeleteAsync(identifier);

            result.Item.Should().Be(data);
        }

        [Fact]
        public async Task Delete_Group_Throw_Exception_Data()
        {
            var identifier = 11;
            var value = new UpdateGroupRequest { Name = "test name", Capacity = 200 };
            var data = true;

            A.CallTo(() => _groupDal.DeleteAsync(A<Group>._)).Throws(new Exception());
            var result = await _groupService.DeleteAsync(identifier);

            result.HasError.Should().Be(true);
            result.Errors.Count.Should().Be(1);
        }
    }
}