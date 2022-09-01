using System.Net;

public class HttpService
{
    private HttpClient client;
    public HttpService(string address)
    {
        client = new HttpClient();
        client.BaseAddress = new Uri(address);
    }

    public async Task RegisterUser(Usuario user)
    {
        var response = await client
            .PostAsJsonAsync("/user/register", user);
    }

    public async Task<CEP> GetCepInformation(string cep)
    {
        var response = await client
            .GetAsync("/cep/query/" + cep);
        
        if (response.StatusCode != HttpStatusCode.OK)
            return null;
        
        var result = await response.Content
            .ReadFromJsonAsync<APIResult<CEP>>();
        
        if (result.Status == "Fail")
            return null;
        
        return result.Data;
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