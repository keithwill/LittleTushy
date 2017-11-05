# Introduction 
Little Tushy is a websocket based client-server library meant to make it
easier to casually call and host back-end services.

# Why

The desire existed for a library that would allow calling out to backend services that would
fit the following goals:

*  Load Balancer Friendly
*  Low Latency
*  Efficient and Backwards Compatible Request and Reply Serialization
*  Async friendly
*  Minimal configuration and setup
*  Conventions familiar to aspnet developers (web api in particular)

There are many options for hosting or calling a backend service in .NET, but
most libraries are ambivalent about how they should be configured and incorporated
into a project, or they were designed for a different purpose.

SignalR is a great library and full featured, but was not designed for making server to
server calls. Its primary use-case is for connecting end user clients to a web server
for push notifications and RPC. For example, the client is limited to sending and receiving
only one request at a time.

ZeroMQ / NetMQ are high performance options for communicating between servers, but
their foreign multithreading model and lack of async and await support mean that they
are difficult to incorporate properly into most projects and can't always take full 
advantage of I/O completion ports. Figuring out how to transition a request behind a load
balancer is also difficult.

TcpClient and TcpListener are also high performance options, and were considered for the
implementation of this library, but they suffer from the same issues that ZeroMQ and NetMQ
do, that using them directly forces you to write more networking code than application code
and they are overly neutral about request shapes and message multiplexing. They also suffer
when trying to transition a request to a server behind a load balancer.

Of these SignalR almost meets the requirements, but was not designed in a way to meet the
goals of Little Tushy.

# What it does
Little Tushy is an asp.net core middleware that can be cofigured and added to a project
at startup with the AddLittleTushy and UseLittleTushy extension methods.

On configuration of services, the assembly that calls to AddLittleTushy is scanned for classes that inherit from ServiceController and those controllers are added to the services collection.
Those controllers are also checked for methods annotated with Action attributes that return an ActionResult and those methods are enlisted into a map of available controllers and actions. A cached invoker for that method using FasterFlect is stored with each action defintion.

On configuration of the application, a middleware is added that listens for websocket requests on a path specific to Little Tushy. Those requests are turned into websocket clients, which then
enter a loop listening for requests. When a request is received, a controller is located
from the service provider (IoC) and the requested action is invoked on the controller
using the request parameter passed (if any). Requests, results, and parameters passed back
and forth are serialized using protobuf-net.

Results are wrapped a very small result class called an ActionResult which includes the payload, an Http like StatusCode (for familiarity) and an optional message (such as for reporting details about an error code).

Inheriting from a ServiceController gives your controller classes access to a series of convenience methods for returning a payload or messages with various status codes in a way
that is similar to a web api controller returning an IHttpActionResult (e.g. 'Ok(someResult)').

# Getting Started
For a new asp.net core web api or web application:
1.  Install the Little Tushy Server nuget package
2.  Find your configure services method and call AddLittleTushy on your service collection (add the LittleTushyServer namespace first)
3.  Find your configure method and call UseLittleTushy on your application builder
4.  Add classes that inherit from ServiceController. 
5.  Add async methods to those classes that return Task<ActionResult> and annotate those classes with 'Action' attributes.
6.  In your calling application or library, install the Little Tushy Client nuget package
7.  Create an instance of ServiceClient with your first applications hostname. This client is like HttpClient, and only one client per server is necessary and is designed to be used as a shared instance. You should not instantiate and dispose of this object for every request
8.  Call RequestAsync on the client with the request and response generic type, as well as
the controller name, action name, and request parameter specified. It is safe to call this from
multiple threads at the same time, as the client maintains a very simple first-available pool of
websocket connections to the server.

<!-- # Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://www.visualstudio.com/en-us/docs/git/create-a-readme). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore) -->