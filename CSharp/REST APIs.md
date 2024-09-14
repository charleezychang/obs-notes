## Start Fresh
```
dotnet new sln --name <name>
mkdir src
mkdir tests
cd src
dotnet new webapi -o <name>
cd ..
dotnet new editorconfig
dotnet new globaljson
dotnet new gitignore
// not working, add manually
dotnet sln add (ls -r **/**.csproj)
dotnet sln add ./src/Olympia/Olympia.csproj
code .
```
1. Remove http file
2. In `Program.cs`, only retain 3 lines, `builder`, `app`, and `Run()`. Can also add scoping braces
3. In `.csproj`, remove `ItemGroup` and children
4. Create folders: Controllers, Domain, Persistence, Services
5. Running: `dotnet run --projetc src/<ProjectFolder>`

[How To Design Amazing REST APIs - YouTube](https://www.youtube.com/watch?v=A8t5LSxVJFM)
# Designing REST APIs
## URI
1. Noun
2. Plural
3. Nest resources - `products/{product-id}`, max 3
4. Consistency - consistent casing, and naming
5. Versionioning - `/v1/products`

## HTTP Methods
1. `GET`
	1. Collection - pagination, filtering, sorting
	2. Single
	3. Request
		1. Caching Details (If-None-Match etc), 
		2. No Body
	4. Response
		1. Headers: Caching details (Last-Modified, Etag, etc.)
		2. Status Code: 304 Not Modified, 200 Ok
2. `POST`
	1. Collection - creates resource
	2. Single - Initiates action/processing
	3. Request
	4. Response
		1. Headers: Location (uri to fetch the new resource)
		2. Status Code: 201 Created, 202 Accepted
3. `PUT`
	1. Collection - never
	2. Single: Create(Optional) or Update
	3. Request
		1. Headers: If-match
		2. Body: Complete resource
	4. Status Code: 201 Created/404 Not Found, 200 Ok/204 No Content/412 Precondition Failed
4. `DELETE`
	1. Collection - never
	2. Single: Delete resource
	3. Request
		1. Headers: If-match
		2. Body: No Body
	4. Response
		1. Status Code: 204 No Content, 412 Precondition Failed
## Idempotency
- server will not change in state for the same request
- not dependent on the response, i.e. if first delete call returns 204, and succeeding calls return 412, it still does not change the state of the server
- YES: GET, PUT, DELETE
- NO: POST
## Safe and Unsafe
- method is safe if it does not modify data in the server
- YES: GET
- NO: PUT, DELETE, POST
## Error Handling
- 4xx - Client error
- 5xx - Server error
# Sending HTTP Requests
## `http` file type
- use `###` to separate requests in one file
- using extension `REST Client`, you can add a file variable via `@`
```http
@token=ey...

POST http://localhost:5000/products
Authorization: Bearer token
Content-Type: application/json
{
	"Name": "Max book"
}
```
- to make this variable, be an environment variable, go to Settings > `rest env`
- to switch environments `Ctrl + P` -> `Rest Client: Switch Environment`
```json
{
	"rest-client.environmentVariables": {
		"$shared": {
			"token": "ey..."
		},
		"dev": {
			"token": "ey..."
		},
		"test": {
			"token": "ey..."
		}
	}
}
```

# Presentation vs. Application vs Domain Logic
- Presentation is the UI, controllers
- Application handles the "movement" of data, CRUD
- Domain logic is the business rules for the data, models and validation
- Presentation interacts with Application, Application interacts with Domain