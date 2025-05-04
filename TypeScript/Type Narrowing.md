[Type-Safe TypeScript with Type Narrowing (youtube.com)](https://www.youtube.com/watch?v=MUJBT3Pb_Eg)
1. Equality `if (value !== null && value !== undefined)`
2. `typeof`
	1. `typeof value === 'object'` - needs more narrowing as `null` is considered an object (with no properties)
	2. `typeof value === 'string` - note that empty string is considered a falsy value thus will enter condition `!value`
3. `if (value)`
4. `instanceof` - used if class is used as type, but cannot use for 
5. Discriminated Unions - common technique for working with unions is to have a single field which uses literal types which you can use to let TypeScript narrow down the possible current type
   ```ts
type Person = {
   birthday: Date;
   type: 'Person'
}

type Car = {
	yearOfConstruction: Date;
	type: 'Car'
}

if (value.type === 'Car') {
	return diffInYears(value.yearOfConstruction)
} else {
	return diffInYears(value.birthday)
}
```
6. `in` - `('birthday' in value)`
7. Type Predicate - special function to work with Type to add logic, returns boolean so must be used in a conditional statement
```ts
type Person = {
   birthday: Date;
}

type PersonJson = {
	birthday: string; // perhaps from API
}

if (isPerson(value)) {
	return value
}

function isPerson(person: Person | PersonJson): person is Person {
	return person.birthday instanceof Date
}

// robust alternative
function isPerson(person: unknown): person is Person {
	return (
		typeof person === 'object' &&
		person !== null &&
		"birthday" in person &&
		(person as { birthday: unknown }).birthday instanceof Date
	)
}
```
8. Assertion Function - returns void and throws an error
```ts
function assertIsNotNullable<T>(value: T): asserts value is NonNullable<T> {
	if (value === undefined || value === null) {
		throw new Error("null or undefined not allowed")
	}
}
```