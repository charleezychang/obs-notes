`npm i zustand`

State management library, similar to react context but more efficient since you can specifically select which state or functions you want to extract for your components to use unlike react context where if one component uses a state and another one uses another state from the same context, both will re-render even if only one state is changed 
![[Pasted image 20240112195720.png]]

Folder inside `src` called `stores` with files named as `<noun>Store.js`
```js
import { create } from "zustand"
import { initialItems } from "../lib/constants"

type Store = {
	items: ItemType[]'
	removeAllItems: () => void;
	getFilteredItems: () => ItemType[];
	betterGetFilteredItems: (id: string) => Promise<ItemType[]>
}

export const useItemsStore = create<Store>((set, get) => ({
	items: initialItems,
	removeAllItems: () => {
		// set edits the entire store, so it must return an object
		// if a property is untouched, it is retained 
		set(() => ({
			items: []
		}))
	},
	getFilteredItems: () => {
		// get() returns the entire store
		return get().items.filter(() => ())
	},
	betterGetFilteredItems: async (id: string) => {
		const state = get();
		return state.items.filter(() => ())
	}
}))
```

Using the selectors
```tsx
import { useItemsStore } from "../stores/itemsStore"

export default function ButtonGroup() {
	const items = useItemsStore(state => state.items)
	const removeAllItems = useItemsStore(state => state.removeAllItems)

	return(
		<button onClick={removeAllItems}>{items}</button>
	)
}
```

Middleware - wrap `create`'s callback function
```tsx
import { create } from "zustand"
import { initialItems } from "../lib/constants"
import { persist } from "zustand/middleware"

export const useItemsStore = create(
	persist((set) => ({
		items: initialItems,
		removeAllItems: () => {
			set(() => ({
				items: []
			}))
		}
	}), {
		name: "items"
	})
)
```