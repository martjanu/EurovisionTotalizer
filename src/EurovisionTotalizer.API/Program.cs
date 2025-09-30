using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.Domain.Persistence.Serializera;
using EurovisionTotalizer.Domain.Persistence.Serializers;
using EurovisionTotalizer.Domain.Calculators;

var builder = WebApplication.CreateBuilder(args);

// ----------------- MVC Setup -----------------
builder.Services.AddControllersWithViews();

// ----------------- Factories -----------------
builder.Services.AddScoped<IJsonSerializerFactory, JsonSerializerFactory>();
builder.Services.AddScoped<IJsonStorageFactory, JsonStorageFactory>();

// ----------------- Serializers -----------------
builder.Services.AddScoped<IJsonSerializer>(sp =>
{
    var serializerFactory = sp.GetRequiredService<IJsonSerializerFactory>();
    return serializerFactory.Create();
});

// ----------------- Repositories -----------------
builder.Services.AddScoped<IJsonStorageRepository<Participant>>(sp =>
{
    var repoFactory = sp.GetRequiredService<IJsonStorageFactory>();
    var serializer = sp.GetRequiredService<IJsonSerializer>();
    return repoFactory.Create<Participant>("wwwroot/data/participants.json", serializer);
});

builder.Services.AddScoped<IJsonStorageRepository<Country>>(sp =>
{
    var repoFactory = sp.GetRequiredService<IJsonStorageFactory>();
    var serializer = sp.GetRequiredService<IJsonSerializer>();
    return repoFactory.Create<Country>("wwwroot/data/countries.json", serializer);
});

// ----------------- Domain Services -----------------
builder.Services.AddScoped<IScoreCalculator, ScoreCalculator>();
builder.Services.AddScoped<IParticipantRanker, ParticipantRanker>();

// ----------------- Factory Pattern Example -----------------
// Jei įgyvendinsi scorer factory, registracija galėtų atrodyti taip:
// builder.Services.AddScoped<IPredictionScorerFactory, PredictionScorerFactory>();

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
