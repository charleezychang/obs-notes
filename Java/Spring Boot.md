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
### 🧹 `mvnw [phase]` – Clean Lifecycle Phases
- `pre-clean` – hook for tasks before cleaning.
- `clean` – deletes the `target/` directory (main cleanup).
- `post-clean` – hook for tasks after cleaning.

---
### ⚙️ `mvnw [phase]` – Default Lifecycle Phases (executed in order)

> _Running a phase will also execute all previous phases in the default lifecycle._
- `validate` – checks project structure and configuration.
- `compile` – compiles source code into bytecode.
- `test` – runs unit tests using a test framework (e.g., JUnit).
- `package` – packages code into a JAR, WAR, or other distributable format.
- `verify` – runs additional checks (like integration tests).
- `install` – installs the package into the local Maven repository (`~/.m2/repository`).
- `deploy` – deploys the package to a remote repository (CI/CD pipeline).
---
### 📄 `mvnw [phase]` – Site Lifecycle Phases
- `pre-site` – hook for tasks before documentation generation.
- `site` – generates the project documentation.
- `post-site` – hook for tasks after site generation.
- `site-deploy` – deploys the generated site to a web server or repo.

`target` directory
- where maven puts its output files
- running jar files: `java -jar <filename>` or alternatively `./mvnw sprint-boot:run` to run without packaging

Spring Framework
- connecting to database, exposing APIs
- Spring Boot extends Spring Framework by adding conveniences and defaults that reduce setup and configuration, to make Spring-based applications faster and easier to build.