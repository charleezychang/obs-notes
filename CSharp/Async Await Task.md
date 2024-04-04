------------------

## Task
always use await in i/o bound processes

returned Tasks should always be awaited so it doesnt block async code, if async is not used, the 

if returning of a function is `Task<int>` then the return line of code is only int type
awaiting a last line of a task function is redundant, just return the task

avoid using async void, why?? because you cannot await a void method (only tasks), so if the method is modifying a collection and the parent thats calling this method also modifies this collection, they will modify it at the same time = bug, another thing is if the void method has an exception, and its called in a try catch block in the caller, it will be skipped since the main thread already had gone through the trycatch block

if you are 100% SURE that you dont want to await a Task (or planned to return void), maybe want to run in the background, use MyAsyncMethod.SafeFireAndForget()

use async all the way

always use suffix Async for method names

`Task.Run(() => {})` makes a method async, same as calling a async function that returns a task

always await every task!!

never call `.Wait()`!!
## CancellationToken

## ConfigureAwait

.ConfigureAwait(false) = allows the main thread to continue even if there await (basically await pauses the main thread), *default is true*

**If we set 'ConfigureAwait(true)' then the continuation task runs on the same thread used before the 'await' statement. If we set 'ConfigureAwait(false)' then the continuation task runs on the available thread pool thread** -  continuation task (lines after await statement)

If the developer asynchronously waits for the method ( await DoSomethingAsync() ) then everything is well and good as there won't be a blocking thread

[c# - Usage of ConfigureAwait in .NET - Stack Overflow](https://stackoverflow.com/questions/62681749/usage-of-configureawait-in-net)
ConfigureAwait(true): Runs the rest of the code on the same context the code before the await was run on.
ConfigureAwait(false): Tells it that it does not need the context, so the code can be run _anywhere_. It could be any thread that runs it.

WHY NOT TO USE ConfigureAwait(false): [.NET: Don't use ConfigureAwait(false) | Gabe's Code (gabescode.com)](https://www.gabescode.com/dotnet/2022/02/04/dont-use-configureawait.html)

