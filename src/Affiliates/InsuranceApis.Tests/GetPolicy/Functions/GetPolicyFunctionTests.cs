using System;
using InsuranceApis.GetPolicy.Functions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InsuranceApis.Tests.GetPolicy.Functions
{
    [TestClass]
    public class GetPolicyFunctionTests
    {
        private Mock<GetPolicyFunction> _sut;
        private Mock<ILogger> _log;

        public GetPolicyFunctionTests()
        {
            _sut = new Mock<GetPolicyFunction>();
            _log = new Mock<ILogger>();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void GetPolicyFunction_ArgumentException_When_Payload_Wrong_Type()
        {
            // arrange


            // act
            var result = _sut.Object.InvokeAsync<object, object>(new { }, _log.Object).Result;

            // assert
        }
    }
}
