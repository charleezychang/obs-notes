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


### useMemo
Consider useMemo as pure optimization technique. Cache the result of a calculation between re-renders. Program should continue to work correctly even if you replace useMemo with a regular function call.
`useMemo(calculateValue, dependencies)`
```tsx
import { useMemo } from 'react';  

function TodoList({ todos, tab }) {  
	const visibleTodos = useMemo(  
		() => filterTodos(todos, tab), [todos, tab]  
	);  
	// ...  
}
```
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

### Custom hook with Context API (Template)
Consider using [[Zustand]]
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