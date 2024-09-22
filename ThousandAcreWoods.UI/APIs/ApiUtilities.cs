using Microsoft.AspNetCore.WebUtilities;
using NYK.Collections.Extensions;
using System.Buffers.Text;

namespace ThousandAcreWoods.UI.APIs;

public static class ApiUtilities
{

    public static void Map(this WebApplication app, GroupDefinition groupDefinition, params EndpointDefinition[] endpointDefinitions)
    {
        var group = app.MapGroup(groupDefinition.Name);
        group.WithTags(groupDefinition.Name)
             .WithOpenApi();
        if (groupDefinition.AuthorizationPolicy != null)
            group.RequireAuthorization(groupDefinition.AuthorizationPolicy);
        foreach(var endpointDef in endpointDefinitions)
        {
            var conventionBuilder = endpointDef.Method switch
            {
                HttpMethod.Get => group.MapGet(endpointDef.Pattern, endpointDef.Delegate),
                HttpMethod.Post => group.MapPost(endpointDef.Pattern, endpointDef.Delegate),
                HttpMethod.Put => group.MapPut(endpointDef.Pattern, endpointDef.Delegate),
                HttpMethod.Delete => group.MapDelete(endpointDef.Pattern, endpointDef.Delegate),
                _ => group.MapPatch(endpointDef.Pattern, endpointDef.Delegate)
            };
            conventionBuilder.WithOpenApi();
            conventionBuilder.WithDescription(endpointDef.Description);
            if(endpointDef.AuthorizationPolicy != null)
                conventionBuilder.RequireAuthorization(endpointDef.AuthorizationPolicy);
        }
    }

    public record GroupDefinition(string Name, string? AuthorizationPolicy);

    public record EndpointDefinition(string Name, string Pattern, HttpMethod Method, Delegate Delegate, string Description, string? AuthorizationPolicy = null);

    public enum HttpMethod
    {
        Get = 1,
        Put = 2,
        Post = 3,
        Patch = 4,
        Delete = 5
    }

    public static byte[] ParseBase64BlobString(string base64BlobString)
    {
        var relIndex = base64BlobString.IndexOf("base64,"); //base64,
        var relString = base64BlobString.Substring(relIndex + 7, base64BlobString.Length - relIndex - 7);
        var parsed = Base64UrlTextEncoder.Decode(relString);
        return parsed;
    }



    public static string? ValidateImageFiles(this IWebHostEnvironment env, string imageSubFolder, string? backgroundFile, string rewardFile, string winnerFile)
    {
        if (backgroundFile != null)
        {
            var backgroundPath = env.ImageFile(imageSubFolder, backgroundFile);
            if (!Path.Exists(backgroundPath))
                return $"Background file: {backgroundPath} does not exist";
            var backgroundFileTypeError = ImageFileTypeError("Background", backgroundFile);
            if (backgroundFileTypeError != null)
                return backgroundFileTypeError;
        }
        var rewardPath = env.ImageFile(imageSubFolder, rewardFile);
        if (!Path.Exists(rewardPath))
            return $"Reward file: {rewardPath} does not exist";
        var rewardFileTypeError = ImageFileTypeError("Reward", rewardFile);
        if (rewardFileTypeError != null)
            return rewardFileTypeError;

        var winnerPath = env.ImageFile(imageSubFolder, winnerFile);
        if (!Path.Exists(winnerPath))
            return $"Winner file: {winnerPath} does not exist";
        var winnerFileTypeError = ImageFileTypeError("Winner", winnerFile);
        if (winnerFileTypeError != null)
            return winnerFileTypeError;
        return null;

    }

    private static string ImageFile(this IWebHostEnvironment env, string subFolder, string fileName) =>
        Path.Combine(env.WebRootPath, subFolder, fileName);


    private static string? ImageFileTypeError(string fileUsage, string fileName) =>
            ApiConstants.ImageFileTypes.Any(_ => fileName.ToLower().EndsWith(_)) ?
            null :
            $"{fileUsage} file type has to be one of: {ApiConstants.ImageFileTypes.MakeString(",")}";





}
