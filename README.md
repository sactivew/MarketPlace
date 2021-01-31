# MarketPlace

# Tools needed to run the application:

* [Visual Studio 2019 16.8 or later](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 5.0 or later](https://dotnet.microsoft.com/download/dotnet/5.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

# Steps to get the development environment set up:

1. Clone the repository to local.
2. Make sure Docker Desktop is running.
3. At the root directory which include docker-compose.yml files, run the below command:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up –d
```
4. After the docker composes the microservice, you can launch microservice at:
http://localhost:5000/swagger/index.html and run the postman tests against http://localhost:5000/

Any questions please let me know. Thanks!
