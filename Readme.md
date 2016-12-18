# Weather App
This is an interview sample application task for the C# position.

## Objective
The interviewee should be able to show her knowledge of the following technologies/languages:

* C#
* .Net
* Windows Universal Applications SDK


## Task
Create a UWP app, targeting Mobile and Desktop, which:

* Allows a user to search for a city and add it to favorites
* Downloads weather information for cities in favorites over HTTP (API provider is up to interviewee to choose, preferably a RESTful)
* Supports offline mode (optional)
* Additionally, unit testing core functionality would be a definite plus

## Design
* Shows a grid with minimum two columns, each cell should contain at least the city name and current temperature (some image would be nice too, but it is optional). Grid and number of columns should be responsive in regard to different screen sizes ( _Hint:_ take a look into ```AdaptiveTrigger``` class). Optionally, you can also implement a "pull-to-refresh" behavior
* The rest of the application's interface is for the interviewee to decide
* NB: You could use as an inspiration the attached design page, but it is not obligatory

## Links

* [Windows Developer Tooling](http://dev.windows.com/en-us/develop/downloads)
* [Guide to Universal Windows Platform (UWP) apps](https://msdn.microsoft.com/en-us/windows/uwp/get-started/universal-application-platform-guide)
* [AdaptiveTrigger class](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.adaptivetrigger.aspx)
* [Yahoo Weather API Playground](https://developer.yahoo.com/weather/) 

An example of HTTP request which returns weather information in JSON format
> http
> GET https://query.yahooapis.com/v1/public/yql?format=json&q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text="moscow") and u='c'
```
