using AutoFixture;
using AutoMapper;
using GrpcDemo.DomainService.Core.Interfaces.Repositories;
using NUnit.Framework;
using NSubstitute;
using GrpcDemo.DomainService.Core.Services;
using GrpcDemo.DomainService.Core.Entities;
using GrpcDemo.DomainService.Core.DTOs;
using System.Threading.Tasks;
using FluentAssertions;
using System;
using NSubstitute.Core;
using System.Collections;
using System.Collections.Generic;
using GrpcDemo.DomainService.Core.Misc;

namespace GrpcDemo.Test
{
    [TestFixture()]
    public class CompanyServiceTests
    {
        private IMapper _mapper;
        private ICompanyRepository _fakeRepository;
        private Fixture _fixture;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mapper = TestHook.MapperConfigurationProvider.CreateMapper();
            _fixture = new Fixture();
        }

        [SetUp]
        public void Setup()
        {
           _fakeRepository = Substitute.For<ICompanyRepository>();
        }

        private CompanyService GetSystemUnderTest()
        {
            return new CompanyService(_mapper, _fakeRepository);
        }


        [Test]
        [TestCase(520)]
        public async Task GetById_當輸入Id有資料_應回傳包含該Id的物件(int input)
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var entity = _fixture.Build<CompanyEntity>().With(x => x.Id, input).Create();
            _fakeRepository.GetById(Arg.Any<QueryCompanyEntity>())
                .Returns(entity);

            // Actual
            var actual = await sut.GetById(new QueryCompanyDTO { Id = input });

            // Assert
            actual.Should().NotBeNull();
            actual.Id.Should().Be(input);
        }

        [Test]
        [TestCase(520)]
        public void GetById_當出現例外_應拋出Exception(int input)
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var entity = _fixture.Build<CompanyEntity>().With(x => x.Id, input).Create();
            _fakeRepository.GetById(Arg.Any<QueryCompanyEntity>())
                .Returns((Func<CallInfo, CompanyEntity>)(x => throw new Exception()));

            // Actual
            Func<Task> actual = async () => await sut.GetById(new QueryCompanyDTO { Id = input } );

            // Assert
            actual.Should().ThrowAsync<Exception>();
        }

        [Test]
        [TestCase(20)]
        public async Task GetAll_當呼叫函式_應回傳所有物件(int count)
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var entity = _fixture.Build<CompanyEntity>().CreateMany(count);
            _fakeRepository.GetAll().Returns(entity);

            // Actual
            var actual = await sut.GetAll();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().HaveCount(count);
        }

        [Test]
        [TestCase(20)]
        public void GetAll_當出現例外_應拋出Exception(int count)
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var entity = _fixture.Build<CompanyEntity>().CreateMany(count);
            _fakeRepository.GetAll()
                .Returns((Func<CallInfo, IEnumerable<CompanyEntity>>)(x => throw new Exception()));

            // Actual
            Func<Task> actual = async () => await sut.GetAll();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().ThrowAsync<Exception>();
        }

        [Test]
        public async Task Create_當輸入物件已存在_應回傳重複訊息()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(true);

            // Actual
            var actual = await sut.Create(_fixture.Build<CompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Success.Should().BeFalse();
            actual.Message.Should().Be("Duplicate company");
        }

        [Test]
        public void Create_當物件沒有重複但出現例外_應拋出Exception()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(false);
            _fakeRepository.Create(Arg.Any<CompanyEntity>())
                .Returns((Func<CallInfo, bool>)(x => throw new Exception()));

            // Actual
            Func<Task> actual = async () => await sut.Create(_fixture.Build<CompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Should().ThrowAsync<Exception>();
        }

        [Test]
        public async Task Create_當物件沒有重複且新增失敗_應回傳false和失敗訊息()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(false);
            _fakeRepository.Create(Arg.Any<CompanyEntity>())
                .Returns(false);

            // Actual
            var actual = await sut.Create(_fixture.Build<CompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Success.Should().BeFalse();
            actual.Message.Should().Be("Create failed");
        }

        [Test]
        public async Task Create_當物件沒有重複且新增成功_應回傳true和成功訊息()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(false);
            _fakeRepository.Create(Arg.Any<CompanyEntity>())
                .Returns(true);

            // Actual
            var actual = await sut.Create(_fixture.Build<CompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Success.Should().BeTrue();
            actual.Message.Should().Be("Create success");
        }

        [Test]
        public async Task Update_當輸入物件不存在_應回傳不存在訊息()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(false);

            // Actual
            var actual = await sut.Update(_fixture.Build<CompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Success.Should().BeFalse();
            actual.Message.Should().Be("This company is no exists");
        }

        [Test]
        public void Update_當物件存在但更新出現例外_應拋出Exception()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(true);
            _fakeRepository.Update(Arg.Any<CompanyEntity>())
                .Returns((Func<CallInfo, bool>)(x => throw new Exception()));

            // Actual
            Func<Task> actual = async () => await sut.Update(_fixture.Build<CompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Should().ThrowAsync<Exception>();
        }

        [Test]
        public async Task Update_當物件存在且更新失敗_應回傳false和失敗訊息()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(true);
            _fakeRepository.Update(Arg.Any<CompanyEntity>())
                .Returns(false);

            // Actual
            var actual = await sut.Update(_fixture.Build<CompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Success.Should().BeFalse();
            actual.Message.Should().Be("Update failed");
        }

        [Test]
        public async Task Update_當物件存在且更新成功_應回傳true和成功訊息()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(true);
            _fakeRepository.Update(Arg.Any<CompanyEntity>())
                .Returns(true);

            // Actual
            var actual = await sut.Update(_fixture.Build<CompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Success.Should().BeTrue();
            actual.Message.Should().Be("Update success");
        }

        // Delete

        [Test]
        public async Task Delete_當輸入物件不存在_應回傳不存在訊息()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(false);

            // Actual
            var actual = await sut.Delete(_fixture.Build<QueryCompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Success.Should().BeFalse();
            actual.Message.Should().Be("This company is no exists");
        }

        [Test]
        public void Delete_當物件存在但刪除出現例外_應拋出Exception()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(true);
            _fakeRepository.Delete(Arg.Any<QueryCompanyEntity>())
                .Returns((Func<CallInfo, bool>)(x => throw new Exception()));

            // Actual
            Func<Task> actual = async () => await sut.Delete(_fixture.Build<QueryCompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Should().ThrowAsync<Exception>();
        }

        [Test]
        public async Task Delete_當物件存在且刪除失敗_應回傳false和失敗訊息()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(true);
            _fakeRepository.Delete(Arg.Any<QueryCompanyEntity>())
                .Returns(false);

            // Actual
            var actual = await sut.Delete(_fixture.Build<QueryCompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Success.Should().BeFalse();
            actual.Message.Should().Be("Delete failed");
        }

        [Test]
        public async Task Delete_當物件存在且刪除成功_應回傳true和成功訊息()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            _fakeRepository.IsExists(Arg.Any<QueryCompanyEntity>()).Returns(true);
            _fakeRepository.Delete(Arg.Any<QueryCompanyEntity>())
                .Returns(true);

            // Actual
            var actual = await sut.Delete(_fixture.Build<QueryCompanyDTO>().Create());

            // Assert
            actual.Should().NotBeNull();
            actual.Success.Should().BeTrue();
            actual.Message.Should().Be("Delete success");
        }
    }
}