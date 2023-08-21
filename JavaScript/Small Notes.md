---------
### Shorthand Object Property
If the property of an object has the same name as a variable being assigned to it, you can write it in short form
```js
let message = "My message";

const data = {
	message: message;
}

const data = {
	message;
}
```

-------
### String() vs. toString()
Key difference is that `String()` will return null and undefined as a string while `toString()` will throw an error
```js
var value = null;
alert(value.toString()); // TypeError

var value = null;
alert(String(value)); // null
```

