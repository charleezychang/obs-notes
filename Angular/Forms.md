
|                     | Reactive                             | Template-driven                 |
| ------------------- | ------------------------------------ | ------------------------------- |
| Setup of form model | Explicit, created in component class | Implicit, created by directives |
| Data model          | Structured and immutable             | Unstructured and mutable        |
| Data flow           | Synchronous                          | Asynchronous                    |
| Form validation     | Functions                            | Directives                      |
#### Foundation Classes
- `FormControl` - tracks value and validation status of an individual form control
- `FormGroup` - tracks values and validation status of a collection of form controls
- `FormArray` - tracks values and validation status of an array of form controls
- `ControlValueAccessor` - creates a bridge between form control and DOM elements
## Reactive Forms
##### ==Basic Form Control==
```ts
import {Component} from '@angular/core';
import {FormControl} from '@angular/forms';
@Component({
  selector: 'app-reactive-favorite-color',
  template: `
    Favorite Color: <input type="text" [formControl]="favoriteColorControl">
  `,
})
export class FavoriteColorComponent {
  favoriteColorControl = new FormControl('');
}
```
###### Displaying a form control value
```html
<p>Value: {{ name.value }}</p>
```
###### Replacing a form control value
```ts
updateName() {
    this.name.setValue('Nancy');
}
```

```html
<button type="button" (click)="updateName()">Update Name</button>
```
##### ==Form Group and Submission==
```html
<form [formGroup]="profileForm" (ngSubmit)="onSubmit()">
  <label for="first-name">First Name: </label>
  <input id="first-name" type="text" formControlName="firstName">
  <label for="last-name">Last Name: </label>
  <input id="last-name" type="text" formControlName="lastName">
  <button type="submit" [disabled]="!profileForm.valid">Submit</button>
...
</form>
```

```ts
export class ProfileEditorComponent {
	profileForm = new FormGroup({
		firstName: new FormControl(''),
		lastName: new FormControl(''),
	});
  
	onSubmit() {
	    console.warn(this.profileForm.value);
	}
}
```
##### ==Nesting Form Groups==
```html
<form [formGroup]="profileForm">
  <label for="first-name">First Name: </label>
  <input id="first-name" type="text" formControlName="firstName">
  <label for="last-name">Last Name: </label>
  <input id="last-name" type="text" formControlName="lastName">
  <div formGroupName="address">
    <h2>Address</h2>
    <label for="street">Street: </label>
    <input id="street" type="text" formControlName="street">
    <label for="city">City: </label>
    <input id="city" type="text" formControlName="city">
    <label for="state">State: </label>
    <input id="state" type="text" formControlName="state">
    <label for="zip">Zip Code: </label>
    <input id="zip" type="text" formControlName="zip">
  </div>
</form>
```

```ts
export class ProfileEditorComponent {
  profileForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    address: new FormGroup({
      street: new FormControl(''),
      city: new FormControl(''),
      state: new FormControl(''),
      zip: new FormControl(''),
    }),
  });
}
```
 - `setValue()` - adheres to structure and replaces entire value for the control (best for model error catching)
 - `patchValue()` - replace an properties defined in the model
###### Form Builder
- way of reducing boiler plate and code repetition
```ts
import {Component} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {FormArray} from '@angular/forms';
@Component({
  selector: 'app-profile-editor',
  templateUrl: './profile-editor.component.html',
  styleUrls: ['./profile-editor.component.css'],
})
export class ProfileEditorComponent {
  profileForm = this.formBuilder.group({
    firstName: [''],
    lastName: [''],
    address: this.formBuilder.group({
      street: [''],
      city: [''],
      state: [''],
      zip: [''],
    }),
    aliases: this.formBuilder.array([this.formBuilder.control('')]),
  });
  
  constructor(private formBuilder: FormBuilder) {}
}
```
##### ==Form Array==
- manages any number of unnamed controls
```ts
profileForm = this.formBuilder.group({
    firstName: ['', Validators.required],
    lastName: [''],
    address: this.formBuilder.group({
      street: [''],
      city: [''],
      state: [''],
      zip: [''],
    }),
    aliases: this.formBuilder.array([this.formBuilder.control('')]),
});
```

```ts
get aliases() {
    return this.profileForm.get('aliases') as FormArray;
}

addAlias() {
    this.aliases.push(this.formBuilder.control(''));
}
```
Because the returned control is of the type `AbstractControl`, you need to provide an explicit type to access the method syntax for the form array instance (also `FormArray`, `FormControl`, `FormGroup`).

```html
<div formArrayName="aliases">
    <h2>Aliases</h2>
    <button type="button" (click)="addAlias()">+ Add another alias</button>
    <div *ngFor="let alias of aliases.controls; let i=index">
      <!-- The repeated alias template -->
      <label for="alias-{{ i }}">Alias:</label>
      <input id="alias-{{ i }}" type="text" [formControlName]="i">
    </div>
</div>
```
##### ==Typed Forms==
- controls' types are automatically inferred to `<inferred|null>`
###### Nullability
```ts
const email = new FormControl('angularrox@gmail.com', {nonNullable: true});
email.reset();
console.log(email.value); // angularrox@gmail.com
```
This will cause the control to reset to its initial value, instead of `null`
###### Explicit Type
```ts
const email = new FormControl<string|null>(null);
email.setValue('angularrox@gmail.com');
```
- On any `FormGroup`, it is possible to disable controls (via `disable()`). Any disabled control will not appear in the group's value.
- If you want to access the value _including_ disabled controls, and thus bypass possible `undefined` fields, you can use `login.getRawValue()`.
###### Optional Controls
```ts
interface LoginForm {
  email: FormControl<string>;
  password?: FormControl<string>;
}
const login = new FormGroup<LoginForm>({
  email: new FormControl('', {nonNullable: true}),
  password: new FormControl('', {nonNullable: true}),
});
login.removeControl('password');
```
- TypeScript will enforce that only optional controls can be added or removed.
###### Unknown Type (`FormRecord`)
```ts
const addresses = new FormRecord<FormControl<string|null>>({});
addresses.addControl('Andrew', new FormControl('2340 Folsom St'));
```

```ts
const addresses = fb.record({'Andrew': '2340 Folsom St'});
```
- If you need a `FormGroup` that is both dynamic (open-ended) and heterogeneous (the controls are different types), no improved type safety is possible, and you should use `UntypedFormGroup`.

## Template-driven forms
