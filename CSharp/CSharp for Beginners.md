[C# for Beginners - YouTube](https://www.youtube.com/playlist?list=PLdo4fOcmZ0oULFjxrOagaERVAMbmG20Xe)



VSCode Extension - C# Dev Kit

Use `decimal` instead of `float` or `double`

Strings are immutable, Lists are not
> Strings return a copy, lists modify the original

Counting List from the back
> But starts at index 1 instead of 0

Explicitly declaring a range
> `names[2..4]` - including 2 excluding 4

Adding items in arrays (only works in C# 12)
> `names = [..names, "Damian"]`

IEnumerable
> Doesn't execute the query immediately (deferred execution), only when you use the variable (usually `foreach`)
> Doesn't know the number of items, it just know to it needs to iterate over the items

Imperative vs. Declarative Programming
> explicitly mention step by step vs. say what do you want to happen

LINQ
> Common syntax to data query even in other querying language
> Also allows the IDE to highlight syntax (i.e. SQL is allowed but in string format, aka hard to see)\

Abstract class cannot be instantiated
Abstract methods are required to be overridden
Virtual methods can be optionally overridden

[GitHub - davidfowl/AspNetCoreDiagnosticScenarios: This repository has examples of broken patterns in ASP.NET Core applications](https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/tree/master)

> Contracts - responses and requests models

[C# Language Features vs. Target Frameworks | Dissecting the Code (sergeyteplyakov.github.io)](https://sergeyteplyakov.github.io/Blog/c%23/2024/03/06/CSharp_Language_Features_vs_Target_Frameworks.html)