
Procedural Programming
 - functions and data
 - interdependency of functions become problematic
Objective Programming
- group functions(methods) and properties

1. Encapsulation - function doesn't need parameters, less parameters = the better. HIDING implementation details and PROTECTING code from random access
```tsx
let employee = {
	baseSalary: 30000,
	overtime: 10,
	rate: 20,
	getWage: function() {
		return this.baseSalary + (this.overtime * this.rate*)
	}
}

employee.getWage()
```

2. Abstraction - hide methods and properties, simpler interface, reduce impact of change (if we change the hidden properties or methods, it will not impact the 'outside' of the object). ignoring unimportant parts of objects providing only essential interface for 'outer world'
3. Inheritance - eliminate redundant code
4. Polymorphism - many form, multiple object can use the same methods but they are executed differently