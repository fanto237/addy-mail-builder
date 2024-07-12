using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


app.MapPost("/api/mailbuilder", ([FromBody] MailBuild entity) =>
    {
        var test = new BadRequestResult();
        // if (entity is null) return result;
        var toSend = entity.ToSend;
        var usedMail = entity.UsedMail;
        var toSendReformat = toSend.Replace('@', '=');
        var firstSub = usedMail.Substring(0, usedMail.IndexOf('@'));
        var secondSub = usedMail.Substring(usedMail.IndexOf('@'));

        var result = $"{firstSub}+{toSendReformat}{secondSub}";

        var response = new Response
        {
            Status = "success",
            Result = result
        };

        return response;
    })
    .WithOpenApi();
// app.Run(async (context) =>
// {
//     context.Response.Headers.Append("MyHeader", "MyHeaderValue");
//     await context.Response.WriteAsync("Hello World");
// });
app.Run();