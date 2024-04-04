var builder = WebApplication.CreateBuilder(args);

{ // Empty Scope for clear separation of registration and request pipelines

    // When NumbersService is requested, this will tell the container what lifetime to use
    builder.Services.AddTransient<INumbersTransientService, NumbersTransientService>();
    builder.Services.AddSingleton<INumbersSingletonService, NumbersSingletonService>();

    // If theres a second registration of the same service, it will override the first one, unless you use TryAddTransient
    // builder.Services.AddSingleton<INumbersSingletonService, NumbersSingletonService>();

    // Optimistic way to register services (meaning if it the container still doesn't have this service, it will add it, otherwise it will ignore it)
    // builder.Services.TryAddSingleton<INumbersSingletonService, NumbersSingletonService>();

    // Alternative to builder.Services.AddSingleton<INumbersSingletonService, NumbersSingletonService>();
    // ServiceDescriptor serviceDescriptor = new(
    //     serviceType: typeof(INumbersSingletonService),
    //     implementationType: typeof(NumbersSingletonService),
    //     lifetime: ServiceLifetime.Singleton);
    // builder.Services.Add(serviceDescriptor);

    // Scan assembly and find all the controllers and register them into the dependency IoC contrainer
    builder.Services.AddControllers();
}

var app = builder.Build();
{ // Empty Scope for clear separation of registration and request pipelines

    // Middleware which looks at the route of the request and invokes the appropriate endpoint
    app.MapControllers();
    app.Run();
}

// Alternative way to register services and controllers

// ServiceCollection services = [];
// services.AddTransient<NumbersService>();
// services.AddTransient<NumbersController>();

// var serviceProvider = services.BuildServiceProvider();

// var controller = serviceProvider.GetRequiredService<NumbersController>();

// controller.GetNumber(); // 5