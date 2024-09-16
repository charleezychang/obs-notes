[Change Detection in Angular - You Project Is 20x Slower! (youtube.com)](https://www.youtube.com/watch?v=-tB-QDrPmuI)
- method in which the framework checks the state of the application and apply necessary changes to the DOM
- Angular change detection is synchronous
- Checks twice in development mode - angular makes sure nothing changes (if there are changes, error: `ExpressionChangedAfterItHasBeenCheckedError`). This error doesn't appear in production since the check only happens once.

Triggers CD cycle for the whole component tree (except those with `OnPush`) automatically
1. User interactions: clicks, input changes
2. Asynchronous operations: HTTP Requests, setTimeout, setInterval, promises
3. Life cycle hooks: ngOnChanges, ngDoCheck, ngAfterViewChecked
4. Zone.js: intercepts asynchronous operations and trigger CD automatically
5. Manual trigger: `detectChanges()`

`ChangeDetectionStrategy.OnPush`
Component is excluded from the usual change detection cycle unless specific cases are met:
1. Input change - changed from the parent component, important to note that objects and arrays are compared by reference! To circumvent this, create a new object/array via spread operator
2. Event change - changed in the component itself, (click), (keyup), etc.
3. Stream change - for `| aync`, even though observables are objects in nature, change detection is triggered 

[Change Detection in Angular - Pt.1 View Checking (youtube.com)](https://www.youtube.com/watch?v=hZOauXaO8Z8)
[Change Detection in Angular Pt.2 - The Role of ZoneJS (2023) (youtube.com)](https://www.youtube.com/watch?v=Ys7xdebd66Y)
[Change Detection in Angular Pt.3 - OnPush Change Detection Strategy (youtube.com)](https://www.youtube.com/watch?v=WAu7omIoerM)
Change detection happens in two processes:
1. View checking - synchronize the component view (html) with the data model (component.ts)
2. Rerun Process 1 - automatically re-execute process 1 when state is changed

Process 2 can be disabled, but have to manually detect changes `this.cdr.detectChanges()`, in which angular goes through the view and update the data bindings, including children components! (if using default strategy)
```ts
// main.ts
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
// { ngZone: 'noop' } is the option to disabled automatic view checking
platformBrowserDynamic().bootstrapModule(AppModule, { ngZone: 'noop' })
    .catch(err => console.error(err))
```
If the data binding is coming from a function, it will re-execute the function (so avoid heavy computation). If data is using pipes, then it will re-execute that pipe also

Zone.js
- “monkey-patches” the browser’s native APIs for asynchronous tasks, allowing it to detect when these tasks start and finish. When an asynchronous operation completes, Zone.js triggers Angular’s change detection to ensure the view is updated accordingly12.
```ts
const originalConsoleLog = console.log;
console.log = function(...args) {
	originalConsoleLog(...args);
	console.warn(`This will trigger change detection`)
}
```

`this.cdr.detectChanges()`
- runs the CD cycle for the current component and children component immediately
`this.cdr.markForCheck()`
- behind the scene, calls `markViewDirty()`
- does not trigger change detection, just marks the component as dirty for the next change detection cycle
- important to note that all of the component's ancestors are marked since some might have OnPush method and and not reach the component in discussion

Component is marked automatically as dirty (calls `markViewDirty()`) for these cases:
1. Component state is changed via event handler , ==marks ancestors as dirty==
2. Value of any component input in changing, ==marks child component as dirty==- compares by reference, so mutated objects always has the same reference so in Angular's POV, it has not changed
3. When you use use `| async`, ==marks ancestors as dirty==

[Angular's Change Detection (youtube.com)](https://www.youtube.com/watch?v=0PJPZ3rLqrY)
Zone.js implements the default strategy for change detection
Functions in the template do not trigger CD, instead its triggered by:
1. DOM Event
2. Specific Async Task (for example, fetch() doesn't count)

`ChangeDetectionStrategy.OnPush `
- change detection cycle stops on this component and will only run it through if it is marked as dirty
- Criteria for "Dirty Marking"
	- manually via `ChangeDetectorRef.markForCheck()`
	- handled DOM event (with event listener)
	- async Pipe
	- property binding (`@Input`)
	- signal (Angular 16+)

Common encounters with `ChangeDetectionStrategy.OnPush`
1. If you feed data into a Material UI component (or any component libraries for that matter), it will be marked as dirty, since this marking happens upwards, all ancestors are marked dirty as well
2. (11 min.) If a parent component, with `OnPush`, has a DOM event which updates a property of a child of `Default` strategy, you may encounter bugs since this triggers the CD cycle twice for the child (first is the DOM event, second one is property binding change of which may not yet be triggered). Example of this is a click event that updates an input of a child (which may have delay).  To solve this, use `ngOnChanges()` lifecycle hook so that the change in property is handled separately.
   Best way is to change the child component's change detection strategy to `OnPush` also. This way, the DOM event from the parent component does not trigger the CD cycle onto the child.
3. Mutable changes to objects and arrays do not trigger CD. Use spread operator.
4. `ChangeDetectorRef.detectChanges()` runs CD cycle for the component and its children. It does not go through the parent's nor ancestor's which are still handled by the default change detection which is triggered by Zone.js
5. `ChangeDetectorRef.markForCheck()` marks component and its ancestors as dirty for the next CD cycle. Commonly paired with the `OnPush` strategy. Case example, if a child component triggers a CD, a parent with `OnPush` strategy will not be re-rendered without being marked as dirty.
6. DOM events with event listeners will trigger dirty marking even if the function for that listener is empty. `(click)="update()"` and `update()` function is empty
7. Signals mark the component as dirty, but not the ancestors. However, even if the parent is not marked as dirty, the CD will go pass through/skip it and go through its children

[Angular Performance: Your App at the Speed of Light - Christian Liebel | NG-DE 2019 (youtube.com)](https://www.youtube.com/watch?v=moUCZoJfhwY)
Keep CD cycle < 16ms
Profiling
```ts
// main.ts
platformBrowserDynamic().bootstrapModule(AppModule).then(mobule => 
enableDebugTools(module.injector.get(ApplicationRef).components[0]))
```

```ts
// browser command line api, measure the duration of a change detection run (500ms or 5 change detection cycles)
Execute ng.profiler.timeChangeDetection()
```

Component Tree Visualization
[Understand Angular Change Detection (jeanmeche.github.io)](https://jeanmeche.github.io/angular-change-detection/)

