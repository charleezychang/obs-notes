Official Documentation: [TypeScript: JavaScript With Syntax For Types. (typescriptlang.org)](https://www.typescriptlang.org/)
### Current Issues
[TS Reset - Official Docs | Total TypeScript](https://www.totaltypescript.com/ts-reset)
### `as const` and extract as its own type
```ts
const routes = {
  home: "/",
  admin: "/admin",
  users: "/users",
} as const;

// would throw an error because you cannot re-assign objects casted as const
routes.admin = "/whatever";

const goToRoute = (route: "/" | "/admin" | "/users") => {};

goToRoute(routes.admin);
```
Extracting object as a type
```ts
const routes = {
  home: "/",
  admin: "/admin",
  users: "/users",
  newUser: "/users/new",
} as const;

type Route = (typeof routes)[keyof typeof routes];
const goToRoute = (route: Route) => {};

goToRoute(routes.admin);
```
### `any` vs. `unknown` 
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

### Discriminating Unions
[This Is One Of My Favorite TypeScript Features - YouTube](https://www.youtube.com/watch?v=xsfdypZCLQ8&ab_channel=WebDevSimplified)

Useful to intentionally avoid null checks
```tsx
// Instead of
type LocationState = {
	state: "Loading" | "Success" | "Error"
	coords?: { lat: number, lon: number }
	error? : { message: string }
}

function printLocation(location: LocationState) {
	switch (location.state) {
		case "Loading":
			console.log(location.state)
			break
		case "Success":
			// location.coords might be undefined
			console.log(location.cooords.lat, location.coords.lon)
			break
		case "Error":
			// location.error might be undefined
			console.log(location.error.message)
			break
	}
}

// refactor to this
type LoadingLocationState = {
	state: "Loading"
}

type SuccessLocationState = {
	state: "Success"
	coords: { lat: number, lon: number }
}

type ErrorLocationState = {
	state: "Error"
	error: { message: string }
}

type LoadingState = LoadingLocationState | SuccessLocationState | ErrorLocationState
```


Another example [How Did I Not Know This TypeScript Trick Earlier??! - YouTube](https://www.youtube.com/watch?v=9i38FPugxB8&ab_channel=Joshtriedcoding)
```tsx
type Person = { name: string } 
type Male = Person & { gender: 'male' salary: number }
type Female = Person & { gender: 'male' weight: number } 
type Props = Male | Female
```

### Generics
In some cases, a function can received any type and return the same type, instead of using *any*, you can use generics
```tsx
// Problem: 
function convertToArray(input: string | number | someObject): string[] | number[] | someObject[] {
	return [input]
}

// T here is just the generic name, you can use any name
function convertToArray<T>(input: T): T[] {
	return [input]
}
// Arrow function
const convertToArray = <T>(input: T): T[] {
	return [input]
}
```

Example 2
```tsx
// all generic types that will be used should be specified in the angled brackets
// this will also tell TypeScript that the function uses generic types
function getIndexOfArrayItem<T>(array: T[], arrayItem: T) {
	return array.findIndex((item) => item === arrayItem)
}

// Arrow function
const getIndexOfArrayitem = <T>(array: T[], arrayItem: T) => {
	return array.findItem((item) => item === arrayItem)
}
```

Example 3
```tsx
function createArrayPair<T, K>(input1: T, input2: K): [T, K] {
	return [input1, input2]
}
```

Example 4
```tsx
function Theme() {
	const [selectedTheme, setSelectedTheme] = useState("light");
	const themeOptions = ["light", "dark", "system"]

	return 
		<ThemeOptions 
			themeOptions={themeOptions} 
			selectedTheme={selectedTheme}    
			onThemeClick={(theme) => setSelectedTheme(theme)}
		/>
}

type ThemeOptionsProps<T extends React.ReactNode> = {
	themeOptions: T[];
	selectedTheme: T;
	onThemeClick: (theme: T) => void;
}

function ThemeOptions<T>({ themeOptions, selectedTheme }: ThemeOptionsProps<T>){
	return(
		themeOptions.map((theme, index) => {
			<li key={index}>
				<button
					onClick={() => onThemeClick(theme)}
				>
					{ theme } // Need to extend React.ReactNode
				</button> 
			</li>
		})
	)
}
```
### Assertion Functions
```js
import { createPost } from "./createPost";

export class SDK {
    constructor(public loggedInUserId?: string) {}

    createPost(title: string) {
        this.assertUserIsLoggedIn();
	    // TypeScript will still complain that this.loggedInUserId can be underfined
	    // Even though it is handled in assertUserIsLoggedIn() function
        createPost(this.loggedInUserId, title);
    }

    assertUserIsLoggedIn(): asserts this is this & {
        loggedInUserId: string;
    } {
        if (!this.loggedInUserId) {
            throw new Error("User is not logged in");
        }
    }
}
```