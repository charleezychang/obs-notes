c----------
```
npm create vite@<version> .
```
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
### Custom hook with Context API (Template)
```tsx
import { createContext, useContext, useState } from "react";

type ThemeContextProviderProps = {
	children: React.ReactNode;
}

type theme = 'dark' | 'light'

type ThemeContext = {
	theme: theme;
	// Can hover setTheme to check type
	setTheme: React.Dispatch<React.SetStateAction<theme>>;
}
// if the context is used outside the provider, it will return null
const ThemeContext = createContext<ThemeContext | null>(null);

export default function ThemeContextProvider({
	children
}: ThemeContextProviderProps) {
	const [theme, setTheme] = useState<Theme>("light");

	return (
		<ThemeContext.Provider
			value={{
				theme,
				setTheme
			}}
		>
			{children}
		</ThemeContext.Provider> 	
	)
}
// Custom hook for Context API's solves 2 problems
// 1. The need to import the context in each component that uses it
// 2. The need to check if its null in each component that uses it
export function useThemeContext() {
	const context = useContext(ThemeContext);
	if (!context) {
		throw new Error("useThemeContext must be used within a ThemeContextProvider")
	}
	return context
}

// importing the custom hook
const { theme, setTheme } = useThemeContext()
```



### Passing only the setState
You can reference the state by using the parameter of setState
```tsx
function ParentComponent() {
	const [count, setCount] = useState(0)
	<ChildComponent setCount={setCount} />
}

function ChildComponent(props) {
	props.setCount(prev => prev + 1)
}
```

### Attach key listener to website
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