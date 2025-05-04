[Cloud Computing Explained: The Most Important Concepts To Know](https://www.youtube.com/watch?v=ZaA0kNm18pE)

1. Scaling - need to increase capacity due to increased traffic
	1. Vertical 
		- increase CPU, Mem, Disk, Network
		- cost has diminishing returns (32gb ram cost more than 2x of 16gb)
		- single point of failure
	2. Horizontal
		- clone application and host it on different servers
		- no diminishing returns potentially lower cost
		- multiple points of failure
2. Load Balancing - way of distribution of traffic into horizontally scaled hosts
	- simple - distribute 1 by 1 per host
	- interval (?) = 10 by 10 (n by n)
	- CPU utilization
	- etc.
3. AutoScaling - automatically add more instances when some condition is met
4. Serverless - basically create/run functions on abstract hosts (no idea of the infrastructure), you dont need you setup your own host
5. Event Driven Architecture (EDA) 
	- traditional method is called request-response model, it introduces tight coupling dependencies (each services in the chain listens for the previous event)
	- send message to notification engine (SNS, EventBridge) and all dependencies are subscribed to this instead of to each other
	- but downside is the complexity if the services does affect the other services (fraud services affecting credit services), resolution: add another message, reverse/refund credit
	- pubsub:
	- Publisher - person producing the message (amazon)
	- Subscriber - services/consumers of message
6. Container Orchestration (ECS / EKS)
	- maintenance of containers
	- deployment, removal when container is down, health checks
7. Storage
	- general object - media, mp4, audio
	- block - volumes, automatically scale up/down (i.e. machine learning)
	- databases - relational, nosql (document-model)
8. Availability
	- on average, per timeframe (year, month), non-outage rate
9. Durability
	- data safe from outages
10. Infrastructure as Code (IAC)
	- issue: easy to make mistake in making resources, settings, in aws, replicating these to deploy in another host
	- the resources/settings are written in code instead of it being managed in the aws console
	- can upload to repository, can code review, easy to clone
	- CloudFormation / CDK - aws IAC
	- terraform - can hook into different providers 
11. Cloud Network
	- in aws, you can control which resources are private and public, enable/disable inbound and outbound connections for specific connectors