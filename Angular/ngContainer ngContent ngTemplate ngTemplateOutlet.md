https://www.youtube.com/watch?v=Sd9aAVL3Uk8&t=1s

`ng-template`
- used together with structural directives (ngIf, ngFor, ngSwitch)
- it does not represent/render any html element
- most of the time, just use 2 `ngIf`'s instead of this
```html
<div *ngIf="article; else loading">{{ article.title }}</div>

<ng-template #loading>
	<div>Loading...</div>
</ng-template>

// under the hood
<ng-template [ngIf]="article" [ngIfElse]="loading">
	<div *ngIf="article; else loading">{{ article.title }}</div>
</ng-template>
```

`ngTemplateOutlet`
- similar to `ng-template` but no structural directive required
- can pass property
```html
<ng-container *ngTemplateOutlet="articleTemplate; context: { article }"></ng-container>

// important to have the let- to access the property
<ng-template #articleTemplate let-article="article">
	<div>{{ article.title }}</div>
</ng-template>
```

`ng-container`
- used for cases where multiple structural directives are used
- does not render any markup
```html
<ng-container *ngIf="numbers">
	<ng-container *ngFor="let number of numbers">{{ number }}</ng-container
</ng-container>
```

`ng-content`
- used for passing markup from parent to child component
```html
// parent
<app-child>
	<p>This will be passed into the child component</p>
</app-child>

// child
<ng-content><ng-content>
```