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
Tailwind does not take class order into consideration so you cannot overwrite classes in your favor. Use `twMerge`: [tailwind-merge - npm (npmjs.com)](https://www.npmjs.com/package/tailwind-merge)
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
- `items-center`
- 