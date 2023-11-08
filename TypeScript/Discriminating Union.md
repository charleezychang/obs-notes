[This Is One Of My Favorite TypeScript Features - YouTube](https://www.youtube.com/watch?v=xsfdypZCLQ8&ab_channel=WebDevSimplified)

Useful to avoid null checks
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