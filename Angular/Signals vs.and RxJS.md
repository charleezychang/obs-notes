[Angular Signals vs RxJS - Reuse It Effectively - YouTube](https://www.youtube.com/watch?v=Qv6pV-A14gw)

```ts
private users$ = new BehaviorSubject<UserInterface[]>([]);

getUsers(): Observable<UserInterface[]> {
	return this.users$.asObservable();
}

addUser(user: UserInterface): void {
	const updatedUsers = [...this.users$.getValue(), user];
	this.users$.next(updatedUsers);
} 

removeUser(userId: string): void {
	const updatedUsers = this.users$
		.getValue()
		.filter((user) => user.id !== userId);
	this.users$.next(updatedUsers);
}
```

```ts
private usersSig = signal<UserInterface[]>([]);

getUsers(): Signal<UserInterface[]> {
	return computed(this.usersSig); // read-only (comapared to this.usersSig())
}

addUser(user: UserInterface): void {
	this.usersSig.update(users => [...users, user])
} 

removeUser(userId: string): void {
	const updatedUsers = this.usersSig().filter((user) => user.id !== userId);
	this.usersSig.set(updatedUsers);
}
```