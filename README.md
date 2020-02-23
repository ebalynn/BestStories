# Balynn.BestStories

Implementation of a service which retrievesthe first best 20 stories from Hacker News API

The solution consists of the two projects:
Balynn.BestStories 
Balynn.BestStories.Tests

## Balynn.BestStories

Provides a naive implementation of the specification.  

Uses the following:

'Kestrel' cross platform web server, 'Microsoft.AspNet.WebApi.Client' used for consumption of the source Hacker News API, 'IMemeryCache' for caching the results, 'DataAnnotations' for field validation of 'StoryModel'

To improve the performance of the API a simple implementation of memory caching with absolute expiration policy of 60 seconds is used.


## Balynn.BestStories.Tests

Contains unit tests for the implementation utilising NUnit and Moq. The intention is to show methodology rather than to provide the complete code coverage. 

## To run

Build the project and run it as a console appplication - 'dotnet Balynn.Stories.dll' from the output folder, then navigate to: 
 
http://localhost:5000/api/v1/stories/best20


Elliot Balynn

