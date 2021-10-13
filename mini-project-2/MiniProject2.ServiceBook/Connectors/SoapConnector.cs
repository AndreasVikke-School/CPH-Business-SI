using System;
using System.Threading.Tasks;
using MiniProject1.ClassLib.SoapServices;

namespace MiniProject2.Connectors
{
    public class SoapConnector
    {
        public static async Task<bool> ISBN13Validator(string isbn) 
        {
            SBNServiceSoapTypeClient client = new SBNServiceSoapTypeClient(SBNServiceSoapTypeClient.EndpointConfiguration.ISBNServiceSoap);
            IsValidISBN13Response response = await client.IsValidISBN13Async(isbn);
            return response.Body.IsValidISBN13Result;
        }
        
        public static async Task<bool> ISBN10Validator(string isbn) 
        {
            SBNServiceSoapTypeClient client = new SBNServiceSoapTypeClient(SBNServiceSoapTypeClient.EndpointConfiguration.ISBNServiceSoap);
            IsValidISBN10Response response = await client.IsValidISBN10Async(isbn);
            return response.Body.IsValidISBN10Result;
        }
    }
}
