using System.Net;

public class HttpService
{
    private HttpClient client;
    public HttpService(string address)
    {
        client = new HttpClient();
        client.BaseAddress = new Uri(address);
    }

    public async Task<bool> CpfValidation(string cpf)
    {
        var response = await client
            .GetAsync("/cpf/validate/" + cpf);
        
        if (response.StatusCode != HttpStatusCode.OK)
            return true;
        
        var result = await response.Content
            .ReadFromJsonAsync<APIResult<bool>>();
        
        if (result.Status == "Fail")
            return true;
        
        return result.Data;
    }
}