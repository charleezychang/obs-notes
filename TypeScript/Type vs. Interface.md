Generally, `type` can do what `interface` can except the syntaxes are shorter and more readable. [Reference](https://www.youtube.com/watch?v=Idf0zh9f3qQ)
### Intersection vs. Extension
```ts
// Intersection
type UserProps = {
	name: string;
	age: number;
}

type AdminProps = UserProps & {
	role: string;
}

// Extension
interface UserProps {
	name: string;
	age: number;
}

interface AdminProps extends UserProps {
	role: string;
}
```
### Interface can only describe objects while type can describe everything
```ts
// Type
type Address = string;
const address: Address = "Blk 1 Lot 2"

// Interface
interface Address {
	address: string;
}
const address: Address {
	address: "Blk 1 Lot 2"
}
```
### Type alias can describe union types while interface cannot
```ts
// Type
type Address = string | string[]
const address1 = "Blk 1 Lot 2"
const address2 = ["Blk 1 Lot 2", "Blk 2 Lot 3"]
```
### Type alias' utilities are more readable than interface's
```ts
// Type
type UserProps = {
	name: string;
	age: number;
	createdAt: Date;
}
type GuestProps = Omit<UserProps, "name" | "age">

// Interface
interface GuestProps extends Omit<UserProps, "name" | "age"> {}
```
### Type alias can easily describe tuples
```ts
// type
type Address = [number, string];

// interface
interface Address extends Array<number | string> {
	0: number;
	1: string;
}

const address: Address = [12, "Other St."]
```
### Type alias can extract types from objects
```ts
const project = {
	title: "Project",
	specification: {
		areaSize: 100,
		rooms: 3
	}
}

type specification = typeof project["specification"]
```
### Type alias is closed while interface is open
```ts
// type
type User = {
	name: string;
}

// Will have duplicate error, use intersection instead
type User = {
	age: number;
}

// interface can be redeclared and merged
```



