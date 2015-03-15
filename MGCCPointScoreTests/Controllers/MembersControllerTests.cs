using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MGCCPointScore.Controllers;
using Repositories.Members;
using MGCCPointScore.Models;
using Moq;

namespace MGCCPointScoreTests.Controllers
{
    [TestClass]
    public class MembersControllerTests
    {
        [TestMethod]
        public void CreateMemberCallsAddOnRepositoryWithGuidEmpty()
        {
            Setup();

            When.CreateIsPOSTedWithThisViewModel(new ClubMemberViewModel { Name = "Test" });

            mockMemberRepo.Verify(x => x.Add(It.Is<ClubMember>(m => m.Id.Equals(Guid.Empty))));
        }

        [TestMethod]
        public void CreateMemberCallsAddOnRepositoryWithPassedInValues()
        {
            Setup();

            When.CreateIsPOSTedWithThisViewModel(new ClubMemberViewModel { Name = "Test" });

            mockMemberRepo.Verify(x => x.Add(It.Is<ClubMember>(m => m.Name.Equals("Test"))));
        }

        [TestMethod]
        public void CreateMemberForNewNameReturnsNoErrors()
        {
            Setup();

            When.CreateIsPOSTedWithThisViewModel(new ClubMemberViewModel { Name = "NewName" });

            Assert.IsFalse(controller.ModelState.Values.Any(x => x.Errors.Count > 0));
        }

        [TestMethod]
        public void CreateMemberForExistingNameReturnsAnError()
        {
            Setup();

            Given.ExistingNameInDB();

            When.CreateIsPOSTedWithThisViewModel(new ClubMemberViewModel { Name = "TestName" });

            Assert.IsTrue(controller.ModelState.Values.Any(x => x.Errors.Count > 0));
        }


        private MembersControllerTests Given { get { return this; } }
        private MembersControllerTests When { get { return this; } }

        private Mock<IMemberRepository> mockMemberRepo;
        private MembersController controller;

        private void Setup()
        {
            mockMemberRepo = new Mock<IMemberRepository>();
            controller = new MembersController(mockMemberRepo.Object);
        }

        private void ExistingNameInDB()
        {
            mockMemberRepo.Setup(x => x.Add(It.IsAny<ClubMember>())).Throws(new DuplicateMemberNameException("Test"));
        }

        private void CreateIsPOSTedWithThisViewModel(ClubMemberViewModel viewModel)
        {
            var returnedThing = controller.Create(viewModel);
            
            if (!returnedThing.IsCompleted)
            {
                returnedThing.RunSynchronously();
            }
        }
    }
}
