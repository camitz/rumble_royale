using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

namespace HelloWorldAot.Tests;

public class FunctionTest
{
  private static readonly HttpClient client = new HttpClient();

  private static async Task<string> GetCallingIP()
  {
          client.DefaultRequestHeaders.Accept.Clear();
          client.DefaultRequestHeaders.Add("User-Agent", "AWS Lambda .Net Client");

          var stringTask = client.GetStringAsync("http://checkip.amazonaws.com/").ConfigureAwait(continueOnCapturedContext:false);

          var msg = await stringTask;
          return msg.Replace("\n","");
  }

  [Fact]
  public async Task TestHelloWorldFunctionHandler()
  {
          var request = new APIGatewayHttpApiV2ProxyRequest();
          var context = new TestLambdaContext();
          string location = GetCallingIP().Result;
          var body = new Output("1000", "b21351ceb0292c4e755e911918e40cd9");

          var expectedResponse = new APIGatewayHttpApiV2ProxyResponse
          {
              Body = JsonSerializer.Serialize(body),
              StatusCode = 200,
              Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
          };

          var response = await new Function().FunctionHandler("1000");

          Console.WriteLine("Lambda Response: \n" + response);
          Console.WriteLine("Expected Response: \n" + expectedResponse.Body);

          Assert.Equal(body, response);
  }
}