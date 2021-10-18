global using NBomber.Contracts;
global using NBomber.CSharp;
global using NBomber.Plugins.Http.CSharp;

var httpFactory = HttpClientFactory.Create();

var stepNet5 = Step.Create("net5-api", httpFactory, async context =>
{
    context.Client.BaseAddress = new Uri("https://localhost:5001");
    var response = await context.Client.GetAsync("api/devs", context.CancellationToken);

    return response.IsSuccessStatusCode
        ? Response.Ok(statusCode: (int)response.StatusCode)
        : Response.Fail(statusCode: (int)response.StatusCode);
});

var stepNet6 = Step.Create("net6-minimal-api", httpFactory, async context =>
{
    context.Client.BaseAddress = new Uri("https://localhost:7014");
    var response = await context.Client.GetAsync("api/devs", context.CancellationToken);

    return response.IsSuccessStatusCode
        ? Response.Ok(statusCode: (int)response.StatusCode)
        : Response.Fail(statusCode: (int)response.StatusCode);
});

var scenarioNet5 = ScenarioBuilder
    .CreateScenario("net5-api", stepNet5)
    .WithWarmUpDuration(TimeSpan.FromSeconds(5))
    .WithLoadSimulations(Simulation.KeepConstant(24, TimeSpan.FromSeconds(60)));

var scenarioNet6 = ScenarioBuilder
    .CreateScenario("net6-minimal-api", stepNet6)
    .WithWarmUpDuration(TimeSpan.FromSeconds(5))
    .WithLoadSimulations(Simulation.KeepConstant(24, TimeSpan.FromSeconds(60)));

NBomberRunner
    .RegisterScenarios(scenarioNet5, scenarioNet6)
    .Run();