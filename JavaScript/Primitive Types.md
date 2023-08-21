[Reference](https://developer.mozilla.org/en-US/docs/Glossary/Primitive)

- `null` - the value of null
- `undefined` - the value of undefined
- `number` - all numbers including `Nan` and `Infinity`
- `boolean` - the value of true or false
- `string` - all strings including "" (empty)
- `bigint` - represent integers in the arbitrary precision format
- `symbol` - 
```
typeof null; // "object" should be "null" but its a mistake that was never corrected
typeof undefined; // "undefined"
typeof 0; // "number" (`typeof NaN` is also "number")
typeof true; // "boolean"
typeof "foo"; // "string"
typeof {}; // "object"
typeof function () {}; // "function"
typeof []; // "object"
```
