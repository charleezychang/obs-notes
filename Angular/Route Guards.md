[Angular Guards | Angular Auth Guard | Angular Canactivate (youtube.com)](https://www.youtube.com/watch?v=6vZSMVxnnTE)

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
