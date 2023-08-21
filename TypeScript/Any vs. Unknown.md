Much like `any`, any value is assignable to` unknown`; however, unlike any, you ==cannot access any properties== on values with the type `unknown`, nor can you call/construct them.

This is useful for APIs that want to signal “this can be any value, so you must ==perform some type of checking== before you use it”. This forces users to safely introspect returned values.

```ts
// utils
const getErrorMessage = (error: unknown): string => {
	let message: string;

	if (error instanceof Error) {
		message = error.message;
	}
	else if (error && typeof error === "object" && "message" in error) {
		message = String(error.message);
	}
	else if (typeof error === "string") {
		message = error
	}
	else {
		message: "Something went wrong."
	}

	return message;
}
```

```ts
const getMovies = () => {
	try {
		fetch("http://getmovies.com")
		.then(res => res.json())
		.then(data => console.log(data))
	}
	catch (error) {
		return {
			message: getErrorMessage(error)
		}
	}
}
```
