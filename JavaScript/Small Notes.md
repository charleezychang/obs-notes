---------
### Shorthand Object Property
If the property of an object has the same name as a variable being assigned to it, you can write it in short form
```js
let message = "My message";

const data = {
	message: message;
}

const data = {
	message;
}
```

-------
### String() vs. toString()
Key difference is that `String()` will return null and undefined as a string while `toString()` will throw an error
```js
var value = null;
alert(value.toString()); // TypeError

var value = null;
alert(String(value)); // null
```

### || vs ?? 
**Logical OR operator uses the right side value if the left side one is falsy**
Undefined or null does not mean false
```tsx
console.log(true || "shameel"); // true
console.log(false || "shameel"); // "shameel"

console.log(0 || "shameel"); // "shameel"
console.log(1 || "shameel"); // 1

console.log("uddin" || "shameel"); // "uddin"
console.log("" || "shameel"); // "shameel"

console.log(undefined || "shameel"); // "shameel"
console.log(null || "shameel"); // "shameel"
```

```tsx
console.log(true ?? "shameel"); // true
console.log(false ?? "shameel"); // false

console.log(0 ?? "shameel"); // 0
console.log(1 ?? "shameel"); // 1

console.log("uddin" ?? "shameel"); // "uddin"
console.log("" ?? "shameel"); // ""

console.log(undefined ?? "shameel"); // "shameel"
console.log(null ?? "shameel"); // "shameel"
```


### event.target vs event.currentTarget
[Do you know the difference between target vs. currentTarget? (youtube.com)](https://www.youtube.com/watch?v=F2pbD_Mr91Y&ab_channel=CodinginPublic)
target = what is specifically clicked on
currentTarget = where the eventListener is attached (commonly a container where there are more child elements)
### array.some(), array.find(), array.filter()
some() - atleast 1 condition is true, returns boolean
```js
numbers = [10, 100, 1000]
const result = numbers.some(function(number) {
	return number > 10000;
})
console.log(result) // false
```
find() - atleast 1 condition is true, returns first value that satisfies condition
```js
numbers = [10, 100, 1000]
const result = numbers.find(function(number) {
	return number > 1000;
})
console.log(result) // 1000
```
filter() - similar to find() but returns an array of all true cases
### async/await fetch
```js
const newUser = {
	name: 'John',
	job: 'Carpenter'
}

const clickHandler = async () => {
    try {
        const res = await fetch('https://regres.in/api/users', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)
        });

        const data = await res.json();

        if (res.ok) {
            console.log(data.description);
            return;

        }

        console.log(data);
    } catch (error) {
        console.log(error);
    }
}
```
### Sorting
The compare function should return a negative, zero, or positive value, depending on the arguments (note that true has a value of 1 and false has a value of -1)
If the result is negative, `a` is sorted before `b`.
If the result is positive, `b` is sorted before `a`.
If the result is 0, no changes are done with the sort order of the two values.
```tsx
const sorted = [...items].sort((a, b) => {
	if (sortBy == 'packed') {
		return b.packed - a.packed
	}

	if (conditionB) {
		return a.packed - b.packed
	}

	return
})
```
### How to tell if a calculation is expensive?
```js
console.time('filter array')
const visibleTodos = filterTodos(todos, tab);
console.timeEnd('filter array')

// log: filter array: 0.15ms
// if its more than 1ms, its considered expensive
```