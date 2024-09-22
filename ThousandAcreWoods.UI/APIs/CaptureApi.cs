using Microsoft.AspNetCore.Mvc;
using ThousandAcreWoods.Application.Capture.Infrastructure;
using static ThousandAcreWoods.UI.APIs.ApiUtilities;

namespace ThousandAcreWoods.UI.APIs;

public static class CaptureApi
{
    public static readonly GroupDefinition GroupDefinition = new(
        Name: "Capture",
        AuthorizationPolicy: null
        );


    public static readonly IReadOnlyCollection<EndpointDefinition> Endpoints = [
        new EndpointDefinition(
            Name: "save-bytes",
            Pattern: "save-bytes",
            Method: ApiUtilities.HttpMethod.Post,
            Delegate: async(
                    [FromServices] ICaptureFileWriter captureFileWriter ,
                    [FromBody] CaptureSubmitData submitData
                ) => {
                    var parsedBytes = ParseBase64BlobString(submitData.captureData);
                    await captureFileWriter.WriteFile(parsedBytes, submitData.captureName);
            },
            Description: "Received captured bytes"
            )
        ];

    public static WebApplication UseCaptureApi(this WebApplication app)
    {
        app.Map(GroupDefinition, Endpoints.ToArray());
        return app;
    }

    public record CaptureSubmitData(
        string captureName,
        string captureData
        );
}
