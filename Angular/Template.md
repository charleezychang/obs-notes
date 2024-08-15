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
- `[style]="styleExpression"`, `styleExpression: string = {width: '100px', height: '100px', backgroundColor: 'cornflowerblue'}`
#### Binding to keyboard events
```html
<input (keydown.shift.t)="onKeydown($event)" />
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
