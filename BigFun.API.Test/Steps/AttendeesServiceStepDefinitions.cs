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
public class AttendeesServiceStepDefinitions:WebApplicationFactory<Program>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AttendeesServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    private HttpClient Client { get; set; } 
    private Uri BaseUri { get; set; } 
    private Task<HttpResponseMessage> Response { get; set; }
    
    
    [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/attendees is available")]
    public void GivenTheEndpointHttpsLocalhostApiVAttendeesIsAvailable(int port, int version)
    {
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/organizers"); 
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = BaseUri
        });
    }

    [When(@"a Post Request is sent")]
    public void WhenAPostRequestIsSent(Table saveAttendeeResource)
    {
        var resource = saveAttendeeResource.CreateSet<SaveAttendeeResource>().First(); 
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
    
    
    [Then(@"a Attendee Resource is included in Response Body")]
    public async Task ThenAAttendeeResourceIsIncludedInResponseBody(Table expectedAttendeeResource)
    {
        var expectedResource = expectedAttendeeResource.CreateSet<AttendeeResource>().First(); 
        var responseData = await Response.Result.Content.ReadAsStringAsync(); 
        var resource = JsonConvert.DeserializeObject<AttendeeResource>(responseData); 
      
        Assert.Equal(expectedResource.Email, resource.Email);     
    }

    [Given(@"A Attendee is already stored")]
    public async void GivenAAttendeeIsAlreadyStored(Table saveAttendeeResource)
    {
        var resource = saveAttendeeResource.CreateSet<SaveAttendeeResource>().First(); 
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json); Response = Client.PostAsync(BaseUri, content); 
        var responseData = await Response.Result.Content.ReadAsStringAsync();
        var responseResource = new AttendeeResource();
        try
        {
            responseResource = JsonConvert.DeserializeObject<AttendeeResource>(responseData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        Assert.Equal(resource.Email, responseResource.Email);
    }
    
    [Then(@"An Error Message is returned with value ""(.*)""")]
    public void ThenAnErrorMessageIsReturnedWithValue(string expectedMessage)
    {
        var message = Response.Result.Content.ReadAsStringAsync().Result; 
        Assert.Equal(expectedMessage, message);
    }
}










