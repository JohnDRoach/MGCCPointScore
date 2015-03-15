using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories.Members;
using Moq;
using Repositories.Database;
using MongoDB.Driver;

namespace Repositories.Tests
{
    [TestClass]
    public class MemberRepositoryContractTests
    {
        [TestMethod]
        [ExpectedException(typeof(DuplicateMemberNameException))]
        public void AddingAMemberWithTheSameNameAsAnExistingMemberThrowsException()
        {
            Setup();

            repository.Add(new ClubMember("ExistingName", Sex.Other, "Note"));
        }

        [TestMethod]
        public void AddingANewMemberCallsAddOnInternalRepository()
        {
            Setup();
            mockInternalRepo.Setup(x => x.GetFromQuery(It.IsAny<IMongoQuery>())).Throws(new ItemNotFoundException(""));

            repository.Add(new ClubMember("NewName", Sex.Other, "Note"));

            mockInternalRepo.Verify(x => x.Add(It.Is<ClubMember>(m => m.Name.Equals("NewName"))));
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateMemberNameException))]
        public void UpdatingAMemberWithANameThatSomeoneElseHasThrowsException()
        {
            Setup();

            repository.Update(new ClubMember("ExistingName", Sex.Other, "Note") { Id = Guid.NewGuid() });
        }

        [TestMethod]
        public void UpdatingAMemberWithANewNameCallsUpdateOnInternalRepository()
        {
            Setup();
            var id = Guid.NewGuid();
            mockInternalRepo.Setup(x => x.GetFromQuery(It.IsAny<IMongoQuery>())).Throws(new ItemNotFoundException(""));

            repository.Update(new ClubMember("NewName", Sex.Other, "Note") { Id = id });

            mockInternalRepo.Verify(x => x.Update(It.Is<ClubMember>(m => m.Name.Equals("NewName") && m.Id.Equals(id))));
        }

        [TestMethod]
        public void DeleteCallsDeleteOnTheInternalRepository()
        {
            Setup();
            var id = Guid.NewGuid();

            repository.Delete(new ClubMember("ExistingName", Sex.Other, "Note") { Id = id });

            mockInternalRepo.Verify(x => x.Delete(It.Is<ClubMember>(m => m.Id.Equals(id))));
        }

        [TestMethod]
        public void GetFromIdCallsGetFromIdOnTheInternalRepository()
        {
            Setup();
            Guid id = Guid.NewGuid();

            repository.GetFromId(id);

            mockInternalRepo.Verify(x => x.GetFromId(id));
        }

        [TestMethod]
        public void GetFromIdReturnsResultFromInternalCallOnGetFromId()
        {
            Setup();
            ClubMember member = new ClubMember("Name", Sex.Other, "Note");
            mockInternalRepo.Setup(x => x.GetFromId(It.IsAny<Guid>())).Returns(member);

            var result = repository.GetFromId(Guid.NewGuid());

            Assert.ReferenceEquals(member, result);
        }


        private MemberRepository repository;
        private Mock<IMongoDBRepository<ClubMember>> mockInternalRepo;

        private void Setup()
        {
            mockInternalRepo = new Mock<IMongoDBRepository<ClubMember>>();

            repository = new MemberRepository(mockInternalRepo.Object);
        }
    }
}
