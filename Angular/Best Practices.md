#### 01-01
- Consider limiting to 400 lines of code
#### 02-01
- Pattern: describe symbol's feature then its type, `feature.type.ts`
#### 02-02
- Use dashes to separate words in a descriptive name
- Use dots to separate name from the type
- Use conventional type names `service`, `component`, `pipe`, `module`, `directive`
#### 02-03
- Upper camel case for class names
- Append symbol name with conventional suffix `Component`, `Directive`, `Module`, `Pipe`, `Service`
#### 02-04
- Services with verb names could drop conventional suffix and use "-er". i.e. a service that logs messages could be `Logger` instead of `LoggerService`
#### 02-05
- Put bootstrapping and platform logic in a file named `main.ts`
- Include error handling in bootstrapping logic
- Avoid putting application logic in `main.ts`
#### 05-02
- Use dash/kebab-case for element selectors
#### 02-07
- Use prefix that identifies the feature area. i.e. if `users` is part of the `admin` feature, then use `admin-users`
#### 02-06
- Use lower camel case for naming selectors of directives
#### 02-08
- Spell non-element selectors in lower camel case unless the selector is meant to match a native HTML attribute. i.e. `[tohValidate]` instead of `[validate]`
#### 02-09
- Pipe class names should use upper camel case and corresponding name string should be lower camel case (cannot use hyphens)
#### 02-10
- Name test specification files the same as the component they test and with suffix `.spec`
#### 04-06
- Put all code in `src` folder
- Consider creating a folder for a component to accompany multiple files
#### 04-08
- Create a NgModule in the application's root folder
#### 04-09
- Create a NgModule for all distinct features in an application and place in the same named folder as the feature area
#### 04-10
- Create a feature module named `SharedModule` in a `shared` folder
- Consider NOT providing services in shared modules because they are singletons with the exception of services that are stateless (consumers aren't impacted by new instances)
- Import all modules required by the assets in the `SharedModule` such as `CommonModule` and `FormsModule`
#### 04-11
- Put contents of lazy loaded features in a lazy loaded folder which typically contains a routing component, child components, and their related assets
#### 05-03
- Consider giving components an element selector instead of attribute or class selectors
#### 05-12
- Use `@Input` and `@Output` decorators instead of `inputs` and `outputs` properties of `@Directive` and `@Component`
#### 05-13
- Avoid aliasing input and output
#### 05-15
- Limit logic in a component to only that required for the view, all other logic should be delegated to services
#### 05-16
- Name events (output) without the prefix `on`
- Name event handler methods with the prefix `on` followed by event name
#### 05-17
- Put presentation logic in the component class, and not in the template
#### 05-18
- Initialize inputs
#### 06-01
- Use attribute directives when you have a presentation logic without a template
#### 06-03
- **Consider** preferring the `@HostListener` and `@HostBinding` to the `host` property of the `@Directive` and `@Component` decorators.