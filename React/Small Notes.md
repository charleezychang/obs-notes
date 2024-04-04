----------
```
npm create vite@<version> .
```

React TypeScript Cheat Sheet: https://react-typescript-cheatsheet.netlify.app/
### Logical `&&` Operator for Conditional Rendering
Be aware when using `&&` operator when the expression is `0` or `NaN`
```tsx
const [items, setItems] = useState([])

function Component() {
	// displays 0
	return items.length && <AnotherComponent />
}

// workarounds
function Component() {
	return items.length ? <AnotherComponent /> : null
}

function Component() {
	return items.length > 0 && <AnotherComponent />
}

function Component() {
	return !!items.length && <AnotherComponent />
}
```
### React Fragment
```tsx
// to enclose elements when they are returned
<>
	<p> My element </p>
</>

// use the full syntax when dealing with mapping so you can use the key attribute
// note: using map index as key is bad, there should be item.id
items.map((item, i) => {
	return (
		<React.Fragment key={i}>
			<p> Item </p>
		</React.Fragment>
	)
})
```
### useState previous state value
```tsx
const [count, setCount] = useState(0)

const handleClick = () => {
	// all 3 setCount's will get the initial state of 0
	// not inherently wrong, useful if only updating once
	setCount(count + 1)
	setCount(count + 1)
	setCount(count + 1)
}

const handleClick = () => {
	// setting an arrow function, we can pass in the previous state value as parameter
	setCount(prev => prev + 1)
}
```
### useState updating state object
```tsx
const [profile, setProfile] = useState({ name: '', age: '' })

const handleChange = (e) => {
	setProfile({
		// without the spread operator, setState will replace the entire object
		...profile,
		age: e.target.value
	})
}

const handleChange = (e) => {
	// previous value also works but pay attention that since its an arrow function
	// you need to wrap the object in parenthesis to immediately return it since
	// the braces will act as a function encloser without it
	setProfile(prev => ({
		...prev,
		age: e.target.value
	}))
}
```
### Handling forms with multiple input
```tsx
const [profile, setProfile] = useState({ name: '', age: '' })

const handleChange = (e) => {
	setProfile(prev => ({
		...prev,
		// automatically infers to the name attribute of whichever input element called
		// this function
		[e.target.name]: e.target.value
	}))
}

return (
	<>
		<input
			type="text"
			name="name"
		/>
		<input
			type="text"
			name="age"
		/>
	</>
)
```
### Redundant Hooks
```tsx
const PRICE = 5;
const [quantity, setQuantity] = useState(0)

// no need to make a useState and useEffect to update total price, since after the
// setState, it will rerender the component and subsequently update this
const totalPrice = quantity * PRICE;
// another use case is combining first name and last name

const handleShowPrice = () => {
	setQuantity(quantity + 1)
}

```
### useState: Primitive vs. Non-primitive
```tsx
// primitive types are passed by value, so when react tries to determine if the 
// new state is changed, same value primitives will not rerender the component
// additionally, primitives' types are inferred by TS while non-primitives are not
const [count, setCount] = useState(0)

const handleClick = () => {
	setCount(0) // will not rerender
}

// non-primitives like objects and arrays are passed by reference so they are not 
// equal when compared even with the same values
const [profile, setProfile] = useState({ name: '' })

const handleClick = () => {
	setProfile({ name: '' }) // will rerender
}

// this is especially dangerous when using useEffect dependency array
// it is better to pass primitive types (you can pass in contents of object)
useEffect(() => {
	// do something
}, [profile.name])
```


### useState with TypeScript
```tsx
type ProfileType = {
	name: string,
	age: number
}

const [profile, setProfile] = useState<ProfileType | null>(null)
```
### Passing only the setState
You can reference the state by using the parameter of setState
Though, consider making a function in the parent component that use this setState and pass this function to the child component instead
```tsx
function ParentComponent() {
	const [count, setCount] = useState(0)
	<ChildComponent setCount={setCount} />
}

function ChildComponent(props) {
	props.setCount(prev => prev + 1)
}
```

### Attach key listener to website using useEffect
```tsx
useEffect(() => {
	const handleKeydown = (event) => {
				if (event.code === "Space") {
			setCount(count + 1)
		}
	}

	window.addEventListener('keydown', handleKeydown)

	return () => {
		window.removeEventListener('keydown', handleKeydown)
	}
}, [count])

// if there is a focus, it will also count as a button clicked if spacebar is pressed
// remove it via:

const handleClick = (event) => {
	event.currentTarget.blur()
}
```
### Using children props to prop drill
When you have a component that serves as a style/container only, you can opt to not pass the props in it for its children to use
```tsx
export default function ComponentWithState({ children }) {
	const [state, setState] = useState(null)
	return (
	<ParentComponent>
		<ChildrenComponent state={state}/>
	</ParentComponent>
	)
}

export default functin ParentComponent({ children }) {
	return <div className="style">{ children }<div>
}
```
### Controlled vs. Uncontrolled inputs
Controlled input is where the value is stored in a state, vs. uncontrolled input where the value is just stored in the input tags. Main advantage of controlled input is you can now use the value elsewhere
```tsx
const [text, setText] = useState("")

return (
	<textarea
		value={text}
		onChange={(e) => {
			const newText = e.target.value;
			setText(newText)
		}}
		placeholder="Enter your text"
		spellCheck="false"
)
```
### htmlFor, affixing input and label
Since `for` is reserved for loops in JavaScript, the syntax for `for` in the context of labels and inputs is `htmlFor` in JSX. Additionally, you can make the `input` the child of `label`:
```jsx
<label>
	<input type="checkbox"/> The Label
</label>
```

### react-select
[react-select - npm (npmjs.com)](https://www.npmjs.com/package/react-select)

### Running useState's initial value only on first render
```tsx
const [items, setItems] = useState(() => {
	JSON.parse(localStorage.getItem("item") || initialItems)
})
```
### Alternative in Typing Elements
```tsx
import { useRef, ElementRef, useEffect } from "react",
// instead of hovering over the audio tag to determine its type
const Component = () => {
	const audioRef = useRef<HTMLAudioElement>(null);
	return <audio ref=(audioRef)>Hello</audio>;
}
// you can use ElementRef
const Component = () => {
	const audioRef = useRef<ElementRef<audio>>(null);
	return <audio ref=(audioRef)>Hello</audio>;
}
```
### Inserting components into specific parts of the DOM (portals)
Particularly useful for pop-ups to minimize the use of the z-index in CSS as it can be troublesome if the component is deeply drilled into the DOM tree.
```tsx
import { createPortal } from 'react-dom';  

function MyComponent() {  
	return (  
		<div style={{ border: '2px solid black' }}>  
			<p>This child is placed in the parent div.</p>  
			{createPortal(  
				<p>This child is placed in the document body.</p>,  
				document.body  
			)}  
		</div>  
	);  
}
```

### Repeating components via mapping empty array with length
```js
Array.from({ length: 6 }).map((_item, index) => {
	<SkeletonCard key={i} />
})
```
