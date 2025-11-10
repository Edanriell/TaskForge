using Scalar.AspNetCore;
using TaskForge.Api;
using TaskForge.Api.Extensions;
using TaskForge.Api.Infrastructure;
using TaskForge.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddApiServices()
    .AddCorsPolicy();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapScalarApiReference(options => { options.WithOpenApiRoutePattern("/swagger/1.0/swagger.json"); });

if (app.Environment.IsDevelopment())
    // app.MapOpenApi();
    app.ApplyMigrations();
// await app.SeedInitialDataAsync();
// app.UseCustomExceptionHandler();
// app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCors(CorsOptions.PolicyName);

app.MapControllers();

app.Run();

// GET  /api/projects
// GET  /api/projects/{id}
// POST /api/projects
// PUT  /api/projects/{id}
// DELETE /api/projects/{id}
// POST /api/projects/{id}/tasks
// PATCH /api/projects/{id}/tasks/{taskId}
// DELETE /api/projects/{id}/tasks/{taskId}
// Comment