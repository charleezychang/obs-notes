---------------
### HTML Semantics
Do not overuse `<div>`. Other elements include:
- section - group elements that go together visually through the page
- header
- footer
- nav
- ul
- li
- article - widget, can be stand-alone
### CSS Reset
```css
* {
	margin: 0;
	padding: 0;
	box-sizing: border-box;
}
```
### Viewport Units
[The large, small, and dynamic viewport units  |  web.dev](https://web.dev/blog/viewport-units)
### How Tailwind Works
Tailwind CSS works by scanning all of your HTML files, JavaScript components, and any other templates for class names, generating the corresponding styles and then writing them to a static CSS file (if it scans a `bg-purple-500` even in a html file, it would still be included in the final CSS file) 

So if for example line 2 is omitted, `bg-purple-500` will not be generated in the CSS thereby it wont change the color of the button.
```tsx
import { useState } from "react";
const test = "bg-purple-500"

export default function Button() {
  const [color, setColor] = useState("purple");

  return (
    <button
      className={`bg-${color}-500 text-white font-semibold py-2 px-4 rounded`}
    >
      Submit
    </button>
  );
}
```
### Tailwind: Overwriting Classes and Conditional Classes (via Object)
Tailwind does not take class order into consideration so you cannot overwrite classes in your favor. This is particularly useful when you want to add a `className` prop into a custom reusable component and then want to modify some of its styling.
Use `twMerge`: [tailwind-merge - npm (npmjs.com)](https://www.npmjs.com/package/tailwind-merge)
```tsx
import { twMerge } from 'tailwind-merge'

twMerge('px-2 py-1 bg-red hover:bg-dark-red', 'p-3 bg-[#B91C1C]')
// → 'hover:bg-dark-red p-3 bg-[#B91C1C]'
```
You can use conditional classes in `twMerge` via `&&` but it cannot do conditional classes from object. Use `clsx`: [clsx - npm (npmjs.com)](https://www.npmjs.com/package/clsx)
```tsx
import clsx from 'clsx';
// or
import { clsx } from 'clsx';

// Objects
clsx({ foo:true, bar:false, baz:isTrue() });
//=> 'foo baz'
```
Creating a utility function to use both `twMerge` and `clsx`:
```js
import { twMerge } from "tailwind-merge"
import { clsx, ClassValue } from "clsx"
// ...inputs takes in all parameters regardless of type and places them in an array
export function cn(...inputs: ClassValue[]) {
	return twMerge(clsx(inputs))
}
```
### Tailwind Dynamic Classes Using Objects
```js
function Button({ color, children }) {
  const colorVariants = {
    blue: 'bg-blue-600 hover:bg-blue-500 text-white',
    red: 'bg-red-500 hover:bg-red-400 text-white',
    yellow: 'bg-yellow-300 hover:bg-yellow-400 text-black',
  };

  return (
    <button className={colorVariants[color]}> {children} </button>
  );
}
```
### Common Tailwind Classes
- `flex`
- `flex-col`
- `flex-1`
- `flex-wrap`
- `basis-80`
- `items-center`
- `justify-between`
- `max-w-screen`
- `border-b`
- `border-white/50` 
- `h-14`
- `md:px-9`
- `mt-auto`
- `gap-x-6`
- `text-sm`
- `hover:text-white`
- `text-3xl`
- `font-bold`
- `tracking-tight`
- `leading-8`
- `opacity-75`
- `w-full`
- `rounded-lg`
- `outline-none`
- `space-x-2`
- `mx-auto`
- `object-fit`
- `object-cover
- `whitespace-nowrap`
- `transition`
- `w-[initial]`
### Add custom colors to Tailwind
In `tailwind.config.ts`
```tsx
const config: Config = {
	theme: {
		extend: {
			colors: {
				accent: "#a4f839"
			}
		}
	}
}
```
### Customize Scrollbar
`globals.css`
```css
/* SCROLLBAR STYLING */
/* Chrome, Edge, and Safari */
::-webkit-scrollbar {
  width: 10px;
}

::-webkit-scrollbar-track {
  background: #0f1015;
}

::-webkit-scrollbar-thumb {
  background-color: rgba(255, 255, 255, 0.1);
}

::-webkit-scrollbar-thumb:hover {
  background-color: rgba(255, 255, 255, 0.2);
}

/* Firefox */
* {
  scrollbar-width: thin;
  scrollbar-color: #0f1015 rgba(255, 255, 255, 0.1);
}
```
### Custom classes in Tailwind
Not recommended but in some cases, this is the best case to reduce duplicate code.
`globals.css`
```css
.state-effects {
	@apply transition hover:scale-105 active:scale-[1.02]
}
```