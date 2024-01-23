### useRef
```tsx
export default function AddItemForm() {
	const inputRef = useRef();

	return (
		<form
			onSubmit={(e) => {
				e.preventDefault()
				if (!itemText) {
					alert("Item cannot be empty")
					inputRef.current.focus()
					return
				}
			}}
			<input
				value={itemText}
				onChange={(e) => {
					setItemText(e.target.value)
				}}
				autoFocus
		</form>
	)
}
```
#### `forwardRef`
 There is a bit of unorthodox way in passing `ref` as a prop into a child component. First, `ref` is a special prop. The child component must be an arrow function and `forwardRef` is wrapped around the render function (basically the original initial child component) which accepts parameters `props` and `ref`. Take note that in the render function, `props` comes first before `ref` but in typing `forwardRef`, `ref` is typed first prior to `props`
```tsx
import { forwardRef } from 'react';  

const MyInput = forwardRef<HTMLDivElement, PropsType>(function MyInput(props, ref) {  
	return (  
		<label>  
			{props.label}  
			<input ref={ref} />  
		</label>  
	);  
});
```
### useMemo
`useMemo(calculateValue, dependencies)`
Consider useMemo as pure optimization technique. Cache the result of a calculation between re-renders. Program should continue to work correctly even if you replace useMemo with a regular function call.
- Sorting
- Filtering
- Mapping
```tsx
import { useMemo } from 'react';  

function TodoList({ todos, tab }) {  
	const visibleTodos = useMemo(  
		() => filterTodos(todos, tab), [todos, tab]  
	);  
	// ...  
}
```

Another use case for useMemo is in context provider values
### useCallback
### useContext
- Generally file-named as nounContextProvider.jsx under a different folder, usually named as "Contexts". 
- Wrap the provider to the components that need it as children prop. Context also needs to import and return children. 
- Create the context outside the function by using `createContext()` and then consume it via useContext
- Consider using [[Zustand]]
```tsx
export const ItemsContext = createContext()

export default const ItemsContextProvider({ children }) {
	const [items, setItems] = useState()

	const handleAddItem = () => {}

	return (
		<ItemsContext.Provider value={{
			items,
			handleAddItem
		}}>
			{children}
		</ItemsContext.Provider>
	)
}

// consume context via destructuring
const {items, handleAdditem} = useContext(ItemsContext)
```
- To simplify and to avoid importing useContext everytime, make a [[#Custom hook with Context API (Template)]]

### useEffect
```jsx
useEffect(() => {
	const handleChangeHash = () => {
		console.log(window.location.hash.slice(1));
	}
	handleChangeHash()
	window.addEventListener("hashchange", handleChangeHash)
	// clean up function will run everytime the dependency changes
	return () => {
		window.removeEventListener("hashchange", handleChangeHash)
	}
}, [])
```

-------------------------
### useContext
Best used with [[#Custom hook with Context API (Template)|custom hooks]]:
### useReducer
- Similar to useState
- Copies state (immutability)

```tsx
import { useReducer } from "react"

type State = {
	count: number;
	error: string | nulll;
}

type Action = "increment" | "decrement"

function reducer (state: State, action: Action) {
	switch (action) {
		case "increment": {
			const newCount = stateCount + 1
			const hasError = newCount > 5
			return {
				...state,
				count: hasError? state.count : newCount,
				error: hasError? 'Maximum reached' : null
			}
		}
		case "increment": {
			const newCount = stateCount + 1
			const hasError = newCount < 0
			return {
				...state,
				count: hasError? state.count : newCount,
				error: hasError? 'Maximum reached' : null
			}
		}
		default:
			return state
	}
}

export default function Demo() {
	const [state, dispatch] = useReducer(reducer, {
		count: 0,
		error: null
	})

	return (<>
		<div>{ state.count }</div>
		{ state.error && <div>{ state.error }</div> }
		<button onClick=(() => dispatch('increment'))>Increment</button>
		<button onClick=(() => dispatch('decrement'))>Decrement</button>
	</>)
}
```
# Custom Pre-made Hooks

[useHooks – The React Hooks Library](https://usehooks.com/)
[usehooks-ts](https://usehooks-ts.com/)

---------------------------------
# Custom Hooks
Note: every time you attach a hook to a component, it will run an instance of that hook. This is especially costly when we have eventListeners. Consider using context API.
### Custom hook with Context API (Template)
- Since it uses the children props pattern, it does not re-render all children, but rather only the components that consume the context
- Consider using [[Zustand]] if the context has a lot of states and various components consume different states from the context for selectivity
- You can use context inside another context provided that the providers are nested properly
- Data Wrapper Pattern
- Using useMemo for the provider values
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
### Custom hook destructuring
The returning value of a custom hook can be an object or array. Destructured object must strictly use the same naming convention of the properties of the object but you can opt to not use all the returned variables. Destructured array can have aliases (the way that useState is also configured), consequently, you must return all variables in the same order.
```tsx
const useJobItems() => {
	const [job, setJob] = useState('')
	const [id, setId] = useState(0)
	return {job, id}
}

const {job, id: renamedId} = useJobItems()
```
```tsx
const useJobItems() => {
	const [job, setJob] = useState('')
	const [id, setId] = useState(0)
	return [job, id] as const
}

const [job, renamedId] = useJobItems()
```
Important to note the `as const` in the return. This makes sure that the items in destructured array will follow the exact types (each corresponding item) of the returned array. Without it, `job` and `renamedId` will both have types `string | number`. With it, `job` will be typed as `string` and `id` as `number`