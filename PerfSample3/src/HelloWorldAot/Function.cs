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
    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/{size}")]
    public async Task<Output> FunctionHandler(string size)
    {
        Console.WriteLine($"size: {size}");
        var mandelbrotHash = MandelBrot.Do(int.Parse(size));
        return new Output(size, mandelbrotHash);
    }
}

public record Output(string size, string mandelbrot);

[JsonSerializable(typeof(APIGatewayProxyRequest))]
[JsonSerializable(typeof(APIGatewayProxyResponse))]
[JsonSerializable(typeof(Dictionary<string, string>))]
[JsonSerializable(typeof(Output))]
public partial class LambdaFunctionJsonSerializerContext : JsonSerializerContext;
