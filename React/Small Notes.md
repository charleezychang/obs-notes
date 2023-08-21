----------
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

-----
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
