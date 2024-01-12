## Affiliate Program Management System
### Background

> [!NOTE] 
> See implementation notes and instructions at the bottom!

Your company runs an affiliate program, where partners (affiliates) can earn commissions by referring customers. You are tasked to create a backend system to manage this program. The system should handle basic operations like linking customers to affiliates and providing a basic commission report.
Requirements

* Affiliate and Customer Management: Create a RESTful API using ASP.NET Core to manage affiliates and customers. Each affiliate and customer has a unique identifier and a name. The API should support the following operations:
        Create a new affiliate
        Create a new customer linked to an affiliate
        List all affiliates
        List all customers of a specific affiliate

* Basic Commission Reporting: Your API should provide an endpoint for affiliates to see a count of their referred customers.

* Persistence: Implement a simple data persistence layer using Entity Framework Core. Use any type of data source you are comfortable with.

* Testing: Write basic unit tests for your business logic to ensure it works as expected.

### Evaluation Criteria

Your solution will be evaluated on the following criteria:

* Functionality: Does the application do what was asked?
* Code Quality: Is the code easy to understand and maintain? Is it following C# and .NET best practices?
* Testing: How well is the code tested? Are edge cases and error conditions considered?

### Submission

Please submit a link to a Git repository containing your solution, including all source code, tests, and a README with instructions on how to run and test your application.

###################################################

### Implementation Notes

Some decisions were made on purpose to show a bit of my coding style, that ofc can be discussed later on a call.

* Although for a simple(when it's super small in scope and size) API implementation as this one, splitting it into multiple layers  
and multiple projects, might be subject to discussion within a team(company policy, patterns), I personally think when it's indeed too small, 
it's better not having all layers separate into mutiple projects to keeps it simple and have a better maintenability. But in this case, in order 
to demostrate what a bigger one would(more or less) look like, I've decided to do the splitting.
Same applies to the actual existence of some layers, like in this case for instance, AffiliateService & AffiliateRepository, that for a very small
project, wouldn't make much sense, but in a bigger one, it's totallly needed to comply with SOLID principles(specially the "O" one, in this case).

* As for Unit Tests, the requirement was to have BL, edge cases and error conditions considered. Im not covering with unit tests 
all classes and methods as this would extrapolate the purpose of this exercise which I believe is to show What, How, When I do things while coding.
For that reason then, I have implemented a few unit tests, covering what was required in some classes and not in others.

* On how the projects were split(0-Presnetantion, 1-Application, ...), I like having it so that we have a clear visual separation of the layers
as well as giviging a logical separation(layer project, layer test projects, etc). Also, this helps with visually follow the clean architecture 
style where communication flow always happen in one direction only(lower layer do not see higher layers)~~, which when the project is bigger, also 
helps with preventing a sleepless developer after some pizza nights, to make wrong judgements while asking which layer is allowed to call another~~.

* SqLite was chosen, as it serves the purpose of having a database with phisical persistence, has most of basic/common features 
found in a prod-like RDBMS, at the same time no extra infrastructure tiers need to be in place, plus the database itself can get 
created on the fly.

* Although not in the requirements, I have implemented all the CRUD as they show also some extra details and it was mostly a 
mere Copy & Paste job. 
Again, just mentioning it here so that you know Im aware of the YAGNI principle, and I dont want to "breach" it.
;)

* The following features were implemented:
    * Mediator pattern within the controllers;
    * Unhandled exceptions as a middleware and not as a pipe filter;
        * Hint: It's also implemented(in the code) as a filter(but commented out) as it seems like the community favors it in a middleware instead, 
    but I've decided to leave it in the code anyways, to show how it would be done, if needed.
    * Model validation;
    * Any exceptions(model validation & unhandled exceptions) returned, in all cases, following [ProblemDetails](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0#customize-problem-details) structure;
    * API versioning according to [Microsoft Guidelines](https://github.com/Microsoft/api-guidelines/blob/master/Guidelines.md#12-versioning);


### Instructions

* Requirements:
    * Visual Studio 2022;
    * .net 8;
    * (optional if running Integration Tests from inside VS) [SpecFlow](https://specflow.org/) Visual Studio [Extension](https://marketplace.visualstudio.com/items?itemName=TechTalkSpecFlowTeam.SpecFlowForVisualStudio2022);

* Running the solution:
    * As simple as opening it up with Visual Studio 2022 and running it. Database will be automatically created and the Swagger UI 
    will show up in the browser.

* Running Unit Tests:
    * Can be done either from inside VS;
    * Can alternativelly be done by opening a prompt/bash/whatever, making sure you are in the same directory level as the .sln file 
    and runing the following:
        ```
        dotnet test
        ```

* Running Integration Tests:
    * Make sure the API is running
        * Make sure the API port is the same as in the 'appsettings.json' file located at 'tests\AffiliateService.Integration\AffiliateService.Api.Tests.Integration'
    * Can be done either from inside VS;
        * The .sln file can be fond at 'tests\AffiliateService.Integration' directory;
    * Can alternativelly be done by opening a prompt/bash/whatever, making sure you are in the same directory level as 'tests\AffiliateService.Integration'
    and runing the following:
        ```
        dotnet test
        ```