S - Single Responsibility
> Classes should have closely related properties and methods
> Consider breaking methods into services

O - Open Closed Principle
> Classes should be open to extension but closed for modification
> Dependency Injection

L - Liskov Substitution
> Child class should do everything that a parent class could do
> i.e. If parent class has work() method, child class doesn't, consider making them both inherit from a human class that doesn't implement work() method

I - Interface Segregation
> Bulky interface breaks Liskov, consider breaking them down into smaller ones and use a main interface that inherits the smaller interfaces

D - Dependency Inversion
> High level modules shouldn't depend on low level modules, they should both depend on an abstraction (interface)
> You could instantiate a lower module class into the higher one but it will be hard to test or difficult to modify if ever the dependency is changed (i.e. if you have two databases and want to switch between them)