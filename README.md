# Hacker News Test

### How to run the application

* The app is hosted on Google Cloud, simply visit [here](https://hackernewstest-jugtoax2fq-nw.a.run.app/swagger/index.html) to test it out via Swagger.


* You can run the app via [Docker](https://www.docker.com/)
>Go to ..\HackerNews\Api folder via command prompt/terminal
> 
>docker build -t hackernewstest .
> 
>docker run -d -p 5627:80 hackernewstest
>
> Visit http://localhost:5627/swagger/index.html

* You can run the app via [ASP.NET Core Runtime 7.0.11](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

>Go to ..\HackerNews\Api folder via command prompt/terminal
>
>dotnet run
>
> Visit http://localhost:5287/swagger/index.html

* To run unit tests 
>Go to ..\HackerNews\Tests folder via command prompt/terminal
>
>dotnet test

### Assumptions
* Best story ids returned by the Hacker News API is always ordered descending by the score.
* Returning an empty response is acceptable on some cases instead of an error.

### Enhancements
* Cross cutting concerns such as authentication, logging/uncaught exception handling, caching etc. could be implemented via middlewares.
* On top of memory cache, a distributed cache ie. Redis could be implemented considering this service/other services would have more than one instance running and they might want to share the cache.
* On top of unit tests, integration and load tests could be added.
* No exception is handled, exceptions can be handled depending on the service's desired behaviour ie. returning no/partial results by ignoring upstream service errors.
* Circuit breaker could be implemented to mitigate upstream service delays/failures.
* More unit tests could be added to cover more scenarios and increase coverage.
* Validations could be added.
