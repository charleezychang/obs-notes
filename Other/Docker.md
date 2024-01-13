[Docker Tutorial for Beginners | Dockerize Any App in 2024 (youtube.com)](https://www.youtube.com/watch?v=GFgJkfScVNU&ab_channel=JavaScriptMastery)

Advantage
1. Consistency Across Environment - no more "it works on my machine" drama
2. Isolation - clear boundary and dependency
3. Portability - easily move app between stages (dev, test, uat, etc.)
4. Version Control - like git, you can revert to a previous version
5. Scalability - more users? just create copies of the application
6. DevOps Integration

Concepts
1. Images
	-  lightweight, standalone, executable - provide code libraries and instructions
2. Containers
	- runnable instance of an image, execution environment, takes the image's instructions
3. Volumes
	- persistent data storage mechanism, data shared between container and machine or multiple containers, still exists even if container is closed
4. Network
	1- communication channel allows containers to communicate with each other or the outside world

Docker Workflow
> Docker Client
	- user interface for interacting with Docker, issue commands
> Docker Host (Daemon)
	- background process responsible for managing containers on the host system, listens for commands, create containers, builds images
> Docker Registry
> 	- centralized hub of docker images, hosts public and private

Docker Commands
- `FROM image[:tag] [AS name]` - selecting base image
- `WORKDIR /path/to/dir` - sets working directory for the following instructions
- `COPY [--chown=<user>:<group>] <src>... <dest>` - copies files or directories
- `RUN <command>` - executes commands in the shell during image build
- `EXPOSE <port> [<port>/<protocol>...]` - informs Docker that the container will listen on specified Network ports at runtime
- `ENV KEY=VALUE` - sets environment variables during build process
- `ARG <name>[=<default value>]` - defined built time variables
- `VOLUME ["/data"]` = creates a mount point for externally mounted volumes essentially specifying a location inside your container where you can connect external storages
- `CMD ["executable", "param1", "param2"]` - provides default command to execute when the container starts, can be overridden
- `ENTRYPOINT ["executable", "param1", "param2"]` - cannot be overridden