using System.Net;

namespace ELECON.Application.Security;

public static class GetUserLocation
{
     public static string GetCountry()
    {
        return new WebClient().DownloadString("http://api.hostip.info/country.php");
    }
}