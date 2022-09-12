using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using RechargeSharp.v2021_11.Endpoints.Addresses;
using RechargeSharp.v2021_11.Endpoints.Products;
using RechargeSharp.v2021_11.Tests.TestHelpers;
using RechargeSharp.v2021_11.Tests.TestResources.SampleResponses.Addresses;
using RechargeSharp.v2021_11.Tests.TestResources.SampleResponses.Products;
using Xunit;

namespace RechargeSharp.v2021_11.Tests.Endpoints.Products;

public class ProductsServiceIntegrationTests
{
    /// <summary>
    ///     Tests that the service behaves as expected when the Recharge API returns certain successful HTTP responses
    /// </summary>
    [Theory]
    [MemberData(nameof(RechargeApiHttpResponseSuccessTestCases))]
    public async Task TestingSuccessResponseCodes<T>(string sampleResponseJsonFile, HttpStatusCode httpStatusCode, string uriToMatch, HttpMethod method, Func<ProductService, Task<T>> apiCallerFunc, T? expectedDeserializedResponse)
    {
        // Arrange
        var sampleResponseJson = await TestResourcesHelper.GetSampleResponseJson(sampleResponseJsonFile);
        var handlerMock = HttpHandlerMocking.SetupHttpHandlerMock_ReturningJsonWithStatusCode(sampleResponseJson, httpStatusCode, uriToMatch, method);
        var apiCaller = RechargeApiCallerMocking.CreateRechargeApiCallerWithMockedHttpHandler(handlerMock);
        
        var sut = new ProductService(apiCaller);
        
        // Act
        var result = await apiCallerFunc(sut);
        
        // Assert
        if (expectedDeserializedResponse != null)
            result.Should().BeEquivalentTo(expectedDeserializedResponse);
        else
            result.Should().BeNull();
    }
    
    public static IEnumerable<object[]> RechargeApiHttpResponseSuccessTestCases()
    {
        var fixture = new Fixture();
        
        yield return new object[]
        {
            // Get all products
            "Products/get-products_200.json",
            HttpStatusCode.OK,
            "/products",
            HttpMethod.Get,
            new Func<ProductService, Task<ProductService.GetProductsTypes.Response>>(service => service.GetProductsAsync(fixture.Create<ProductService.GetProductsTypes.Request>())),
            get_products_200.CorrectlyDeserializedJson()
        };
    }
}