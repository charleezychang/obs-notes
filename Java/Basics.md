https://www.youtube.com/watch?v=xTtL8E4LzTQ

Object code (machine code)
- in bytes 0 and 1
Source code 
- understandable by humans
- compiled to machine but machine specific (compiled on mac will only run on mac)
- but have cross platform support which is byte code
Byte code
- .class file extension
- translated by JVM which is included in JDK
JDK (Java development kit)
- developer tools
- provides JRE (java runtime env) which includes JVM (java virtual machine)
- JRE - libraries and toolkits
- JVM - runs java programs

Print to console
- `System.out.print` or `System.out.println` or `sout` + `Tab`

Read input
```java
Scanner scanner = new Scanner(System.in);
// code here
scanner.close();
```

Random class (`java.util.Random`)
```java
Random random = new Random()
```

Data Types (p: primitive, r: reference)
- boolean (p) - true or false
- byte (p) - 128
- short (p) - 32768
- int (p) - 2 billion
- long (p) - 9 quintillion, must be suffixed by L
- float (p) - 6 to 7 digits decimal, must be suffixed by f (i.e `3.1414314f`)
- double (p) - 15 digits decimal, no need suffix
- char (p) - single ASCII character/value, single quotes
- String (r) - sequence of characters, double quotes

String Methods
- `equals("x")`, `equalsIgnoreCase("x")` , `length()`,  `charAt(index)`, `indexOf("x")`, `isEmpty()`,  `.toUpperCase()`, `toLowerCase()`, `trim()`, `replace("x", "y")`, `substring(start, end)`

Enhanced Switches
```java
switch(day) {
	case "Monday", "Tuesday" -> System.out.println("xxx");
	default -> System.out.println("aa")
}
```

`static` Modifier
- class "owns" the static variable/method so it is shared by all objects that were instantiated
- it is best to call this member not by the instantiated object, but via the class (i.e. `Friend` class with instantiated `friend1`, call `Friend.numberOfFriends`)
- analogy: angular class property (provided that the class is singleton (default))

Overloaded Methods
- method name + parameters = method signature
- can declare methods of the same name but different parameters (by number or type)
- this also applies to object constructors (multiple constructors in 1 class)

Array (fixed length)
- `String[] cars = {"derp"}` or `String[] cars = new String[length]`

`ArrayList`
- only stores reference data types, use wrapper class if want to store primitive
- `ArrayList<String> food = new ArrayList<String>()`
- `add("x")`, `get(index)`, `set(index, "x")`, `clear()`
- Nested ArrayList - `ArrayList<ArrayList<String>> = new ArrayList()`
- foreach - `for(String food: foods) {}`

`final` Modifier
- basically making a variable a constant, cannot be updated even in its own class (cannot set a setter)
- `final double PI = 3.1416`, convention to use uppercase

Overriding `toString()`
- this method prints the address of the object that is why it is usually overridden in the class to return a string representation of the object
- can be used implicitly (without having to type the method itself) or explicitly

OOP
```java
Car car = new Car();
Sout(car) // prints Car@<reference> so need to use . to access properties
```
- constructor - create objects by passing arguments to initialize properties
- overloaded constructor, implement ctr multiple times to accommodate different parameter
- static
- inheritance
- super - subclass (child), superclass (parent), used in inheritance, called in constructors, calls parent constructor to initialize attributes (like literally)
```java
class Student extends Person {
	double gpa;
	Student(String first, String last, double gpa) {
		super(first, last);
		this.gpa = gpa;
	}
}

class Person {
	String first;
	String last;
	Person (String first, String last) {
		this.first = first;
		this.last = last;
	}
}
```
- method overriding - subclass overwrites method of same name from superclass (add annotation of `@Override` before the method name, acts like type checking, it checks if parent class has the method to reduce human error)
- Overriding `toString()`
	- default behavior of this method from Object class is to print the address of the object
	- that is why it is usually overridden (use Override annotation) in the class to return a string representation of the object
	- can be used implicitly (without having to type `.toString()` itself) or explicitly
- abstract
	- define abstract classes and methods (abstract method can only be implemented in abstract class)
	- hide implementation and only show essential features
	- can only have a single parent
	- cannot be instantiated (somehow adds protection measures since it prevents it like how private access modifier prevents it from being accessed)
	- can contain abstract methods (which must be implemented)
	- can contain concrete methods (no `abstract` modifier, which are inherited)
	- "partially built house" — some things are already in place, but the subclass must finish building.
