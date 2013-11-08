using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Moq;

using NSubstitute;

using NUnit.Framework;

namespace Mocking.Tests
{
    [TestFixture]
    public class MockingTests
    {
        private Model _model;

        [SetUp]
        public void Setup()
        {
            _model = new Model { Name = "heps" };
        }

        [Test]
        public void Test()
        {
            var repo = new Repository();

            var service = new Service(repo);

            service.MethodToTest(_model);
        }
        
        [Test]
        public void TestNsubstitute()
        {
            var mockRepo = Substitute.For<IRepository>();
            
            var service = new Service(mockRepo);

            service.MethodToTest(_model);

            mockRepo.Received().Save(Arg.Is<DTO>(x => x.Name == _model.Name));
        }

        [Test]
        public void TestNsubstituteComplex()
        {
            var mockRepo = Substitute.For<IRepository>();

            var service = new Service(mockRepo);

            service.MethodToTest(_model);

            mockRepo.Received().Save(Arg.Is<DTO>((dto) => EqualityTest(dto)));
        }


        [Test]
        public void TestMoq()
        {
            var mockRepo = new Mock<IRepository>();

            mockRepo.Setup(x => x.Save(It.IsAny<DTO>()));

            var service = new Service(mockRepo.Object);

            service.MethodToTest(_model);

            mockRepo.Verify(x => x.Save(It.Is<DTO>(y => y.Name == "hop")), Times.Once);
        }

        [Test]
        public void TestMoqComplex()
        {
            var mockRepo = new Mock<IRepository>();

            mockRepo.Setup(x => x.Save(It.IsAny<DTO>()));

            var service = new Service(mockRepo.Object);

            service.MethodToTest(_model);

            mockRepo.Verify(x => x.Save(It.Is<DTO>((dto)=>EqualityTest(dto))));
        }

        private bool EqualityTest(DTO dto)
        {
            if(dto.Name.Equals(_model.Name))
                return true;
            
            return false;
        }
    }
}
