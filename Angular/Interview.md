https://www.youtube.com/watch?v=MSAiCicJWQA
How does angular works?
- angular.json -> main module, main.ts, index.html -> component tree
Advantage and Disadvantage
- advantage: full framework (routing, modular systems, streams), LTS (google), TypeScript
- disadvantage: learning curve
AOT vs JIT
- jit: development, compilation on runtime, transpile ts, angular templates to js
- aot: production, transpile before deploy, so size is much smaller and much faster
Dependency Injection
Lifecycle Hooks
- handles component during rendering
Observable vs Promises
- promises are only triggered once
Sharing data
- input and output (close in the tree), services, ViewChild/ContentChild, complex state management (RxJS)
Component vs. Directive
- component: widgets, has markup
- directive: behavior, no markup
Async pipe
- consume a stream in a template without subscription, automatically unsubscribes

https://www.youtube.com/watch?v=pLy2hm7_70o
Directives
- 2 types, attribute:  and structural: modify dom elements (components without templates)
Why structural directives have asterisk?
- shorthand, cause longer these directives are actually property bindings and to you need to use ng-template to use them
Why do we need trackBy for ngFor
- it creates a unique identifier for each element of an array so that the application doesn't need to re-render the whole array and only update the elements that changed
ViewEncapsulation
- describes the behavior of the components styles whether if we want them to affect other templates or not, creates unique class names on compilation
- ShadowDom - applied to component only, and not affected by global styles (legacy browsers unsupported)
- Emulated - default, angular adds attributes to the elements (ng-content) so the styles don't bleed into others
- None - styles are applied globally
ng-deep
- override css (global), breaking the convention that styles should be encapsulated
- use with :host to encapsulate within the component and down to children
Why shouldn't we use ApplicationRef.tick() to run CD cycle?
- it re-renders/run the cycle to the whole component tree
Lazy loading
- way to split up bundles and optimize initial loading time and only load necessary ones
Dependency Injection
Pure vs Impure Pipe
- pure: change in primitive type, pure functions, no side effects, changes in objects are ignored
- impure: can detect changes within objects
Is it good to call a method in a template? 
- no because it will run every CD cycle
Why is better to use http client instead of fetch?
- fetch is not monkey patched by Zone.js, omits observable, can attach headers
Optimization

https://www.youtube.com/watch?v=X-ptPktRzA0&list=PLoC8Q0moRTSjG53-besrUtWXeDpmkZRjL
String Interpolation vs. Property Binding
- former always transform into a string, so false will be "false", so functionality that depend on this boolean might not work properly
- careful in using `innerHTML` since user can inject html codes
Observable vs Promise
- Observable is not executed until subscribed to
- Observable can emit multiple values via next()
- Observable can be cancelled via unsubscribe()
- Observable support RxJS

https://www.youtube.com/watch?v=5A_YKlVWMPo
Define array of numbers and strings
- `let values: (string | number)[]`
What is type assertion
- forcing a type on a variable by using `as`
What is a void type
- commonly used when a function doesn't return anything
What is unknown
- should be used instead of `any` (will ignore type), maybe u call foo() but it doesnt have it
- forces you to check the type or type assertion
How to instantiate an object
- ` const newContent = <Content>({})`
Providing a default value in a function parameter
- `function something(isActive: boolean = true) {}` , TS will treat isActive as optional
What is tsconfig.json and what does this options do?
- configure TS compiler `compilerOptions, what ECMA version etc
What is elvis operator?
- basically null check `.?`, immediately stop running expressions when run into a null or undefined

https://www.youtube.com/watch?v=nS2_VVICnxo
Lazy loadiung
- allow you to load module or components on demand rather than all at once at startup to reduce initial load time
- 
[Angular Experienced Interview questions and answers | angular interview questions @UiDevGuideClips (youtube.com)](https://www.youtube.com/watch?v=8H_UgBCaEuA)
What are dumb components and smart components?

During constructor, inputs are undefined
