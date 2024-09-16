- design pattern and mechanism for creating and delivering some parts of an application to other parts of an application that require them
- two roles: consumer and provider, angular facilitates between the interaction of the two via an abstraction called `Injector`
- `Injector` checks its registry if there is already an instance of the dependency, otherwise, a new instance is created and stored in the registry
- Angular creates an application-wide injector (also known as the "root" injector) during the application bootstrap process
#### Providing and consuming a dependency
- a dependency must have the `@Injectable` decorator and then must be made available in the DI by providing it
- ==Providing it at the root level using `providedIn`==: creates a shared instance and able to inject in any class that asks for it. Enables Angular and JavaScript code to remove services that are unused called *tree-shaking*
```ts
@Injectable({
  providedIn: 'root'
})
class HeroService {}
```
- ==Providing it at the component level==: a new instance is created with each new instance of the component. This type will causes the dependency to be always included in the application - even when dependency is unused
```ts
@Component({
  standalone: true,
  selector: 'hero-list',
  template: '...',
  providers: [HeroService]
})
class HeroListComponent {}
```
- ==Providing it at the application root level==: This type will causes the dependency to be always included in the application - even when dependency is unused
```ts
export const appConfig: ApplicationConfig = {
    providers: [
      { provide: HeroService },
    ]
};

// main.ts
bootstrapApplication(AppComponent, appConfig)
```
- ==Providing in modules: `providers` field of `@NgModule`==. This type will causes the dependency to be always included in the application - even when dependency is unused
##### Consuming Dependency
```ts
@Component({ … })
class HeroListComponent {
  constructor(private service: HeroService) {}
}
```

```ts
@Component({ … })
class HeroListComponent {
  private service = inject(HeroService);
}
```
#### Creating an injectable service
- components should handle view/template/user experience features
- services encapsulate business logic, features, and sharing of data across the application keeping the code modular and reusable
- Injection Context - runtime context where the current injector is available aka during constructor