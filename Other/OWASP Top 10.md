**Broken Access Control**
    - **Description**: Occurs when users can act outside their intended permissions.
    - **Example**: A user can access another user’s account by modifying the URL.
    - **Countermeasure**: Implement robust access control mechanisms, enforce least privilege, and regularly review permissions.
	- **Example**: Use role-based access control (RBAC) to ensure users only have access to necessary resources.
**Cryptographic Failures**
    - **Description**: Involves failures related to cryptography, often leading to sensitive data exposure.
    - **Example**: Using weak encryption algorithms that can be easily cracked.
    - **Countermeasure**: Use strong, up-to-date encryption algorithms and ensure proper key management.
	- **Example**: Implement TLS for data in transit and AES for data at rest.
**Injection**
    - **Description**: Happens when untrusted data is sent to an interpreter as part of a command or query.
    - **Example**: SQL Injection, where an attacker can manipulate a SQL query to access unauthorized data.
    - **Countermeasure**: Incorporate security into the design phase through threat modeling and secure design patterns.
	- **Example**: Conduct regular security reviews and use design principles like least privilege and defense in depth.
**Insecure Design**
    - **Description**: Refers to flaws in the design of the application that can lead to security vulnerabilities.
    - **Example**: Lack of threat modeling or secure design patterns.
    - **Countermeasure**: Regularly update and patch systems, and use automated tools to detect misconfigurations.
	- **Example**: Implement security configuration management tools like Ansible or Chef.
**Security Misconfiguration**
    - **Description**: Occurs when security settings are not defined, implemented, or maintained properly.
    - **Example**: Default configurations, incomplete configurations, or open cloud storage.
    - **Countermeasure**: Regularly update and patch systems, and use automated tools to detect misconfigurations.
	- **Example**: Implement security configuration management tools like Ansible or Chef.
**Vulnerable and Outdated Components**
    - **Description**: Using components with known vulnerabilities can compromise the application.
    - **Example**: Using an outdated library with known security flaws.
    - **Countermeasure**: Keep software components up to date and use tools to monitor for known vulnerabilities.
	- **Example**: Use dependency management tools like Dependabot to automate updates.
**Identification and Authentication Failures**
    - **Description**: Issues related to authentication and session management.
    - **Example**: Weak password policies or session IDs that are not invalidated after logout.
    - **Countermeasure**: Implement multi-factor authentication (MFA) and secure session management practices.
	- **Example**: Use OAuth2 for secure authentication and ensure session tokens are properly invalidated.
**Software and Data Integrity Failures**
    - **Description**: Occurs when software updates, critical data, and CI/CD pipelines are not protected.
    - **Example**: An attacker can inject malicious code during a software update.
    - **Countermeasure**: Protect CI/CD pipelines and ensure integrity checks for software updates.
	- **Example**: Use digital signatures to verify the integrity of software packages.
**Security Logging and Monitoring Failures**
    - **Description**: Inadequate logging and monitoring can delay the detection of security breaches.
    - **Example**: Not logging failed login attempts or not monitoring for suspicious activities.
    - **Countermeasure**: Implement comprehensive logging and monitoring solutions to detect and respond to incidents.
	- **Example**: Use SIEM (Security Information and Event Management) systems to aggregate and analyze logs.
Server-Side Request Forgery (SSRF)**
    - **Description**: Occurs when a web application is tricked into making requests to unintended locations.
    - **Example**: An attacker can make the server send a request to an internal system that is not accessible from the outside.
    - **Countermeasure**: Validate and sanitize all user inputs and restrict outbound network access.
	- **Example**: Use allowlists to control which URLs the server can access.