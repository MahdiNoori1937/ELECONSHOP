using System.Net;
using System.Text;
using ELECON.Domain.Interface.SmsSender;
using Elecon.Infrastructure.Send;
using EleconShop.Domain.Dtos;
using Microsoft.Extensions.Options;
using mpNuget;
using Newtonsoft.Json;

namespace Elecon.Infrastructure.Repositories.SmsSenderRepositories;

public class SmsSenderService: ISmsSenderService
{
    
    private readonly MeliPayamak meliPayamak;

    public SmsSenderService(IOptions<MeliPayamak> melipayamak)
    {
        meliPayamak = melipayamak.Value;
    }
    private RestResponse makeRequest(Dictionary<string, string> values, string op, string endpoint)
    {
        HttpWebRequest request = WebRequest.CreateHttp(endpoint + op);
        request.Method = "POST";

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    string responseJSON = myStreamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<RestResponse>(responseJSON);
                }
            }
        }
    }

    public async Task MeliSmsSender(string text, string to)
    {
       SendSoapClient send = new SendSoapClient(SendSoapClient.EndpointConfiguration.SendSoap);
       await send.SendByBaseNumber2Async(meliPayamak.username, meliPayamak.password, text, to,
            meliPayamak.bodyId);
    }
}