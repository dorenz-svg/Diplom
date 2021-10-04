using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using Diplom.Models.Response;
using Diplom.Models.Repositories.Abstract;
using System.Threading.Tasks;
using System.Linq;
using Diplom.Controllers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Tests
{
    public class DialogsTest
    {
        [Fact]
        public async void GetDialogs()
        {
            var mockDialogs = new List<DialogResponse>();
            for(int i=0; i < 10; i++)
            {
                mockDialogs.Add(new DialogResponse {
                Id=i,
                Message = new Message { Id=i,Text="Ky",Time=DateTime.UtcNow,UserName="Mike" },
                Name=$"Dialog {i}"
                });
            }
            string mockId = "some id";
            var mockRepository = new Mock<IDialogRepository>();
            mockRepository.Setup(x => x.GetDialogs(mockId)).Returns(() => Task.FromResult(mockDialogs.AsEnumerable()));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst(ClaimTypes.NameIdentifier)).Returns(new Claim(ClaimTypes.NameIdentifier,mockId));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));
            var dialogsController = new DialogsController(mockRepository.Object) { ControllerContext = context };
            var result = await dialogsController.GetDialogs();
            Assert.Equal(10, result.Count());
            mockRepository.Verify(x => x.GetDialogs("some id"), Times.Once);
        }
    }
}
