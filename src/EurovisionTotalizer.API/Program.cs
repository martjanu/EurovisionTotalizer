using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.Domain.Persistence.Serializers;
using EurovisionTotalizer.Domain.Calculators;
using EurovisionTotalizer.Domain.Scorers;
using EurovisionTotalizer.Domain.Persistence.Serializera;
using EurovisionTotalizer.Application.Services.Home;

var builder = WebApplication.CreateBuilder(args);

// ----------------- MVC Setup -----------------
builder.Services.AddControllersWithViews();

// ----------------- Factories -----------------
builder.Services.AddScoped<IJsonSerializerFactory, JsonSerializerFactory>();
builder.Services.AddScoped<IJsonStorageFactory, JsonStorageFactory>();
builder.Services.AddScoped<IScorerFactory, ScorerFactory>();

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

// ----------------- Scorers -----------------
builder.Services.AddScoped<IPredictionScorer<SemiFinalPrediction>>(sp =>
{
    var factory = sp.GetRequiredService<IScorerFactory>();
    return factory.CreateSemiFinalPredictionScorer();
});

builder.Services.AddScoped<IPredictionScorer<FinalPrediction>>(sp =>
{
    var factory = sp.GetRequiredService<IScorerFactory>();
    return factory.CreateFinalPredictionScorer();
});

// ----------------- Domain Services -----------------
builder.Services.AddScoped<IScoreCalculator, ScoreCalculator>();
builder.Services.AddScoped<IParticipantRanker, ParticipantRanker>();

// ----------------- Ranking strategy -----------------
builder.Services.AddScoped<IRankingStrategy, RankingStrategy>();
// ----------------- Application Services -----------------
builder.Services.AddScoped<IHomeService, HomeService>();

var app = builder.Build();

// ----------------- Error page configuration -----------------
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
