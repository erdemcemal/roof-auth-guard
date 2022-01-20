# Roof Auth Guard
![client-credentials](https://user-images.githubusercontent.com/56685124/150406631-23d74418-2876-47c3-b61a-82b9ec13c906.jpg)

## Client credentials flow

Client credentials flow is a server-to-server communication. It doesn't require a user interaction. It basically work like below;
1. Client sends credentials (Client-Id and Client-Secret) to auth server.
2. Auth server verify and return a access token
3. Client use that access token to get resorces from other resources api

## USAGE
Run all the projects in kestrel server
1. Roof.API (https://localhost:7234)
2. Roof.AuthGuardServer (https://localhost:5001)
3. Roof.ClientAPI(https://localhost:7007)

There are two endpoints in Roof.API project to consume from Client project. These are;
1. (get)  api/employees => Get Employee List
2. (post) api/employees => Create new Employee

**Get Employee List Steps**
1. Client request auth server with credentials ( client-id and client secret ) and get access token
2. Client send request to employee api service to get employee list with access token
3. Employee service validate access token and return employee list successfully


**Create New Employe Steps**
1. Client request auth server with credentials ( client-id and client secret ) and get access token (client has only read scope)
2. Client send request to employee api service to get employee list with access token
3. Employee service validate access token but since client doesn't have create empleyoee authorization then return forbidden status 
