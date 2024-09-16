#### Class and Style Binding
##### Single class binding
- `[class.sale]="onSale"`
##### Multiple class binding
- `[class]="classExpression"` , `classExpression: string = "myclass-1 myclass-2"`
- `[class]="classExpression"` , `classExpression: Record<string, boolean> = {'myclass1': true, 'myclass2': false}`
- `[class]="classExpression"` , `classExpression: string[] = ["myclass-1", "myclass-2"]`
##### Single style binding
- `<nav [style.background-color]="expression"></nav>`
- `<nav [style.backgroundColor]="expression"></nav>`
- `[style.width.px]="width"`, `width = 100`
##### Multiple style binding
- `[style]="styleExpression"`, `styleExpression: string = "width: 100px; height: 100px"`
- `[style]="styleExpression"`, `styleExpression: Record<string, any> = {width: '100px', height: '100px', backgroundColor: 'cornflowerblue'}`
#### Event listeners
```html
<!-- Matches alt and left shift -->
<input type="text" (keydown.code.alt.leftshift)="updateField($event)" />
```

```ts
@Component({
  template: `
    <input type="text" (keyup)="updateField($event)" />
  `,
  ...
})
export class AppComponent {
  updateField(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      console.log('The user pressed enter in the text field.');
    }
  }
}
```
#### Two-way binding
Changes in the component class reflect in the template, and user interactions in the template update the component class.
```ts
export class MyComponent {
  username: string = '';
}
```

```html
<input [(ngModel)]="username" />
<p>Hello, {{ username }}!</p>
```
- When the user types in the input field, the `username` property is updated.
- The paragraph below the input field displays the current value of `username`, which updates as the user types.
##### Two-way binding between components
- Child component must contain `@Input` property, `@Output` event emitter that has exact same name as input property PLUS "Change" at the end (should also have the same type as input), and a method that emits to the event emitter with the updated value of the `@Input()`.
```ts
// ./app.component.ts
import { Component } from '@angular/core';
import { CounterComponent } from './counter/counter.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CounterComponent],
  template: `
    <main>
      <h1>Counter: {{ initialCount }}</h1>
      <app-counter [(count)]="initialCount"></app-counter>
    </main>
  `,
})
export class AppComponent {
  initialCount = 18;
}
```

```ts
// './counter/counter.component.ts';
import { Component, EventEmitter, Input, Output } from '@angular/core';
@Component({
  selector: 'app-counter',
  standalone: true,
  template: `
    <button (click)="updateCount(-1)">-</button>
    <span>{{ count }}</span>
    <button (click)="updateCount(+1)">+</button>
  `,
})
export class CounterComponent {
  @Input() count: number;
  @Output() countChange = new EventEmitter<number>();
  updateCount(amount: number): void {
    this.count += amount;
    this.countChange.emit(this.count);
  }
}
```
#### Control flow
```html
@if (a > b) {
  {{a}} is greater than {{b}}
} @else if (b > a) {
  {{a}} is less than {{b}}
} @else {
  {{a}} is equal to {{b}}
}
```

```html
@for (item of items; track item.id) {
  {{ item.name }}
} @empty { 
  <li> There are no items.</li>
}
```
- For collections that do not undergo modifications (no items are moved, added, or deleted), using `track $index` is an efficient strategy.
- Other contextual variables for `@for`: `count` `$index` `$first` `$last` `$even` `$odd`

