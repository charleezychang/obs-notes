[The ULTIMATE Guide to Spring Boot: Spring Boot for Beginners - YouTube](https://www.youtube.com/watch?v=Nv2DERaMx-4)

[Spring Initializr](https://start.spring.io/)
- where you can setup spring boot dependencies and config and download essentially the starter app

Apache Maven
- manage their projects and all the things they need to build their programs
- know how to find and fetch dependency
- analogous to npm
- command line tool
- standard directory layout [Introduction to the Standard Directory Layout – Maven](https://maven.apache.org/guides/introduction/introduction-to-the-standard-directory-layout.html)

### 🛠️ Maven Wrapper Command Syntax

- `mvnw [options] [<goal(s)>] [<phase(s)>]`
    - `mvnw` is the Maven Wrapper (use instead of `mvn` to ensure consistent Maven versions).
---
### 🧱 Maven's 3 Standard Lifecycles
1. **clean** – removes temporary files and build artifacts.
2. **default** – the main build lifecycle (compiling, testing, packaging, etc.).
3. **site** – generates and optionally deploys project documentation.
---
### 🧹 `mvnw [goal]` – Clean Lifecycle Phases
- `pre-clean` – hook for tasks before cleaning.
- `clean` – deletes the `target/` directory (main cleanup).
- `post-clean` – hook for tasks after cleaning.

---
### ⚙️ `mvnw [goal]` – Default Lifecycle Phases (executed in order)

> _Running a phase will also execute all previous phases in the default lifecycle._
- `validate` – checks project structure and configuration.
- `compile` – compiles source code into bytecode.
- `test` – runs unit tests using a test framework (e.g., JUnit).
- `package` – packages code into a JAR, WAR, or other distributable format.
- `verify` – runs additional checks (like integration tests).
- `install` – installs the package into the local Maven repository (`~/.m2/repository`).
- `deploy` – deploys the package to a remote repository (CI/CD pipeline).
---
### 📄 `mvnw site:[goal]` – Site Lifecycle Phases
- `pre-site` – hook for tasks before documentation generation.
- `site` – generates the project documentation.
- `post-site` – hook for tasks after site generation.
- `site-deploy` – deploys the generated site to a web server or repo.

`target` directory
- where maven puts its output files
- running jar files: `java -jar <filename>` or alternatively `./mvnw sprint-boot:run` to run without packaging

Spring Framework
- connecting to database, exposing APIs
- highly configurable, downside is takes a lot of effort
- Spring Boot extends Spring Framework by adding conveniences and defaults that reduce setup and configuration, to make Spring-based applications faster and easier to build.

Spring App Layer
- Presentation
	- takes all data from service layer to expose to user (REST API via controllers, graphql)
- Service
	- use functionality of persistence layer to meet the requirements the app
- Persistence
	- interaction with database, think of entities (java objects)
	- repository pattern vs DAO (data access object)
	- CRUD

Beans
- injected via DI by the framework (inversion of control)
- allows spring to recognize it as a object that spring works with (i.e. service for DI, @RestController to enable mapping etc)
- annotate @Configuration to tell app to look into this file for beans
- annotate methods with @Bean and register the **return value** of this method (usually return type is the interface and return value is the implementation, so this is where you can swap implementations)
- means you don't' have to instantiate (new keyword) because spring will manage it for you
- beans can only be used within a bean:
	- ctr injection (similar to how you would construct properties)
	- field injection (just declare it as property) with @Autowired annotation
	- setter injection (similar to ctr but setter method is annotated with @Autowired)

Other beans (@Component and others)
- @Component - can put it in the implementation class so it AND it dependencies (in the constructor) gets managed by spring
- @Service - exact same as component just more semantically correct for services (service layer)

Component scanning
- starts when app starts up, spring will look for beans and where they are needed (will throw error if not found)
- starts on @SpringBootApplication annotation which behind the scenes implements @ComponentScan annotation for the package (the folder that contains the java file)
- @SpringBootApplcation also implements the annotations:
	- @SpringBootConfiguration>@Configuration - where to look (in terms of file) for bean annotations
	- @ComponentScan - where to look (in terms of directory) for classes/methods with beans
	- @EnableAutoConfiguration - responsible for **automatically configuring your Spring application** based on the dependencies available on the classpath (list of directories and JAR files that the JVM or compiler uses to locate compiled classes and resources.

Configuration files
- under /resources, application.properties file (can also be application.yml but will have different format)
- can override default/set application properties
- list of properties: [Common Application Properties :: Spring Boot](https://docs.spring.io/spring-boot/appendix/application-properties/index.html)

Environment variables
- important because example for docker, you use env var to setup app.properties
- convert to all caps + change period/hyphen delimiter to underscore (server.port=8181 -> SERVER_PORT=8181)
- this would already picked up by intelliJ but for external terminals you can
	- `SERVER_PORT=8181 ./mvnw sprint-boot:run`
	- or persistently, you can set it `export SERVER_PORT=8181`
	- can also configure in IntelliJ under Run/Debug Configurations

Configuration properties
- custom properties that our application need that are not exposed/provided by spring
- @ConfigurationProperties(prefix = "pizza"), then in app.prop -> `pizza.{ property }`, this annotation requires @Configuration to register it as a bean because @ConfigurationProperties needs to be on a Spring-managed bean