### Basic routing
- The order of routes is important because the `Router` uses a first-match wins strategy when matching routes, so more specific routes should be placed above less specific routes.
-  Use the `../` notation to go up a level. Use `./` or no leading slash to specify the current level.
```ts
const routes: Routes = [
  { path: 'first-component', component: FirstComponent },
  { path: 'second-component', component: SecondComponent },
  // dynamic parameter
  { path: 'pages/:pageId', component: PagesComponent },
  // redirect to `first-component`
  { path: '',   redirectTo: '/first-component', pathMatch: 'full' },
  // redirect function with access to dynamic parameter
  { path: 'old-pages/:pageId',
	redirectTo: route => {
		return '/pages/${route.params['pageId']}'
	}
  }
  // wildcard route for a 404 page
  { path: '**', component: PageNotFoundComponent },  
];
```
##### Highlighting active link
- requires imports `RouterLink`, and `RouterLinkActive`
- does NOT compare by full match by default, meaning if there's children routes in the same template, it would highlight both parent and children
```html
<h1>Angular Router App</h1>
<nav>
  <ul>
    <li>
	    <a routerLink="/first-component" routerLinkActive="active">
		    First Component
		</a>
	</li>
	<li>
	    <a routerLink="/first-component" routerLinkActive="active"
	    [routerLinkActiveOptions]={ exact: true }>
		    First Component
		</a>
	</li>
    <li><a routerLink="../second-component">Relative Route to second component</a></li>
  </ul>
</nav>
<!-- The routed views render in the <router-outlet>-->
<router-outlet></router-outlet>
```
##### Programmatical Navigation
```ts
export class Component {
	router = inject(Router)
	goToPage(): void {
		this.router.navigateByUrl('/products')
	}

	goToPageV2(): void {
		// products/1
		this.router.navigate(['products', '1'])
	}
}
```
### Nesting and Page Title
```ts
const routes: Routes = [
  {
    path: 'first-component',
    title: 'First component',
    // requires a secondary <router-outlet> in the template
    component: FirstComponent,  
    children: [
      {
	    // child route path
        path: 'child-a',  
        title: resolvedChildATitle,
        // child route component that the router renders
        component: ChildAComponent,  
      },
      {
        path: 'child-b',
        title: 'child b',
        // another child route component that the router renders
        component: ChildBComponent,  
      },
    ],
  },
];
const resolvedChildATitle: ResolveFn<string> = () => Promise.resolve('child a');
```
### Accessing query parameters and fragments
```ts
hero$: Observable<Hero>;
constructor(
  private route: ActivatedRoute,
  private router: Router  ) {}
ngOnInit() {
  const heroId = this.route.snapshot.paramMap.get('id');
  this.hero$ = this.service.getHero(heroId);
}
```
##### With Signals
- requires provider `provideRouter(routes, withComponentInputBinding();`
```ts
// www.localhost.com/pages/1/&limt=5
export class PageComponent {
	// pageId is declared as dynamic parameter
	pageId = input.required<string>();
	limit = input.required<string>();
}
```

### Route Guards
```ts
const routes: Routes = [
	{
		path: 'private',
		component: PrivateComponent,
		canActivate: [AuthGuardService]
	}
]

@NgModule({
	imports: [RouterModule.forChild(routes)],
	declarations: [PrivateComponent],
	providers: [AuthGuardService]
})
```

```ts
// auth-guard.service.ts
@Injectable()
export class AuthGuardService implements CanActivate {
    constructor(
        private currentUserService: CurrentUserService,
        private router: Router;
    )

    canActivate(): Observable<boolean> {
		return this.currentUserService.currentUser$.pipe(
	        filter((currentUser) => currentUser != undefined),
	        map((currentUser) => {
	            if (!currentUser) {
	                this.router.navigateByUrl('/');
	                return false;
	            }
	            return true;
	        })
	    )
    }

    // sync version
    // canActivate(): boolean {
    //     // condition
    //     return false; 
    // }
}
```

[Angular Functional Guards - How to Use Functional Router Guards (youtube.com)](https://www.youtube.com/watch?v=Yc93IvrouxY)
Classes approach are deprecated v15.2+
```ts
const routes: Routes = [
	{
		path: 'private',
		component: PrivateComponent,
		canActivate: [authGuard]
	}
]

@NgModule({
	imports: [RouterModule.forChild(routes)],
	declarations: [PrivateComponent]
})
```

```ts
// auth-guard.ts
export const authGuard = () => {
	const currentUserService = inject(CurrentUserService)
	const router = inject(Router)
	return this.currentUserService.currentUser$.pipe(
		filter((currentUser) => currentUser != undefined),
		map((currentUser) => {
			if (!currentUser) {
				this.router.navigateByUrl('/');
				return false;
			}
			return true;
		})
	)
}
```
### Link parameters array
```html
<a [routerLink]="['/heroes']">Heroes</a>

<a [routerLink]="['/hero', hero.id]">
  <span class="badge">{{ hero.id }}</span>{{ hero.name }}
</a>

<a [routerLink]="['/crisis-center', { foo: 'foo' }]">Crisis Center</a>
```
### Route Resolvers
- make sure certain data are there before you can access a route
- typically data from API call
```ts
// data.resolver.ts
export const pageResolver: ResolveFn<Object> = (route, state) => {
	const pageId = route.paramMap.get('pageId');
	return of({
		pageId,
		name: 'Foo'
	})
}
// app.routes.ts
{
	path: 'page:/pageId',
	component: PageComponent,
	resolve: {
		page: pageResolver
	}
}
// page.component.ts
// requires provider `provideRouter(routes, withComponentInputBinding();`
export class PageComponent {
	page = input.required<{ pageId: stirng; name: string }();
}
```

#### Lazy loading
```ts
const routes: Routes = [
  {
    path: 'items',
    loadChildren: () => import('./items/items.module').then(m => m.ItemsModule)
  }
];
```