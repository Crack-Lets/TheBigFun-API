using System.Net;
using System.Net.Mime;
using System.Text;
using BigFun.API.Booking.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace BigFun.API.Test.Steps;

[Binding]
public sealed class EventsServiceStepDefinitions: WebApplicationFactory<Program>
{
    private readonly WebApplicationFactory<Program> _factory;

    public EventsServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    private HttpClient Client { get; set; } 
    private Uri BaseUri { get; set; } 
    private Task<HttpResponseMessage> Response { get; set; }
    
    [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/events is available")]
    public void GivenTheEndpointHttpsLocalhostApiVEventsIsAvailable(int port, int version)
    {
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/events"); 
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = BaseUri
        });      
    }
    
    [When(@"a Post Request is sent")]
    public void WhenAPostRequestIsSent(Table saveEventResource)
    {
        var resource = saveEventResource.CreateSet<SaveEventResource>().First(); 
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json); 
        Response = Client.PostAsync(BaseUri, content);    
    }
    
    [Then(@"A Response is received with Status (.*)")]
    public void ThenAResponseIsReceivedWithStatus(int expectedStatus)
    {
        var expectedStatusCode = ((HttpStatusCode)expectedStatus).ToString(); 
        var actualStatusCode = Response.Result.StatusCode.ToString(); 
      
        Assert.Equal(expectedStatusCode, actualStatusCode);    
    }

    [Then(@"a Event Resource is included in Response Body")]
    public async Task ThenAEventResourceIsIncludedInResponseBody(Table expectedEventResource)
    {
        var expectedResource = expectedEventResource.CreateSet<EventResource>().First(); 
        var responseData = await Response.Result.Content.ReadAsStringAsync(); 
        var resource = JsonConvert.DeserializeObject<OrganizerResource>(responseData); 
      
        Assert.Equal(expectedResource.Name, resource.Name);
        
    }

    [Given(@"A Event is already stored")]
    public async void GivenAEventIsAlreadyStored(Table saveEventResource)
    {
        var resource = saveEventResource.CreateSet<SaveEventResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        Response = Client.PostAsync(BaseUri, content);
        var responseData = await Response.Result.Content.ReadAsStringAsync();
        var responseResource = new EventResource();
        try
        {
            responseResource = JsonConvert.DeserializeObject<EventResource>(responseData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Assert.Equal(resource.Name, responseResource.Name);
    }
    
    [Then(@"An Error Message is returned with value ""(.*)""")]
    public void ThenAnErrorMessageIsReturnedWithValue(string expectedMessage)
    {
        var message = Response.Result.Content.ReadAsStringAsync().Result; 
        Assert.Equal(expectedMessage, message);
    }
}









