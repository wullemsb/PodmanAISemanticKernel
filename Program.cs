using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Kernel = Microsoft.SemanticKernel.Kernel;

// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

//var client = new HttpClient();
//var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:65527/v1/chat/completions");
//var content = new StringContent("{\n  \"messages\": [\n    {\n      \"content\": \"You are a helpful assistant.\",\n      \"role\": \"system\"\n    },\n    {\n      \"content\": \"What is the capital of France?\",\n      \"role\": \"user\"\n    }\n  ]\n}", null, "application/json");
//request.Content = content;
//var directResponse = await client.SendAsync(request);
//directResponse.EnsureSuccessStatusCode();
//Console.WriteLine(await directResponse.Content.ReadAsStringAsync());


var modelId = "doesntmatter";
// local Podman Desktop endpoint
var endpoint = new Uri("http://localhost:65527");

var kernelBuilder = Kernel.CreateBuilder();
#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var kernel = kernelBuilder
    .AddOpenAIChatCompletion(
        modelId,
        endpoint,
        apiKey: null)
    .Build();
#pragma warning restore SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

var chatService = kernel.GetRequiredService<IChatCompletionService>();

ChatHistory chat = new();
chat.AddSystemMessage("You are a helpful travel assistant.");

var executionSettings = new OpenAIPromptExecutionSettings
{
    MaxTokens = 1000,
    Temperature = 0.5,
    TopP = 1,
    FrequencyPenalty = 0,
    PresencePenalty = 0,
    StopSequences = new[] { "Human:", "AI:" },
};
var prompt = "Why should I visit Paris?";
var response = await chatService.GetChatMessageContentAsync(prompt, executionSettings);
Console.WriteLine(response.Content);



