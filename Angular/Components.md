- building blocks of the framework which usually house a class file, template, and a stylesheet
- Decorator: @Component
- Anatomy (a.k.a component metadata)
	- TypeScript class with behaviors to handle user input and data fetching
	- template / templateUrl: HTML Template 
	- selector: CSS Selector that defined how the component is used in HTML, case-sensitive, never use `ng-` prefix
	- styles / styleUrl
	- standalone
	- encapsulation
	- providers
	- inputs: accepts array of record input name and its alias (optional)
	- outputs: accepts array of record output name and its alias (optional)
	- host
	- changeDetection
	- preserveWhitespace
	- schemas
- Host element - element that houses or utilizes the component via its selector
- View - DOM rendered by the component
#### Style Scoping
1. ViewEncapsulation.Emulated
	- default, generates unique HTML attribute to be added in the elements in the template and CSS selectors
	- prevents leaking of styling to other components but still allow global styles to affect the component
	- supports `:host` and `::ng-deep` (turns into global styles, discouraged) 
2. ViewEncapsulation.ShadowDom
	- uses Shadow DOM API 
	- attaches shadow root to the component's host which guarantees only the component's styles apply to the elements of the template, global styles cannot penetrate this scope
3. ViewEncapsulation.None
	- behave like global style
#### Inputs and Outputs
- when extending a component class, inputs and outputs are inherited by the child class
- case-sensitive
##### Required Inputs
```ts
@Component({...})
export class CustomSlider {
	@Input({required: true}) value = 0;
}
```
##### Transform Inputs
- pure function, must not rely on state outside of the transform function
```ts
@Component({
  selector: 'custom-slider',
  ...
})
export class CustomSlider {
	@Input({transform: trimString}) label = '';
}
function trimString(value: string | undefined) {
	// must be pure function (like idempotent)
	return value?.trim() ?? '';
}
```

Built-in transform functions
1. `booleanAttribute` - presence of value indicates `true`, treats literal string `"false"` as `false`
2. `numberAttribute` - parse given value, returns `NaN` if fails
##### Input and Output Aliases (avoid)
```ts
@Component({...})
export class CustomSlider {
	// used like so: <custom-slider [sliderValue]="50" />
	@Input({alias: 'sliderValue'}) value = 0;
	// decorator accepts alias as default parameter
	@Input('sliderValue') value = 0
}
```

```ts
@Component({...})
export class CustomSlider {
  // template: <custom-slider (changed)="consumeEmitted($event)"
  @Output('valueChanged') changed = new EventEmitter<number>();

  emitToParentComponent(): {
	  changed.emit(10)
  }
}
```
#### Content projection & aliasing (`ngProjectAs`)
```html
<!-- Component template, selector: 'custom-card' -->
<div class="card-shadow">
  <ng-content select="card-title"></ng-content>
  <div class="card-divider"></div>
  <ng-content select="card-body"></ng-content>
  <!-- capture anything except "card-title" -->
  <ng-content></ng-content>
</div>
```

```html
<!-- Using the component -->
<custom-card>
  <card-title>Hello</card-title>
  <h3 ngProjectAs="card-body">Hello</h3>
  <img src="..." />
  <p>Welcome to the example</p>
</custom-card>
```

```html
<!-- Rendered DOM -->
<custom-card>
  <div class="card-shadow">
    <card-title>Hello</card-title>
    <div class="card-divider"></div>
    <h3>Hello</h3>
    <img src="..." />
    <p>Welcome to the example></p>
  </div>
</custom-card>
```

If a component does not include an `<ng-content>` placeholder without a `select` attribute, any elements that don't match one of the component's placeholders do not render into the DOM.

`ngProjectAs` supports only static values and cannot be bound to dynamic expressions.
#### Host elements
```ts
// Component source
@Component({
  selector: 'profile-photo',
  template: `
	<img src="profile-photo.jpg" alt="Your profile photo" />
  `,
  host: {
     'role': 'presentation',
     '[id]': 'id'
  }
})
export class ProfilePhoto {}
```

```html
<!-- Using the component -->
<h3>Your profile photo</h3>
<profile-photo role="group" [id]="otherId" />
<button>Upload a new profile photo</button>
```

