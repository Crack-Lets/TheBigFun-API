using System.Net;
using System.Net.Mime;
using System.Text;
using BigFun.API.Booking.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace BigFun.API.Test.Steps;

[Binding]
public sealed class OrganizersServiceStepDefinitions:WebApplicationFactory<Program>
{
    private readonly WebApplicationFactory<Program> _factory;

    public OrganizersServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    private HttpClient Client { get; set; } 
    private Uri BaseUri { get; set; } 
    private Task<HttpResponseMessage> Response { get; set; }


    //indica que este paso se ejecutará cuando se cumpla la condición de que el endpoint https://localhost:{port}/api/v{version}/organizers esté disponible.
    [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/organizers is available")]
    public void GivenTheEndpointHttpsLocalhostApiVOrganizersIsAvailable(int port, int version)
    { 
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/organizers"); 
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = BaseUri
        });    
    }
    
    //indica que este paso se ejecutará cuando se envíe una solicitud POST.
    [When(@"a Post Request is sent")]
    public void WhenAPostRequestIsSent(Table saveOrganizerResource)
    {
        var resource = saveOrganizerResource.CreateSet<SaveOrganizerResource>().First(); 
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json); 
        Response = Client.PostAsync(BaseUri, content);    
    }
    
    //indica que este paso se ejecutará para verificar el estado de la respuesta.
    [Then(@"A Response is received with Status (.*)")]
    public void ThenAResponseIsReceivedWithStatus(int expectedStatus)
    {
        var expectedStatusCode = ((HttpStatusCode)expectedStatus).ToString(); 
        var actualStatusCode = Response.Result.StatusCode.ToString(); 
      
        Assert.Equal(expectedStatusCode, actualStatusCode);    
    }

    //indica que este paso se ejecutará para verificar la presencia del recurso de organizer en el cuerpo de la respuesta.
    [Then(@"a Organizer Resource is included in Response Body")]
    public async Task ThenAOrganizerResourceIsIncludedInResponseBody(Table expectedOrganizerResource)
    {
        var expectedResource = expectedOrganizerResource.CreateSet<OrganizerResource>().First(); 
        var responseData = await Response.Result.Content.ReadAsStringAsync(); 
        var resource = JsonConvert.DeserializeObject<OrganizerResource>(responseData); 
      
        Assert.Equal(expectedResource.Email, resource.Email);    
    }

    //indica que este paso se ejecutará para simular la existencia de un organizer almacenado previamente.
    [Given(@"A Organizer is already stored")]
    public async void GivenAOrganizerIsAlreadyStored(Table saveOrganizerResource)
    {
        var resource = saveOrganizerResource.CreateSet<SaveOrganizerResource>().First(); 
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json); Response = Client.PostAsync(BaseUri, content); 
        var responseData = await Response.Result.Content.ReadAsStringAsync(); 
        var responseResource = JsonConvert.DeserializeObject<OrganizerResource>(responseData); 
      
        Assert.Equal(resource.Email, responseResource.Email);
    }

    //indica que este paso se ejecutará para verificar si se devuelve un mensaje de error con el valor esperado.
    [Then(@"An Error Message is returned with value ""(.*)""")]
    public void ThenAnErrorMessageIsReturnedWithValue(string expectedMessage)
    {
        var message = Response.Result.Content.ReadAsStringAsync().Result; 
        Assert.Equal(expectedMessage, message);
    }
    
}