- interface
	- blueprint for a class that specifies a set of abstract methods
	- supports multiple inheritance-like behavior
	- "blueprint" — no implementation, just a contract to follow

| Feature                                            | Abstract Class              | Interface                                                    |
| -------------------------------------------------- | --------------------------- | ------------------------------------------------------------ |
| Can have abstract methods                          | y                           | y                                                            |
| Can force subclasses to implement methods          | y                           | y                                                            |
| Cannot be instantiated directly                    | y                           | y                                                            |
| Can have method implementations (concrete methods) | y                           | Yes (in modern versions like Java 8+ with `default` methods) |
| Can have constructors                              | y                           | n                                                            |
| Can have fields (state)                            | Yes (with state)            | (constants only)                                             |
| Supports multiple inheritance                      | n                           | y                                                            |
| Use when...                                        | You want to share base code | You want to define a strict contract or capability           |

- Polymorphism - (many) (shape), if a subclass can be instantiated as the parent class, but parent class cannot be instantiated as the subclass
- Runtime/Dynamic polymorphism - method executed is decided at runtime based on actual type of the object, example, class is typed as parent class and it instantiated as subclass depending on a logic
- Getters/setters - getters - set access to private, setters - basically a mini-constructor
- aggregation - book class can standalone even if library remove Books[] as its property. books and library are instantiated in the same class
- composition - engine must be part of a car. engine class is instantiated in the car class constructor
- Wrapper Classes 
	- able to use primitive data types as reference data types which exposes useful methods and can be used with collections (ex. ArrayList)
	- autoboxing - automatic conversion that the compiler makes between primitive and reference types, opposite is unboxing (could still treat them as primitives)
	- because of autoboxing, you dont have to instantiate `Integer x = 2`
	- autoboxing int y = x
	- boolean - Boolean, char - Character, int - Integer, double - Double
- exception handling - try catch (can have multiple) finally, gracefully handle exception so it doesnt crash the program, catching all = `catch (Exception e)`
- writing files
	- FileWriter - small or medium text files
```java
// FileWrite can accept directory
// use triple set of double quotes for multi-line string
try(FileWriter writer = new FileWriter("text.txt)) {
	writer.write("asd");
}
catch(IOExceiption e) {
}
````
`
	- BufferedWriter - better for large amounts of text
	- PrintWriter - structured data (like reports or logs)
	- FileOutputStream - binary files (images, audio files)
- reading files
	- BufferedReader + FileReader - best for reading text files line by line
	- FileInputStream - binary (iamges and audio files)
	- RandomAccessFile - best for read/write specific portions of a large file
```java
// reader is autoclose
try(BufferedReader reader = new BufferedReader(new FileReader(filePath))) {
	String line;
	while((line = reader.readLine()) != null) {
		Sout(line);
	}
}
catch(FileNotFoundException e) {}
```
- date and time 
	- `LocalDate date = LocalDate.now()` -> YYYY-MM-DD
	- `LocalTime time = LocalTime.now()` -> HH:mm:ss
	- `LocalDateTime dateTime = LocalDateTime.now()` -> YYYY-MM-DDTHH:mm:ss
	- `Instant instant = Instant.now()` => Date and now in UTC+0
	- `DateFormatter formatter = DateTimeFormatter.ofPattern("dd-MM-yyyy"); String newDateTime = dateTime.format(formatter)` 
	- `LocalDate date = LocalDate.of(2024, 12, 25)`
	- `LocalDateTime date1 = LocalDateTime.of(2024, 12, 25, 12, 0, 0)`
	- `LocalDateTime date2 = LocalDateTime.of(2025, 1, 1, 0, 0, 0)`
	- `date1.isBefore(date2)`
- anonymous class - class without name, cannot be reused, used for one time uses (TimerTask, Runnable, callbacks) `Dog dog = new Dog() { @Override void speak() { // action here } };`
- enums - all letters a capitalized
```java
public enum Day {
	SUNDAY(1), MONDAY(2), TUESDAY(3), WEDNESDAY(4), THURSDAY(5);
	private final int dayNumber;
	Day(int dayNumber) {
		this.dayNumber = dayNumber;
	}
	public int getDayNumber() {
		return this.dayNumber;
	}
}

public void method() {
	Day day = Day.SUNDAY;
	Sout(day); // SUNDAY
	Sout(day.getDayNumber()); // 1
}
```

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