```html
<!-- Rendered DOM -->
<h3>Your profile photo</h3>
<profile-photo role="group" [id]="otherId">
  <img src="profile-photo.jpg" alt="Your profile photo" />
</profile-photo>
<button>Upload a new profile photo</button>
```
##### Binding vs.`@HostBinding` and `@HostListener` decorators
```ts
@Component({
  ...,
  host: {
    'role': 'slider',
    '[attr.aria-valuenow]': 'value',
    '[tabIndex]': 'disabled ? -1 : 0',
    '(keydown)': 'updateValue($event)',
  },
})
export class CustomSlider {
  value: number = 0;
  disabled: boolean = false;
  updateValue(event: KeyboardEvent) { /* ... */ }
  /* ... */
}
```

```ts
@Component({
  /* ... */
})
export class CustomSlider {
  @HostBinding('attr.aria-valuenow')
  value: number = 0;
  @HostBinding('tabIndex')
  getTabIndex() {
    return this.disabled ? -1 : 0;
  }
  /* ... */
}
```

```ts
export class CustomSlider {
  @HostListener('keydown', ['$event'])
  updateValue(event: KeyboardEvent) {
    /* ... */
  }
}
```

**Always prefer using the `host` property over `@HostBinding` and `@HostListener`.** These decorators exist exclusively for backwards compatibility.
##### Binding collisions
```ts
@Component({
  ...,
  host: {
    'role': 'presentation',
    '[id]': 'id',
  }
})
export class ProfilePhoto { /* ... */ }
```

```ts
<profile-photo role="group" [id]="otherId" />
```

- If both values are static, the instance binding wins.
- If one value is static and the other dynamic, the dynamic value wins.
- If both values are dynamic, the component's host binding wins.
#### Lifecycle hooks

