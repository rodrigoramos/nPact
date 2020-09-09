using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using nPact.Common.Contracts;
using nPact.Consumer.Rendering;
using Xunit;

namespace nPact.Consumer.Tests.Rendering
{
    public class JsonBodyTests
    {
        [Fact]
        public void RenderWithoutBody_ShouldReturnNull()
        {
            var jBody = new JsonBody(null);
            jBody.Render().Should().BeNull();
        }

        [Fact]
        public void RenderWithIJsonable_ShouldCallRenderMethodOfBody()
        {
            var iJsonableMock = new Mock<IJsonable>();
            var jBody = new JsonBody(iJsonableMock.Object);

            jBody.Render();

            iJsonableMock.Verify(x => x.Render(), Times.Once());
        }

        [Fact]
        public void RenderWithString_ShouldParseAsJsonObject()
        {
            var jsonObjectString = @"{ prop1: 'value' }";
            var jBody = new JsonBody(jsonObjectString);

            jBody.Render()
                .Should()
                    .BeOfType<JObject>()
                .Which
                    .GetValue("prop1").Value<string>().Should().Be("value");

        }

        [Fact]
        public void RenderWithArray_ShouldParseAsJArrayObject()
        {
            var bodyArray = new[] { "value1", "value2" };
            var jBody = new JsonBody(bodyArray);

            jBody.Render()
                .Should()
                    .BeOfType<JArray>()
                .Which
                    .Count.Should().Be(bodyArray.Length);

        }

        [Fact]
        public void RenderWithObject_ShouldCreateAJObject() 
        {
            var jObject = new { prop1 = "value1" };
            var jBody = new JsonBody(jObject);

            jBody.Render()
                .Should()
                    .BeOfType<JObject>()
                .Which
                    .GetValue("prop1").Value<string>().Should().Be("value1");
        }
    }
}