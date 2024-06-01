[Change Detection in Angular - You Project Is 20x Slower! (youtube.com)](https://www.youtube.com/watch?v=-tB-QDrPmuI)
Angular change detection is synchronous
Checks twice in development mode - angular makes sure nothing changes (if there are changes, error: `ExpressionChangedAfterItHasBeenCheckedError`). This error doesn't appear in production since the check only happens once.

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

Zone.js - implements monkey patching 
```ts
const originalConsoleLog = console.log;
console.log = function(...args) {
	originalConsoleLog(...args);
	console.warn(`This will trigger change detection`)
}
```

`this.cdr.markForCheck()`
- behind the scene, calls `markViewDirty()`
- does not trigger change detection, just marks the component as dirty for the next change detection cycle
- important to note that all of the component's ancestors are marked since some might have OnPush method and and not reach the component in discussion

Component is marked automatically as dirty (calls `markViewDirty()`) for these cases:
1. Component state is changed via event handler , ==marks ancestors as dirty==
2. Value of any component input in changing, ==marks child component as dirty==- compares by reference, so mutated objects always has the same reference so in Angular's POV, it has not changed
3. When you use use `| async`, ==marks ancestors as dirty==

[Angular's Change Detection (youtube.com)](https://www.youtube.com/watch?v=0PJPZ3rLqrY)
[Angular zoneless change detection (youtube.com)](https://www.youtube.com/watch?v=R2wjayCaw30)
