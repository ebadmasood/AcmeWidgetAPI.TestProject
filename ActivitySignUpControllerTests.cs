using AcmeWidgetAPI.Controllers;
using AcmeWidgetAPI.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AcmeWidgetAPI.Test.Controllers
{
    public class ActivitySignUpControllerTests
    {
        private readonly ActivitySignUpController _target;
        private readonly Mock<IActivitySignUpRepository> _repository;

        public ActivitySignUpControllerTests()
        {
            _repository = new Mock<IActivitySignUpRepository>();
            _target = new ActivitySignUpController(_repository.Object);
        }

        [Fact]
        public async Task ActivitySignUpController_GetAllSignUpForms_SuccessfulAsync()
        {
            IEnumerable<ActivitySignUpForm> list = new List<ActivitySignUpForm> {
                new ActivitySignUpForm
                {
                    FirstName = "Ebad",
                    LastName = "Masood",
                    EmailAddress = "ebad.masood@gmail.com",
                    Activity = Activity.Dancing,

                }
            };
             _repository.Setup(h => h.GetAllSignUpForms()).Returns(Task.FromResult(list));
            var result = await _target.GetForms();
            _repository.Verify(h => h.GetAllSignUpForms(), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ActivitySignUpController_Create_SuccessfulAsync()
        {
            var form = new ActivitySignUpForm
            {
                FirstName = "Ebad",
                LastName = "Masood",
                EmailAddress = "ebad.masood@gmail.com",
                Activity = Activity.Dancing,

            };
            _repository.Setup(h => h.Create(form)).Returns(Task.FromResult(form));
            var result = await _target.Create(form);
            _repository.Verify(h => h.Create(form), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ActivitySignUpController_Create_UnsuccessfulAsync()
        {
            var form = new ActivitySignUpForm
            {
                FirstName = "Ebad",
                EmailAddress = "ebad.masood@gmail.com",
                Activity = Activity.Dancing,

            };
            await Assert.ThrowsAsync<Exception>(() =>  _target.Create(form));
            _repository.Verify(h => h.Create(form), Times.Never);
        }

    }
}