| Phase            | Method                  | Run Time                                            |
| ---------------- | ----------------------- | --------------------------------------------------- |
| Creation         | `constructor`           | after component instantiation                       |
| Change Detection | `ngOnInit`              | after initializing inputs                           |
|                  | `ngOnChanges`           | every time inputs have changed                      |
|                  | `ngDoCheck`             | every time component is checked for changes         |
|                  | `ngAfterViewInit`       | after view has been initialized                     |
|                  | `ngAfterContentInit`    | after content has been initialized                  |
|                  | `ngAfterViewChecked`    | every time view has been checked                    |
|                  | `ngAfterContentChecked` | every time content has been checked                 |
| Rendering        | `afterNextRender`       | after all components have been rendered to DOM      |
|                  | `afterRender`           | every time all components have been rendered to DOM |
| Destruction      | `ngOnDestroy`           | before component is destroyed                       |
`constructor`
- TypeScript feature
- used for declaring dependency injections
`ngOnInit`
- happens before own template is initialize
- update state/properties of component that are based on input values (since these are not yet instantiated in the constructor)
`ngOnChanges`
- happens before own template is checked
- the first `ngOnChanges` happens before `ngOnInit`, `firstChange` property will be true
- succeeding triggers happen when any input that is used in the template is changed, `firstChange` property will be false
- accepts `SimpleChanges` argument, a mapping of each input to a `SimpleChange` object which contains the input's previous value, current value, and a flag (`firstChange`) whether this is the first time the input has changes
- note that for cycles other than the first and for component multiple inputs, the parameter of type `SimpleChanges` will only contain the inputs that have changed
- objects and arrays are compared by reference so mutating the these does not re-trigger this hook
`ngDoCheck`
- happens before checking of template for changes, after `ngOnInit`
- triggered when change detection runs
- use this hook to manually check for state changes outside of normal change detection
- runs very frequently and can affect performance, avoid as possible
`ngAfterContentInit`
- happens after all children nested inside the component (content via `ng-content) have been initialized
- read results of content queries here
- point where you should avoid changing any state
- subsequent updates will be via `ngAfterContentChecked`
`ngAfterViewInit`
- happens after all children in the template/view have been initialized
- read results of view queries here
- point where you should avoid changing any state
 - subsequent updates will be via `ngAfterContentChecked`
`ngOnDestroy`
- happens when component is no longer shown on the page (i.e. hidden by `*ngIf` or navigation)

![[Pasted image 20240812234140.png]]

#### Referencing component children (queries)
- used to retrieve references to child components, directives, DOM elements
- if target element is hidden by `ngIf`, value is `undefined`
- query results become available in the `ngAfterViewInit` lifecycle, before this, value is `undefined`
- if `@ViewChild` is used to a component that appears multiple times in the template, will only reference the first instance of that component
- `@ViewChild` and `@ContentChild` accept `static` option as a second parameter, by setting this to `true`, you guarantee that the target of this query is ALWAYS present and is not conditionally rendered making it available earlier specifically in the `ngOnInit` lifecycle (not available for `@ViewChildren` and `@ContentChildren`)
- content queries find only _direct_ children of the component and do not traverse into descendants.
```ts
@Component({
  selector: 'custom-card-header',
  ...
})
export class CustomCardHeader {
  text: string;
}
@Component({
  selector: 'custom-card',
  template: `
	  <custom-card-header>Visit sunny California!</custom-card-header>
	  <custom-card-action>Save</custom-card-action>
	  <custom-card-action>Cancel</custom-card-action>
  `,
})
export class CustomCard {
  @ViewChild(CustomCardHeader, {static: true}) header: CustomCardHeader;
  @ViewChildren(CustomCardAction) actions: QueryList<CustomCardAction>;

  ngOnInit() {
     console.log(this.header.text);
  }
  
  ngAfterViewInit() {
    this.actions.forEach(action => {
	    console.log(action.text)
    })
  }
}
```

```ts
@Component({
  selector: 'custom-toggle',
  ...
})
export class CustomToggle {
  text: string;
}
@Component({
  selector: 'custom-expando',
  ...
})
export class CustomExpando {
  @ContentChild(CustomToggle) toggle: CustomToggle;
  ngAfterContentInit() {
    console.log(this.toggle.text);
  }
}
@Component({
  selector: 'user-profile',
  template: `
    <custom-expando>
      <custom-toggle>Show</custom-toggle>
    </custom-expando>
  `
})
export class UserProfile { }
```

Query Locators
- If more than one element defines the same template reference variable, the query retrieves the first matching element.
```ts
@Component({
  ...,
  template: `
    <button #save>Save</button>
    <button #cancel>Cancel</button>
  `
})
export class ActionBar {
  @ViewChild('save') saveButton: ElementRef<HTMLButtonElement>;
}
```
#### TODO: DOM APIs
#### Inheritance
```ts
@Component({
  selector: 'base-listbox',
  template: `
    ...
  `,
  host: {
    '(keydown)': 'handleKey($event)',
  },
})
export class ListboxBase {
  @Input() value: string;
  handleKey(event: KeyboardEvent) {
    /* ... */
  }
}
@Component({
  selector: 'custom-listbox',
  template: `
    ...
  `,
  host: {
    '(click)': 'focusActiveOption()',
  },
})
export class CustomListbox extends ListboxBase {
  @Input() disabled = false;
  focusActiveOption() {
    /* ... */
  }
}
```

`CustomListbox` inherits all the information associated with `ListboxBase`, overriding the selector and template with its own values. `CustomListbox` has two inputs (`value` and `disabled`) and two event listeners (`keydown` and `click`).

If a base class relies on dependency injection, the child class must explicitly pass these dependencies to `super`.
```ts
@Component({ ... })
export class ListboxBase {
  constructor(private element: ElementRef) { }
}
@Component({ ... })
export class CustomListbox extends ListboxBase {
  constructor(element: ElementRef) {
    super(element);
  }
}
```

If a base class defines a lifecycle method, such as `ngOnInit`, a child class that also implements `ngOnInit` _overrides_ the base class's implementation. If you want to preserve the base class's lifecycle method, explicitly call the method with `super`:
```ts
@Component({ ... })
export class ListboxBase {
  protected isInitialized = false;
  ngOnInit() {
    this.isInitialized = true;
  }
}
@Component({ ... })
export class CustomListbox extends ListboxBase {
  override ngOnInit() {
    super.ngOnInit();
    /* ... */
  }
}
```