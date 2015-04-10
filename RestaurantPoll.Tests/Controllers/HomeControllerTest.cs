using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantPoll;
using RestaurantPoll.Controllers;
using System.Web;
using System.Web.SessionState;

namespace RestaurantPoll.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        class FakeSession : HttpSessionStateBase
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            public override object this[string index]
            {
                get { return values[index] ; }
                set { values.Add(index, value); }
            }
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            UserController controller = new UserController(new FakeSession());

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
