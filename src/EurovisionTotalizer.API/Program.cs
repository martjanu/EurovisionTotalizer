using EurovisionTotalizer.Domain.Calculators;
using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Factories;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.Domain.Persistence.Serializera;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Singleton factories
builder.Services.AddSingleton<JsonStorageFactory>();
builder.Services.AddSingleton<JsonSerializerFactory>();

// Scoped serializer per request
builder.Services.AddScoped<IJsonSerializer>(sp =>
{
    var serializerFactory = sp.GetRequiredService<JsonSerializerFactory>();
    return serializerFactory.Create();
});

// Scoped repo: Participant
builder.Services.AddScoped<IJsonStorageRepository<Participant>>(sp =>
{
    var repoFactory = sp.GetRequiredService<JsonStorageFactory>();
    var serializer = sp.GetRequiredService<IJsonSerializer>();
    return repoFactory.Create<Participant>("wwwroot/data/participants.json", serializer);
});

// Scoped repo: Country
builder.Services.AddScoped<IJsonStorageRepository<Country>>(sp =>
{
    var repoFactory = sp.GetRequiredService<JsonStorageFactory>();
    var serializer = sp.GetRequiredService<IJsonSerializer>();
    return repoFactory.Create<Country>("wwwroot/data/countries.json", serializer);
});

// 🔹 Čia pridėk savo servisų implementacijas
builder.Services.AddScoped<IScoreCalculator, ScoreCalculator>();
builder.Services.AddScoped<IParticipantRanker, ParticipantRanker>();

var app = builder.Build();

// ----------------- Klaidų puslapio konfigūracija -----------------

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
