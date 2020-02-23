# Balynn.BestStories

Implementation of a service which retrieves top 20 stories from Hacker News

The solution consists of the two projects:
Balynn.BestStories 
Balynn.BestStories.Tests

## Balynn.BestStories

Provides a naive implementation of the specification.  

Uses the following:

'Kestrel' - cross platform web server
'Microsoft.AspNet.WebApi.Client' - used for consumption of the source Hacker News API 
'IMemeryCache' - used for caching the results 
'DataAnnotations' - field validation 'StoryModel'

## Balynn.BestStories.Tests

Contains unit tests for the implementation. The intention is to show methodology, it is in no way representative of production ready code.   
Uses 'NUnit' and 'Moq' frameworks 


## To run

Build the project and run it as a console appplication - 'dotnet Balynn.Stories.dll' from the output folder, then navigate to: 
 
http://localhost:5000/api/v1/stories/best20


Elliot Balynn

