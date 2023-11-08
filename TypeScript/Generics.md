In some cases, a function can received any type and return the same type, instead of using *any*, you can use generics
```tsx
// Problem: 
function convertToArray(input: string | number | someObject): string[] | number[] | someObject[] {
	return [input]
}

// T here is just the generic name, you can use any name
function convertToArray<T>(input: T): T[] {
	return [input]
}
// Arrow function
const convertToArray = <T>(input: T): T[] {
	return [input]
}
```

Example 2
```tsx
function getIndexOfArrayItem<T>(array: T[], arrayItem: T) {
	return array.findIndex((item) => item === arrayItem)
}

// Arrow function
const getIndexOfArrayitem = <T>(array: T[], arrayItem: T) => {
	return array.findItem((item) => item === arrayItem)
}
```

Example 3
```tsx
function createArrayPair<T, K>(input1: T, input2: K): [T, K] {
	return [input1, input2]
}
```

Example 4
```tsx
function Theme() {
	const [selectedTheme, setSelectedTheme] = useState("light");
	const themeOptions = ["light", "dark", "system"]

	return 
		<ThemeOptions 
			themeOptions={themeOptions} 
			selectedTheme={selectedTheme}    
			onThemeClick={(theme) => setSelectedTheme(theme)}
		/>
}

type ThemeOptionsProps<T extends React.ReactNode> = {
	themeOptions: T[];
	selectedTheme: T;
	onThemeClick: (theme: T) => void;
}

function ThemeOptions<T>({ themeOptions, selectedTheme }: ThemeOptionsProps<T>){
	return(
		themeOptions.map((theme, index) => {
			<li key={index}>
				<button
					onClick={() => onThemeClick(theme)}
				>
					{ theme } // Need to extend React.ReactNode
				</button> 
			</li>
		})
	)
}
```