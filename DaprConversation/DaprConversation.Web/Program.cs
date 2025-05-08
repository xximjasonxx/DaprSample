using System.Text;
using Dapr.AI.Conversation;
using Dapr.AI.Conversation.Extensions;
using DaprConversation.Web;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDaprConversationClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/ask", async (DaprConversationClient conversationClient, [FromBody]AskQuestionRequestModel model) =>
    {
        var response = await conversationClient.ConverseAsync("azopenai-component",
            new List<DaprConversationInput>
            {  new(model.Question, DaprConversationRole.Generic) });

        var sb = new StringBuilder();
        foreach (var resp in response.Outputs)
        {
            sb.AppendLine(resp.Result);
        }
        
        return Results.Ok(sb.ToString());
    })
.WithName("AskQuestion")
.WithOpenApi();

app.Run();

