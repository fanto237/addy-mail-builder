using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddScoped<ILogger, Logger>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();


app.MapPost("/api/mailbuilder", static ([FromBody] MailBuild entity, ILogger<Program> logger, HttpContext ctx) =>
    {
        if (entity is null) return new Response() { Status = "error", Result = "Invalid request" };
        var toSend = entity.ToSend;
        var usedMail = entity.UsedMail;
        var toSendReformat = toSend.Replace('@', '=');
        var firstSub = usedMail[..usedMail.IndexOf('@')];
        var secondSub = usedMail[usedMail.IndexOf('@')..];

        var result = $"{firstSub}+{toSendReformat}{secondSub}";

        var response = new Response
        {
            Status = "success",
            Result = result
        };

        var remoteIpAddress = ctx.Connection.RemoteIpAddress?.ToString();
        logger.LogInformation("Request from {remoteIpAddress} processed and result is {result}", remoteIpAddress, result);

        return response;
    })
    .WithOpenApi();
app.Run();