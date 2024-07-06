[Learn Angular Signals - The Future of State Management - YouTube](https://www.youtube.com/watch?v=RLoACfLYwPs)
- Without signals, Angular runs on life cycle / change detection
- v16+

```ts
// template
<input type="text" [value]="title()" (keyup)=changeTitle($event) />

// component
title = signal('') // initializing signals
users = signal<UserInterface[]>([]) // with type
titleChangeEffect = effect(() => { // side effects (not necessarily assigned to a variable but must be initialized in the constructor)
	console.log('title change effect', this.title())
})
usersTotal = computed(() => this.users().length) // based on other signals
changeTitle(event: Event) {
	const title = (event.target as HTMLInputElement).value
	this.title.set(title)
}
```

Signal Methods
- `set` - change value entirely
- `update` - take previous value and do something with it (immutable)
- `mutate` - take current value and mutate it (for arrays and objects)
- `computed` - produces another signal based from other signals (automatically detects dependencies)

[Signals Unleashed: The Full Guide - YouTube](https://www.youtube.com/watch?v=6W6gycuhiN0)
```ts
query: WritableSignal<string> = signal<string>('');
```

Two-Way Binding
```html
<input 
	[(ngModel)]="query()"	   
	/>

<input
   [ngModel]="queryBefore17point2()"
   (ngModelChange)="queryBefore17point2.set($event)"
	/>
```

## `effect()`
- not running in an injector context, so suggested to put in the ==constructor==
- how long does it live? Angular already handles its decomposition
- if it is ran on `ngOnInit()`, it will show a runtime error, because somebody needs to close this reactive context, and if we do not want to do it then Angular has to take care of it

### Glitch-Free Effect or Push/Pull Algorithm
- `effect()` is only called once in a group of cumulative signal methods (`set`, `update`, `computed`, etc.) after they have been all called (similar to change detection)
- Though, similar to C#'s `async` context, if there is a signal method before an `await` block, it will split the code into two parts: 1 before the `await` and 1 including and after `await`. So the `effect()` in this case will be triggered twice
```ts
constructor(
    effect(() => console.log(this.holidays()))
)

ngOnInit(): void {
    this.search();
}

async search() {
    this.holidays.set([]); // First log call
    const holidays = await this.holidaysService.find(this.query(), this.type());
    this.holidays.set([{ ...createHoliday(), isFavorite: false}]);
    this.holidays.update((value) => [...value, ...value])
    this.holidays.set([]);
    this.holidays.set(holidays); // Second log call
}
```

### Dynamic Dependency Tracking
- If there is a signal consumed in a conditional inside an `effect()`, it will automatically 'unsubscribe' the signal if the condition doesn't hold true anymore therefore even if it is a dependency of the effect, it will not trigger the effect function. 
- It will automatically 'subscribe' again if the conditional that is holding the signal becomes true
- Does not affect signals which are part of the conditional itself
```ts
constructor(
	// called every time query changes
	// not called if city is changed but query is not 'Vienna'
	// called if city is changed but query is 'Vienna'
    effect(() => {
	    console.log("query changed")
	    if (this.query() == "Vienna") {
		    console.log(this.city())
	    }
    })
)
```

### `effect()` updating Signals
- Writing signals inside an `effect()` might cause an infinite loop/circular dependency but in some cases, it is inevitable
- Angular will throw an error for such but developer can turn this off via an `option` object: `effect(() => {}, { allowSignalWrites: true })`
- Interesting note: since signals are immutable, a signal dependency will not trigger an effect if its value is the same (provided that it is a primitive type) which could overcome an infinite loop
```ts
constructor(
    effect(() => {
		this.type().set('city')
    }, { allowSignalWrites: true })

    effect(() => {
	    // won't run if previous value is same as new value
	    // so its not going to trigger an infinite loop
	    console.log(this.type())
    })
)
```

### `untracked()`
- When we call a function inside `effect()` that utilizes signals, this these signals will become a dependency of that `effect()`
- By using this function, every function inside that contains signals will have the signals be dropped from the dependency of the `effect()`
```ts
// prettySearch() signal is dependent on city() and type signals()
printOutSearch() {
	console.log(this.prettySearch())
}

constructor() {
	effect(() => {
		// implicit dependency
		this.query();
	    // even if printOutSearch uses city() and type() signals
	    // it will not trigger this effect()
		untracked(() => {
			this.printOutSearch();
		})
	})
}
```

## RxJS Interoperability
### `takeUntilDestroyed()`
- Somewhat auto un-subscription but must have access to injection context, otherwise, you can use `destroyRef`
```ts
prettyDate = '';
// destroyRef = inject(DestroyRef)

constructor() {
	interval(1000)
		.pipe(tap(console.log), takeUntilDestroyed())
		// .pipe(tap(console.log), takeUntilDestroyed(this.destroyRef))
		.subscribe(() => (this.prettyDate = newDate().toLocaleTimeString()))
}
```

### `toSignal()`
- Converts observable to a signal
- Might initially be `undefined`, workaround is to add initial value as an option: `{ initialValue: initialValue }`
- Will behave as if its marked with `| async`
- Note: signals mark the component as dirty, but not the ancestors. However, even if the parent is not marked as dirty, the CD will go pass through it and go through its children
- Opposite is to use `toObservable(this.mySignal())`
```ts
prettyDate = toSignal(
	interval(1000).pipe(
		tap(console.log), 
		map(() => new Date.toLocaleTimeString())
	),
	{ initialValue: new Date().toLocaleTimeString() }
)
```