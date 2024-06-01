[I only ever use *these* RxJS operators to code reactively (youtube.com)](https://www.youtube.com/watch?v=Byttv3YpjQk)

1. map - do something with the observables, always returns the same number of observables. if condition is involved, might return undefined
2. filter - return observable if condition is true, can take out observables
3. tap - doesnt actually do anything. used for quick debugging within the stream or create a side effect
	- tap vs subscribe - tap is used within the class of the observable, otherwise subscribe if observable is accessed on another class
1. switchMap - switch to a new stream, passing in the values from the older stream. note that if the first stream emits another value, the current second stream will be overwritten
2. concatMap - similar to switchMap but will wait for the second stream to finish before taking in the next value for the first stream
	1. concatMap vs map - concatMap already "pseudo-subscribes" when it returns values
3. combineLatest - when you have multiple streams of observables and want to get the latest of each everytime
4. startWith - allow to start the stream with a specific value (basically append at the first index). useful together with valueChanges (since this only emits on change, so if you want an initial default value, this operator is the key)
5. debounceTime - allows to wait in milliseconds to emit the next value (everything in the waiting period is lost!)
6. distinctUntilChanged - emit value only if its changed, prevent duplicate values delivered consequently
7. distinct - emit value that has not been emitted before
8. catchError - error = lost stream. by using this operator, it will not break the stream
9. take - dictates how many values should be taken from the stream
10. takeLast - dictates how many values should be taken from the stream counting starting from the latest emitted value
11. skip - dictates how many values should be skipped from the stream
12. skipLast - dictates how many values should be skipped from the stream counting starting from the latest emitted value
13. concat - combines streams, but will wait for the previous stream to finish (basically stream 1 then stream 2, stream 3)
14. merge - combines streams, order of emission between streams are mixed, order of which is dependent on how fast the streams emit values
15. zip - takes 1 from each stream and then combines them into an array (best to destructure)


