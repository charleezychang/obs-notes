[I only ever use *these* RxJS operators to code reactively (youtube.com)](https://www.youtube.com/watch?v=Byttv3YpjQk)

1. map - do something with the observables, always returns the same number of observables. if condition is involved, might return undefined
2. filter - return observable if condition is true, can take out observables
3. tap - doesnt actually do anything. used for quick debugging within the stream or create a side effect
	- tap vs subscribe - tap is used within the class of the observable, otherwise subscribe if observable is accessed on another class
5. debounceTime - allows to wait in milliseconds to emit the next value (everything in the waiting period is lost!)
6. distinctUntilChanged - emit value only if its changed, prevent duplicate values delivered consequently
7. distinct - emit value that has not been emitted before
8. catchError - error = lost stream. by using this operator, it will not break the stream
9. take - dictates how many values should be taken from the stream
10. takeLast - dictates how many values should be taken from the stream counting starting from the latest emitted value
11. skip - dictates how many values should be skipped from the stream
12. skipLast - dictates how many values should be skipped from the stream counting starting from the latest emitted value
#### Observables Creation
- `from` - will emit and iterate through the items of an array
- `of` - will emit the whole array, but can act like `from` if the array is passed with spread operator
- `interval` - emits every given time period, first emission is after the time period
- `timer` - takes time to emit first emission as first parameter, and then interval time to emit succeeding emissions as second parameter
#### Combining observables
- `concat` - combine streams WRT to the order of streams, `of(1, 2, 3)` + `of(4, 5, 6)` = `of(1, 2, 3, 4, 5, 6)`
- `merge` - combine streams WRT to the order of emitted values ,`of(1, 2, 3)` + `of(4, 5, 6)` = `of(1, 4, 3, 2, 5, 6)`
- `combineLatest` - get latest values of multiple observables, each of which must emit one value before the operator emits the INITIAL value (tuple), after which it will emit every time a new value is emitted in any of the streams
- `withLatestFrom` - similar to `combineLatest` which requires all streams to at least emit one value for the first emission, but one stream is assigned as primary stream, and then succeeding emissions of the operator will be emitted based on the primary stream emission only
- `zip` - similar to `combineLatest`, but all emissions require all streams to emit one value before the operator emits an output
- `forkJoin` - similar to `zip`, but will only emit once all streams are completed (different from emission in which the former's streams are closed), meaning it only outputs once
- `race` - filters only the stream that first emitted a value, other streams are ignored
- `startWith` - emits an assigned initial value before emitting the rest, useful together with `valueChanges`
#### Dealing with higher ordered observables 
- when an value of an observable is used as a parameter in function that emits another observable, the return type is `Observable<Observable<T>>` (can possibly chain more inner observables) and will require a subscription chain `.subscribe(() => .subscribe())`
- this can be simplified by flattening each observable (essentially like subscribing) before they are consumed via: `concatMap`, `mergeMap`, `switchMap`, `exhaustMap`
```ts
const numbers = of(1, 2, 3);

const higherOrder = numbers.pipe(
  map(value => of(`Number: ${value}`))
);

higherOrder.subscribe(innerObservable => {
  innerObservable.subscribe(value => console.log(value));
});

const flattened = numbers.pipe(
  mergeMap(value => of(`Number: ${value}`)),
  mergeMap(value => of(`Transformed: ${value}`))
);

flattened.subscribe(value => console.log(value));
```
- `concatMap` - if we need to do things in sequence while waiting for completion
- `mergeMap` - for doing things in parallel
- `switchMap` - in case we need cancellation logic
- `exhaustMap` - for ignoring new Observables while the current one is still ongoing

[TOP 6 Mistakes in RxJS code - YouTube](https://www.youtube.com/watch?v=OhuRvfcw3Tw)
1. Nested subscriptions - use `switchMap`
2. `takeUntilDestroyed` / `takeUntil` - only handles subscriptions before the operator, place at the end of the chain
3. Manual subscriptions - not really avoidable in some situations, but much better to use `| async` or `toSignal` for modern versions
4. Cold observables - happens when there are multiple `| async` or subscriptions calling a single observable, use `shareReplay(1)` to fix (check below)
5. `distinctUntilChanged` - compares by reference so use a callback function with parameters previous and current value to compare properties of an object, or use `distinctUntilKeyChanged('propertyIWantToTrack')` if only tracking a single property
6. Use `tap()` for side effects (changing properties outside the scope of the function)

[RxJS: Hot vs Cold Observables - YouTube](https://www.youtube.com/watch?v=dWgpLoD1cCE)

| Cold Observable                            |                                                              Hot Observable |
| ------------------------------------------ | --------------------------------------------------------------------------: |
| Doesn't emit until subscribed to           |                                                   Emits without subscribers |
| Subscription activates the source          |                                              Creation activates the source, |
| Unicast (1 observable = 1 subscriber)      |                                        Multicast (1 subject = n subscriber) |
| `product$ = this.http.get<Product[]>(url)` | `productSubject = new Subject<number>();`<br>`this.productSubject.next(12)` |
`Subject` vs. `BehaviorSubject`
- `BehaviorSubject` always holds one value, when subscribed, it emits the value immediately, also must be created with an initial value
- `Subject` doesn't hold a value
![[Pasted image 20240621041631.png]]

[How to share your RxJS observables for improved performance (youtube.com)](https://www.youtube.com/watch?v=H542ZSyubrE)
Scenario: multiple calls on a function that fetches data and returns an observable, each observer will execute this fetch and therefore expensive
Workaround: save the observable in a variable and use: 
`share() vs shareReplay(1)`
- `.pipe(share())`
- method to transform a cold observable into a hot observable
- `share()` - uses `Subject` under the hood, so if a new subscription is called after emission of data, it will not be able to receive the initial data
- `shareReplay(1)` - uses `ReplaySubject` under the hood, parameter (`bufferSize`) is the number of emissions we want to emit prior to subscription (essentially like `BehaviorSubject` if 1)
- `shareReplay({ bufferSize: 1, refCount: true })` - unsubscribe to source observable if there are no more subscribers (cold), will subscribe again if there a new subscribers (hot again)
- Update: it is recommended to use `share({connector: new ReplaySubject(1)})` that behaves similar to `shareReplay({ bufferSize: 1, refCount: true })` as the latter will likely be deprecated like other multicasting operators