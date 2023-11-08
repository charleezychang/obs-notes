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