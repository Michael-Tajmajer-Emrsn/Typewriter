﻿using System.Linq;
using Should;
using Typewriter.CodeModel;
using Typewriter.Extensions.WebApi;
using Typewriter.Tests.TestInfrastructure;
using Xunit;

namespace Typewriter.Tests.Extensions
{
    [Trait("Extensions", "WebApi"), Collection(nameof(CodeDomFixture))]
    public class CodeDomWebApiRouteClassRouteExtensionsTests : WebApiRouteClassRouteExtensionsTests
    {
        public CodeDomWebApiRouteClassRouteExtensionsTests(CodeDomFixture fixture) : base(fixture)
        {
        }
    }

    [Trait("Extensions", "WebApi"), Collection(nameof(RoslynFixture))]
    public class RoslynWebApiRouteClassRouteExtensionsTests : WebApiRouteClassRouteExtensionsTests
    {
        public RoslynWebApiRouteClassRouteExtensionsTests(RoslynFixture fixture) : base(fixture)
        {
        }
    }

    public abstract class WebApiRouteClassRouteExtensionsTests : TestBase
    {
        private readonly File fileInfo;

        protected WebApiRouteClassRouteExtensionsTests(ITestFixture fixture) : base(fixture)
        {
            fileInfo = GetFile(@"Tests\Extensions\Support\RouteControllerWithDefaultRoute.cs");
        }

        [Fact]
        public void Expect_to_find_parameters_on_wildcard_route_url()
        {
            var classInfo = fileInfo.Classes.First();
            var methodInfo = classInfo.Methods.First(p => p.Name == "WildcardRoute");
            var result = methodInfo.Url();
            result.ShouldEqual("api/RouteControllerWithDefaultRoute/${encodeURIComponent(key)}");
        }

        [Fact]
        public void Expect_to_find_url_on_named_route()
        {
            var classInfo = fileInfo.Classes.First();
            var methodInfo = classInfo.Methods.First(p => p.Name == "NamedRoute");
            var result = methodInfo.Url();
            result.ShouldEqual("api/RouteControllerWithDefaultRoute/${id}");
        }

        [Fact]
        public void Expect_to_find_url_on_route_in_http_attribute()
        {
            var classInfo = fileInfo.Classes.First();
            var methodInfo = classInfo.Methods.First(p => p.Name == "RouteInHttpAttribute");

            methodInfo.Url().ShouldEqual("api/RouteControllerWithDefaultRoute/${id}");
        }

        [Fact]
        public void Expect_to_find_url_on_subroute_in_http_attribute()
        {
            var classInfo = fileInfo.Classes.First();
            var methodInfo = classInfo.Methods.First(p => p.Name == "SubRouteInHttpAttribute");

            methodInfo.Url().ShouldEqual("api/RouteControllerWithDefaultRoute/sub/${id}");
        }
    }
}