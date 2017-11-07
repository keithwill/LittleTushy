[![AppVeyor](https://ci.appveyor.com/api/projects/status/y06lq27jfsi2iweh/branch/master?svg=true)](https://ci.appveyor.com/project/keithwill/littletushy)

[![Licensed under the MIT License](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/keithwill/LittleTushy/blob/master/LICENSE.txt)

# Introduction 
Little Tushy is a websocket based client-server library for when you need to casually call  backend service and you want the following characteristics:

*  Low Latency
*  Efficient and Backwards Compatible Request and Reply Serialization
*  Async friendly
*  Minimal configuration and setup
*  Conventions familiar to aspnet developers (web api in particular)

This project was created because:

*  SignalR doesn't have a high performance server to server client
*  Brokered Messaging adds extra network latency and project complexity
*  NetMQ thinks developers shouldn't be allowed to use threads
*  Using TcpClient / TcpListener for each project is too much boilerplate

# Requirements

See [ASP.NET Core WebSockets Prerequisites](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/websockets#prerequisites) for detailed requirements to use WebSockets.

All classes used to make requests and responses with Little Tushy must be annotated properly with protobuf-net annotations.

# Getting Started

On the Server
1.  Create an asp.net core application, and install the LittleTushy.Server nuget package
2.  In the ConfigureServices method in your Startup class, call AddLittleTushy on the services collection
3.  In the Configure method in the same file call UseLittleTushy on your application builder
4.  Add a class that inherits from ServiceController. 
5.  Add an async method to that class that returns Task<ActionResult> and annotate that method with an 'Action' attributes.

On the Client
1.  In your client application, install the LittleTushy.Client nuget package
2.  Create an instance of ServiceClient with the server and port location of your service application
3.  Keep an instance of ServiceClient around for each server you connect to. It has similar characteristics to an HttpClient. Its thread safe and has much better performance if you don't reinstantiate it every time you use it.
4.  Make a call to RequestAsync on the client, and pass in the controller and action name you setup on the server earlier.

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

# Why would I use this?

The primary use would be to call a backend or micro service from your Web API or Application tier, particularly in cases where you control the code on both sides.

Web sockets are initiated over normal Http/Https requests to a special path, and can be load balancer friendly and hosted on Azure App Services.

Protobuf serialized messages are backwards compatible, very compact, and serialize and deserialize quickly compared to XML or JSON.