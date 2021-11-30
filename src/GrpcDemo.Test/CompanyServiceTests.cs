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
        public async Task GetById_���JId�����_���^�ǥ]�t��Id������(int input)
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
        public void GetById_��X�{�ҥ~_���ߥXException(int input)
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
        public async Task GetAll_��I�s�禡_���^�ǩҦ�����(int count)
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
        public void GetAll_��X�{�ҥ~_���ߥXException(int count)
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
        public async Task Create_���J����w�s�b_���^�ǭ��ưT��()
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
        public void Create_����S�����Ʀ��X�{�ҥ~_���ߥXException()
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
        public async Task Create_����S�����ƥB�s�W����_���^��false�M���ѰT��()
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
        public async Task Create_����S�����ƥB�s�W���\_���^��true�M���\�T��()
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
        public async Task Update_���J���󤣦s�b_���^�Ǥ��s�b�T��()
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
        public void Update_����s�b����s�X�{�ҥ~_���ߥXException()
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
        public async Task Update_����s�b�B��s����_���^��false�M���ѰT��()
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
        public async Task Update_����s�b�B��s���\_���^��true�M���\�T��()
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
        public async Task Delete_���J���󤣦s�b_���^�Ǥ��s�b�T��()
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
        public void Delete_����s�b���R���X�{�ҥ~_���ߥXException()
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
        public async Task Delete_����s�b�B�R������_���^��false�M���ѰT��()
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
        public async Task Delete_����s�b�B�R�����\_���^��true�M���\�T��()
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