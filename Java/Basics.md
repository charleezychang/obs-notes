[Java Full Course for free ☕ (youtube.com)](https://www.youtube.com/watch?v=xk4_1vDrzzo)

Print to console
- `System.out.print` or `System.out.println` or `sout` + `Tab`

Data Types
- boolean (p) - true or false
- byte (p) - 128
- short (p) - 32768
- int (p) - 2 billion
- long (p) - 9 quintillion, must be suffixed by L
- float (p) - 6 to 7 digits, must be suffixed by f
- double (p) - 15 digits
- char (p) - single ASCII character/value, single quotes
- String (r) - sequence of characters, double quotes

Array (fixed length)
- `String[] cars = {"derp"}` or `String[] cars = new String[length]`

`ArrayList`
- only stores reference data types, use wrapper class if want to store primitive
- `ArrayList<String> food = new ArrayList<String>()`
- `add("x")`, `get(index)`, `set(index, "x")`, `clear()`
- Nested ArrayList - `ArrayList<ArrayList<String>> = new ArrayList()`
- foreach - `for(String food: foods) {}`

String Methods
- `equals("x")`, `equalsIgnoreCase("x")` , `length()`,  `charAt(index)`, `indexOf("x")`, `isEmpty()`,  `.toUpperCase()`, `toLowerCase()`, `trim()`, `replace("x", "y")`

Wrapper Classes 
- able to use primitive data types as reference data types which exposes useful methods and can be used with collections (ex. ArrayList)
- autoboxing - automatic conversion that the compiler makes between primitive and reference types, opposite is unboxing (could still treat them as primitives)
- boolean - Boolean, char - Character, int - Integer, double - Double

Overloaded Methods
- method name + parameters = method signature
- can declare methods of the same name but different parameters (by number or type)
- this also applies to object constructors (multiple constructors in 1 class)

`final` Modifier
- basically making a variable a constant, cannot be updated
- `final double PI = 3.1416`, convention to use uppercase

`static` Modifier
- class "owns" the static variable/method so it is shared by all objects that were instantiated
- it is best to call this member not by the instantiated object, but via the class (i.e. `Friend` class with instantiated `friend1`, call `Friend.numberOfFriends`)

Overriding `toString()`
- this method prints the address of the object that is why it is usually overridden in the class to return a string representation of the object
- can be used implicitly (without having to type the method itself) or explicitly

[Hash Map - Java Programming (mooc.fi)](https://java-programming.mooc.fi/part-8/2-hash-map)
`HashMap`
- used whenever data is stored as key-value pairs, `HashMap<String, String> numbers = new HashMap<>();`
- can set initial capacity and load: `new HashMap<>(16, 0.75f)`, start with 16 empty entries and resize when entries exceed 75% of the size
- only accepts reference-variables, use wrapper class for primitive-variables (autoboxing)
- null values: can store one null key and multiple null values
- returns `null` if the hash map does not contain the key used for the search
- concurrency: HashMap is not synchronized so multiple threads accessing the map at the same time will lead to data corruption, use `ConcurrentHashMap` instead (has performance cost)
- why not use an `ArrayList` to store and make a method to search for key-value pairs? Hash Maps are more performant because they store addresses and doesn't have to go through each item
- no internal order, items are listed based on the order they are added, so cannot use index
- when using custom objects as keys, ensure you override `equals()` and `hashCode()` methods in the custom objects' classes, ensures your objects are compared based on their content rather than their memory addresses
- methods: 
	- `put(<T>, <K>)` - rewrites if existing
	- `get(<T>)` - generics can be an object
	- `getOrDefault(<T>, default)` - defaults to second parameter if retrieved value is `null`
	- `containsKey(<T>)` - useful if string keys are sanitized: `toLowerCase()` , `trim()`
	- `keySet()` - returns collection of keys of a hash map (must be used sparingly)
	- `values()` - returns collection of values of a hash map
	- `entrySet()` - returns collection of key-values of type `Map.Entry<K, V>` which exposes methods: `getKey()` and `getValue()`

Measuring performance
- use `System.nanoTime()` twice, stored in variables, and then the difference between them would be the time elapsed