- manages forms, lists, styles, and what users see
- 2 types: attribute and structural
- components are actually directives (they extend the `@Directive` decorator)
#### Built-in Attribute Directives
- `NgClass`, `NgStyle`, `NgModel`
#### Built-in Structural Directives
- only one structural directive per element, use `ng-container` to nest them
- you cannot really use them directly on elements (except for `[ngSwitch]`) but the framework has made some structural directives shorthand notation via `*` which behind the scenes binds the directive into a `ng-template` that wraps the element of concern
- `*ngIf` - conditionally render an element, value of `null` will not render the element, use `ng-container` if there is no element to host the directive
- `*ngFor` - iterate through an array and render elements to each item, use `trackBy` for optimization as it only re-renders items that changed
```html
<div *ngFor="let item of items; let i=index">{{ i + 1 }} - {{ item.name }}</div>
```

```ts
trackByItems(index: number, item: Item): number {
    return item.id;
}
```

```html
<div *ngFor="let item of items; trackBy: trackByItems">
    ({{ item.id }}) {{ item.name }}
</div>
```
- `[ngSwitch]` - is actually an attribute directive that changes the behavior of its companion directive, it acts as a container for `ngSwitchCase` and `ngSwitchDefault`, it can be applied directly to an element has it doesn't modify the DOM itself but only evaluates expressions
#### Attribute Directives
