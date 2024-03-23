using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Serialization.SystemTextJson;
using HelloWorldAot;

[assembly: LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<LambdaFunctionJsonSerializerContext>))]
[assembly: LambdaGlobalProperties(GenerateMain = true)]

namespace HelloWorldAot;

public class Function
{
    private static readonly HttpClient client = new HttpClient();
    private static async Task<string> GetCallingIP()
    {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Add("User-Agent", "AWS Lambda .Net Client");

        var msg = await client.GetStringAsync("http://checkip.amazonaws.com/").ConfigureAwait(continueOnCapturedContext: false);

        return msg.Replace("\n", "");
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/")]
    public async Task<Dictionary<string, string>> FunctionHandler()
    {
        var location = await GetCallingIP();
        return new Dictionary<string, string>
        {
            { "message", "hello world" },
            { "location", location }
        };
    }
}
[JsonSerializable(typeof(APIGatewayProxyRequest))]
[JsonSerializable(typeof(APIGatewayProxyResponse))]
[JsonSerializable(typeof(Dictionary<string, string>))]
public partial class LambdaFunctionJsonSerializerContext : JsonSerializerContext
{
    // By using this partial class derived from JsonSerializerContext, we can generate reflection free JSON Serializer code at compile time
    // which can deserialize our class and properties. However, we must attribute this class to tell it what types to generate serialization code for.
    // See https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-source-generation
}
