[Dependency injection fundamentals in C# - DI vs IoC vs DIP - YouTube](https://www.youtube.com/watch?v=M1jxLQu40qo)
#### Dependency Inversion Principle
- gigher ordered components should not depend on lower ordered components but instead rely on abstractions
- better testability

```c#
// Without inversion
var star = new GithubService().GetStars("repoName");

class GithubService
{
	public int GetStars(string repoName)
	{
		// Dependent on GithubClient
		return new GithubClient().GetRepo(repoName).Stars;
	}
}

class GithubClient
{
	public (string repoName, int Stars) GetRepo(string repoName)
	{
		return (repoName, repoName.Length)	
	}
}
```
#### Inversion of Control
- give control/responsibility to some container or framework
- providing _any kind_ of `callback`, which "implements" and/or controls reaction, instead of acting ourselves directly (in other words, inversion and/or redirecting control to the external handler/controller).
- ex. event handlers ([oop - What is Inversion of Control? - Stack Overflow](https://stackoverflow.com/questions/3058/what-is-inversion-of-control/3108#3108)), dependency injections
#### Dependency Injection
- more specific version of IoC pattern, and is all about removing dependencies from your code.
- separation of concerns ([oop - What is Inversion of Control? - Stack Overflow](https://stackoverflow.com/questions/3058/what-is-inversion-of-control/19007613#19007613))
- instead of creating the dependency, the framework creates it (IoC) when the method is called
- 3 types: ctor injection, setter injection, interface injection

```c#
// With Inversion
var star = new GithubService().GetStars("repoName");

class GithubClient
{
	public (string repoName, int Stars) GetRepo(string repoName)
	{
		return (repoName, repoName.Length)	
	}
}
```

```c#
// ctor injection
class GithubService(IGithubClient githubClient)
{
	private IGithubClient _githubClient = githubClient;

	public int GetStars(string repoName)
	{
		return _githubClient.GetRepo(repoName).Stars;
	}
}

class interface IGithubClient
{
	public (string repoName, int Stars) GetRepo(string repoName)
}
```

Instead of managing the dependency injections ourselves (ctor injection):
```c#
var client = new GithubClient()
var stars = new GithubService(client).GetStars("repoName");
```

We can give the responsibility to some IoC container: `Microsoft.Extensions.DependencyInjection`
```c#
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
// Mapping the interface and the actual object
serviceCollection.AddTransient<IGithubClient, GithubClient>;
serviceCollection.AddTransient<GithubService>;

// This now controls the creation of the injections
var serviceProvider = serviceCollection.BuildServiceProvider();

GithubService githubService = serviceProvider.GetRequiredService<GithubService>();

githubService.GetStars("repoName");
```

## Dependency Injection in an Actual Application
[Dependency Injection Deep Dive | .NET & C# Essentials (youtube.com)](https://www.youtube.com/watch?v=yjUCKSKCQxg)
See `Dependency Injection` Folder for code sample

`AddTransient`
- Created every time the service is accessed
- Disposed at the end of the request
- Usage: During one request you use `httpClient` service to call other web api request multiple times, but the web api endpoint is different. At that time you will register the httpclient service as transient. That means each time when you call the `httpClient` service it will create a new `httpClient` to send the request not used the same one

`AddSingleton`
- Created first time they're requested 
- Persists throughout the lifetime of the application
- Usage: You will have an image store service you could have a service to load images from a given location and keeps them in memory for future use

`AddScoped`
- Created once per client request (connection) but different across different requests
- Disposed at the end of the request
- Usage: We will use this for sql connection. It means it will create and dispose the SQL connection per request

| ![[Pasted image 20240326161052.png\|500]] |
| ----------------------------------------- |
| ![[Pasted image 20240326161148.png\|500]] |


|           | Transient | Scoped | Singleton |
| :-------: | :-------: | :----: | :-------: |
| Transient |     +     |   +    |     +     |
|  Scoped   |     -     |   +    |     +     |
| Singleton |     -     |   -    |     +     |
#### Relationship Between Service Lifetimes
- A long lifetime service cannot request a shorter lifetime service, because during its lifetime, several short-term services may change, and it would not be correct to randomly select any of them.

#### Multiple Registrations
- Using a single service with different implementations

`KeyedTransient`, `AddKeyedScoped`, `AddKeyedSingleton`
- Only for Version 8+

```c#
builder.Services.AddKeyedTransient<IMyInterface, ImplementationServiceOne>("one");
builder.Services.AddKeyedTransient<IMyInterface, ImplementationServiceTwo>("two");
```

```c#
public class NumbersController: ControllerBase
{
	private INumbersClient _numbersOneService;
	private INumbersClient _numbersTwoService;
	
	public class NumbersController(
		[FromKeyedServices("one")] INumbersClient numbersOneService,
		[FromKeyedServices("two")] INumbersClient numbersTwoService)
	{
		_numbersOneService = numbersOneService,
		_numbersTwoService = numbersTwoService,
	}
}
```

Alternative if using `dotnet` of below Version 8

```c#
builder.Services.AddTransient<IMyInterface, ServiceOne>("one");
builder.Services.AddTransient<IMyInterface, ServiceTwo>("two");
```

```c#
public class NumbersController: ControllerBase
{
	private INumbersClient _numbersOneService;
	private INumbersClient _numbersTwoService;
	
	public class NumbersController(
		IEnumberable<INumbersClient> numbersServices)
	{
		_numbersOneService = numbersServices.Single(x => x is ServiceOne),
		_numbersTwoService = numbersServices.Single(x => x is ServiceTwo),
	}
}