```html
@switch (condition) {
  @case (caseA) {
    Case A.
  }
  @case (caseB) {
    Case B.
  }
  @default {
    Default case.
  }
}
```
- **`@switch` does not have fallthrough**, so you do not need an equivalent to a `break` or `return` statement.
#### Pipes 
- operator that allows you to transform declaratively in the template (uses vertical bar `|` )
- you can combined multiple pipes: `{{ schedule | date | uppercase }}`
- passing parameters to pipes: `{{ schedule | date:'hh:mm':'UTC' }}`
- binary operators are grouped/concatenated together before pipe is applied
- ternary operators are not concatenated together therefore must use grouping via parenthesis if pipe is desired to apply to the result of the operator
- pure pipes, which only detects primitive type changes or object reference, offer a performance advantage because Angular can avoid calling the transformation function if the passed value has not changed
##### Built-in Pipes
- AsyncPipe, CurrencyPipe, DatePipe, DecimalPipe, JsonPipe, KeyValuePipe, LowerCasePipe, UpperCasePipe, PercentPipe, SlicePipe, TitleCasePipe
- most of these pipes transform into a format according to locale rules
##### Creating custom pipes
- use `@Pipe` decorator which accepts metadata of `name` (used in the template that uses this pipe operator), `standalone`, and `pure` (when you want a pipe to detect changes within arrays or objects)
- must have a method named `transform` (class must implement `PipeTransform`) that performs the transformation
- metadata `name` should be in camel case, while class should be in pascal case with `Pipe` suffix
```ts
import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
  name: 'myCustomTransformation',
  standalone: true
})
export class MyCustomTransformationPipe implements PipeTransform {
  transform(value: string, format: string): string {
    let msg = `My custom transformation of ${value}.`
    if (format === 'uppercase') {
      return msg.toUpperCase()
    else {
      return msg
    }
  }
}
```
#### `ng-template` and `ng-container`
- `ng-template` is not rendered on the page unless the reference for it is rendered
- more for grouping sections to be rendered
##### Referencing a template fragment
```html
<p>This is a normal element</p>
<ng-template #myFragment>
  <p>This is a template fragment</p>
</ng-template>
```

```ts
@Component({
  /* ... */,
  template: `
    <p>This is a normal element</p>
    <ng-template>
      <p>This is a template fragment</p>
    </ng-template>
  `,
})
export class ComponentWithFragment {
  @ViewChild(TemplateRef) myFragment: TemplateRef<unknown> | undefined;
}
```
- if your template has exactly one template fragment, you can query directly for the `TemplateRef` object with a `@ViewChild` query
```ts
@Component({
  /* ... */,
  template: `
    <p>This is a normal element</p>
    <ng-template #fragmentOne>
      <p>This is one template fragment</p>
    </ng-template>
    <ng-template #fragmentTwo>
      <p>This is another template fragment</p>
    </ng-template>
  `,
})
export class ComponentWithFragment {
  // When querying by name, you can use the `read` option to specify that you want to get the
  // TemplateRef object associated with the element.
  @ViewChild('fragmentOne', {read: TemplateRef}) fragmentOne: TemplateRef<unknown> | undefined;
  @ViewChild('fragmentTwo', {read: TemplateRef}) fragmentTwo: TemplateRef<unknown> | undefined;
}
```
##### Rendering a template fragment and passing parameter
```html
<p>This is a normal element</p>
<ng-template #myFragment>
  <p>This is a fragment</p>
</ng-template>
<ng-container [ngTemplateOutlet]="myFragment" />
```

```html
<ng-template #myFragment let-pizzaTopping="topping">
  <p>You selected: {{pizzaTopping}}</p>
</ng-template>
<ng-container
  [ngTemplateOutlet]="myFragment"
  [ngTemplateOutletContext]="{topping: 'onion'}"
  <!--- alternatively --->
  *ngTemplateOutlet="myFragment"; context: {topping: 'onion'}
/>
```
##### `ng-container`
- does not render a real element in the DOM
- useful for applying structural directives and ignores all attributes bindings and event listeners
- other use is to render components and template fragments via `[ngComponentOutlet]` and `[ngTemplateOutlet]`
#### Local template variables via `@let`
```html
@let user = user$ | async;
@if (user) {
  <h1>Hello, {{user.name}}</h1>
  <user-avatar [photo]="user.photo"/>
  <ul>
    @for (snack of user.favoriteSnacks; track snack.id) {
      <li>{{snack.name}}</li>
    }
  </ul>
  <button (click)="update(user)">Update profile</button>
}
```
- cannot be re-assigned within the template, however its value will be recomputed when Angular runs change detection.
```html
@let topLevel = value;

<div>
  @let insideDiv = value;
</div>

@if (condition) {
  {{topLevel + insideDiv}} <!-- Valid -->
  @let nested = value;
  @if (condition) {
    {{topLevel + insideDiv + nested}} <!-- Valid -->
  }
}

{{nested}} <!-- Error, not hoisted from @if -->
```
- declarations are scoped to the current view and its descendants. Since they are not hoisted, they **cannot** be accessed by parent views or siblings
