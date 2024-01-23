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
```jsx
// If I click on the icon or the count, that is the target
// currentTarget is the whole container itself aka the button
<button onClick={handleClick}>
	<TriangleIcon />
	<span>{count}</span>
</button
```
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
		// res only contains the meta-data (headers, config, etc.)
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
const sorted = [...(items || [])].sort((a, b) => {
	if (sortBy == 'packed') {
		return b.packed - a.packed
	}

	if (conditionB) {
		return a.packed - b.packed
	}

	return 0
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
### API Error Sources
```jsx
fetch("https://xxxxxxxx.com)
	  .then((response) => {
		  if(!response.ok) {
			  throw new Error()
		  }
		  return response.json()
	  })
	  .then((data) => {
		  // do something
	  })
	  .ctach(() => {
		  // 1. network error while fetching
		  // 2. theres a response but not 2xx
		  // 3. theres a response, it is 200, but not in json format
	  })
```

### Filter out duplicates in an array
```js
const unique = array.filter((value, index, array) => {
	return array.indexOf(value) === index
})

// Example
const companyList = [ 'ABC', 'XYZ', 'ABC'];

// Iteration 1 (company: 'ABC', index: 0): `array.indexOf('ABC')` returns 0 (first occurrence), so it's kept.
// Iteration 2 (company: 'XYZ', index: 1): `array.indexOf('XYZ')` returns 1 (first occurrence), so it's kept.
// Iteration 3 (company: 'ABC', index: 2): `array.indexOf('ABC')` returns 0 (not the first occurrence, already included), so it's filtered out.
```
### Prevent Event Bubbling (stopPropagation and preventDefault)
`e.stopPropagation()` prevents the event from bubbling up the DOM tree, `e.preventDefault()` prevents the browser from doing whatever default behavior is associated with the event - e.g. a click event on an `<a>` tag will make the browser redirect to the `href` of the link, but if you use `preventDefault`
```tsx
const handleUpvote = (e: React.MouseEvent<HTMLButtonElement,MouseEvent> => {
	setUpvoteCount((prev) => ++prev)
	e.stopPropagation()
})

return (
	<li onClick={() => setOpen(prev => !prev)>
		// the onClick event on the button will bubble upwards 
		// triggering the onClick event of the <li> tag also 
		<button> onClick={(e) => handleUpvote}></button>
	</li>
)
```
### Renaming destructured object
```jsx
const { jobItemsSliced: jobItems } = useJobItems(searchText)
return(
	<p>{jobItems}</p>
)
```
### Location (HTML DOM API)
[Location - Web APIs | MDN (mozilla.org)](https://developer.mozilla.org/en-US/docs/Web/API/Location)
```js
// location: https://developer.mozilla.org:8080/en-US/search?q=URL#search-results-close-container
const loc = document.location;
console.log(loc.href); // https://developer.mozilla.org:8080/en-US/search?q=URL#search-results-close-container
console.log(loc.protocol); // https:
console.log(loc.host); // developer.mozilla.org:8080
console.log(loc.hostname); // developer.mozilla.org
console.log(loc.port); // 8080
console.log(loc.pathname); // /en-US/search
console.log(loc.search); // ?q=URL
console.log(loc.hash); // #search-results-close-container
console.log(loc.origin); // https://developer.mozilla.org:8080

location.assign("http://another.site"); // load another page
```

### Converting string to a number
[How to Convert a String to a Number in JavaScript (freecodecamp.org)](https://www.freecodecamp.org/news/how-to-convert-a-string-to-a-number-in-javascript/)

### Default parameter in functions
```js
function useDebounce(value, delay = 500) {
	// do something
}

useDebounce(value)
useDebounce(value, 1000)
```