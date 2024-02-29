using LangChain.Providers;
using LangChain.Providers.Amazon.Bedrock;
using System.Reflection;
using Amazon.Bedrock.Model;

namespace Amazon.GenAI;

public static class Constants
{
    private const string Predefined = "LangChain.Providers.Amazon.Bedrock.Predefined";

    public static ChatModel GetChatModelTypeById(string modelId)
    {
        modelId = modelId.ToLower() ?? throw new ArgumentNullException(nameof(modelId));
            
        var typeNameSpace = GetTypeNameSpaceById(modelId);

        var type = Assembly.GetAssembly(typeof(BedrockProvider))!.GetType(typeNameSpace);
        type = type ?? throw new ArgumentNullException(nameof(type));

        var provider = new BedrockProvider();
        object[] args = { provider };
        var model = (ChatModel)Activator.CreateInstance(type, args)!;

        return model;
    }

    public static ITextToImageModel GetImageModelTypeById(string modelId)
    {
        modelId = modelId.ToLower() ?? throw new ArgumentNullException(nameof(modelId));

        var typeNameSpace = GetTypeNameSpaceById(modelId);

        var type = Assembly.GetAssembly(typeof(BedrockProvider))!.GetType(typeNameSpace);
        type = type ?? throw new ArgumentNullException(nameof(type));

        var provider = new BedrockProvider();
        object[] args = { provider };
        var model = (ITextToImageModel)Activator.CreateInstance(type, args)!;

        return model;
    }

    private static string GetTypeNameSpaceById(string modelId)
    {
        return modelId switch
        {
            "ai21.j2-mid-v1" => $"{Predefined}.Ai21Labs.Jurassic2MidModel",
            "ai21.j2-ultra-v1" => $"{Predefined}.Ai21Labs.Jurassic2UltraModel",

            "amazon.titan-text-express-v1" => $"{Predefined}.Amazon.TitanTextExpressV1Model",
            "amazon.titan-text-lite-v1" => $"{Predefined}.Amazon.TitanTextLiteV1Model",
            "amazon.titan-embed-text-v1" => $"{Predefined}.Amazon.TitanEmbedTextV1Model",
            "amazon.titan-embed-image-v1" => $"{Predefined}.Amazon.TitanEmbedImageV1Model",
            "amazon.titan-image-generator-v1" => $"{Predefined}.Amazon.TitanImageGeneratorV1Model",

            "anthropic.claude-instant-v1" => $"{Predefined}.Anthropic.ClaudeInstantV1Model",
            "anthropic.claude-v1" => $"{Predefined}.Anthropic.ClaudeV1Model",
            "anthropic.claude-v2" => $"{Predefined}.Anthropic.ClaudeV2Model",
            "anthropic.claude-v2:1" => $"{Predefined}.Anthropic.ClaudeV21Model",

            "cohere.command-text-v14" => $"{Predefined}.Cohere.CommandTextV14Model",
            "cohere.command-light-text-v14" => $"{Predefined}.Cohere.CommandLightTextV14Model",
            "cohere.embed-english-v3" => $"{Predefined}.Cohere.EmbedEnglishV3Model",
            "cohere.embed-multilingual-v3" => $"{Predefined}.Cohere.EmbedMultilingualV3Model",

            "meta.llama2-13b-chat-v1" => $"{Predefined}.Meta.Llama2With13BModel",
            "meta.llama2-70b-chat-v1" => $"{Predefined}.Meta.Llama2With70BModel",

            "stability.stable-diffusion-xl-v0" => $"{Predefined}.Stability.StableDiffusionExtraLargeV0Model",
            "stability.stable-diffusion-xl-v1" => $"{Predefined}.Stability.StableDiffusionExtraLargeV1Model",

            _ => ""
        };
    }

    public static IEnumerable<FoundationModelSummary>? ListValidModels(IOrderedEnumerable<FoundationModelSummary> allModels)
    {
        return allModels.Where(model => GetTypeNameSpaceById(model.ModelId).Equals("") == false).ToList();
    }
}