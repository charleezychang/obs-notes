https://www.youtube.com/watch?v=e03EHZIVJtM

#### Using `try-catch` blocks
- only works for synchronous code
```ts
error: Error | null = null

try {
	this.service.method()
} catch (error) {
	if (error instantof Error) {
		this.error = error
		
	}
}
```
#### Handling Global Errors: Using `ErrorHandler` which the framework provides
- works for both async and sync code
- under the hood, ngZone (tracking async stuff for CD) handles this but runs outside angular `ngZone.runOutsideAngular(() => { })`,  thereby not triggering CD cycle when the error occurs from an external library or async operation, to handle this, run the `handleError` inside the zone
```ts
import { ErrorHandler, ngZone } from '@angular/core';

@Injectable()
export class CustomErrorHandler implements ErrorHandler {
	constructor(
		private zone: ngZone
		// perhaps httpClient if you want to record client side errors	
	) {
	}
	this.zone.run(() => {
		handleError(error: unknown) {
			// snackbar service or etc.
			console.warn('Error caught':, error)
		}
	})
}
```
Replace the default ErrorHandler by this CustomErrorHandler in ngModule or bootstrapApplication
```ts
{
	provide: ErrorHandler,
	useClass: CustomErrorHandler
}
```
Errors within try-catch blocks are not cascaded to the global error handler, to circumvent this, throw the error after catching (hot-potato)
```ts
catch (error) {
	if (error instantof Error) {
		this.error = error
		throw error
	}
}
```
#### Error Handling in RxJS Observables
- use `tap()` operator to extract the error if it is desired to be displayed
- use catch and replace strategy which utilizes `catchError` operator (returns an observable in place of the error), if desired to ignore error and replace with another observable
- can use both strategy at the same time
```ts
this.tasks$ = this.service.load().pipe(
	tap({
		error: (error) => this.error = error
	}),
	// without catchError, it will be intercepted by ErrorHandler
	catchError(err => of([]))
)
```
You can replace the error (to make the error message readable to users) in the service/observable source itself before it is consumed by using the hot-potato logic
```ts
load() {
	return this.http.get('my.url.com').pipe(
		catchError(err => {
			console.warn('Error in fetching', err);
			throw new Error('There was a problem fetching data...')
		})
	)
}
```
IMPORTANT: When error occurs in an observable, it will skip all downstream operators except for those that use/work with the error object. Think of it as 2 data flows: one for regular flow and one for error flow.
#### HTTP Error Handling
- use [[8. HTTP Client#Interceptors]] 