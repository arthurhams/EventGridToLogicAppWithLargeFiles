using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace azuredemowebapp
{
    public static class Homepage
    {
        [FunctionName("Homepage")]
        public static async Task<HttpResponseMessage> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string name = req.Query["name"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    name = name ?? data?.name;

    string responseMessage = string.IsNullOrEmpty(name)
        ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";
//            return new OkObjectResult(responseMessage);

    responseMessage = "<!DOCTYPE html><html lang='en'>";
    responseMessage += "<head><meta charset='UTF-8'>  <meta name='viewport' content='width=device-width, initial-scale=1.0'>";
responseMessage += "<link rel='stylesheet' href='https://static.azuredemowebapp.com/styles.css'>";
  responseMessage += "<title>Azure Function App</title></head><body>  <main>";
  responseMessage += "  <h1>Azure Function App</h1></main></body></html>";

var response = new HttpResponseMessage(HttpStatusCode.OK);
    response.Content = new StringContent(responseMessage);
   response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
  return response;


}
    }
}
