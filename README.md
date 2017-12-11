# T-I-K Asp.Net Core Microservice

using  
- [Asp.Net Core](https://docs.microsoft.com/en-us/aspnet/core/)
- [SignalR Core](https://github.com/aspnet/SignalR)
- [Akka.Net](https://github.com/akkadotnet/akka.net)
- [Hangfire](https://github.com/HangfireIO/Hangfire)
- [AngularJs](https://angularjs.org/)
- [docker-compose](https://docs.docker.com/compose/)
- [Visual Studio for Mac](https://www.visualstudio.com/vs/visual-studio-mac/)
- [Google Cloud Platform](https://cloud.google.com)

### Application layer

![GitHub Logo](https://doc-08-0c-docs.googleusercontent.com/docs/securesc/ha0ro937gcuc7l7deffksulhg5h7mbp1/cvottfr1cpvrjniotc937unrml7fm2fg/1513000800000/04579721958548200358/*/1gBHid0aHZDSv4OEXCf_nb3nudjGF4APp)

    Portal: 
        - MVC using angularJS UI and asp.net webapi. forwarding message processing service.
        - SignalR using .net signalr broadcast message to clients.
    ProcessService:
        - tik.identity token factory.
        - tik.online service route to actor ref.
        - tik.batch service  batch commands receiver.
        - tik.notification retrieve message to clients.
    Computation:
        - tik.akkaseed using akka.net actors. 
        - tik.hangfirenode using hangfire queue long runing process. 

### Consul ServiceDiscovery
![Consul ServiceDiscovery](https://doc-08-0c-docs.googleusercontent.com/docs/securesc/ha0ro937gcuc7l7deffksulhg5h7mbp1/1qukp1ehjmdfukhjqjs6qrmhi2ht1nin/1513000800000/04579721958548200358/*/1qjl14je6KfqbrwETSvs07ABM9sYt6jVQ)

    Consul service discorvery and helpcheck services that are available.

### Akka.NET online service Page
![Retrive Akka.Net](https://doc-14-0c-docs.googleusercontent.com/docs/securesc/ha0ro937gcuc7l7deffksulhg5h7mbp1/1pb2p30t28vjbcfhjemdac1nas89767r/1513000800000/04579721958548200358/*/1RA6aYDIk6uglOW9GlirGXK4Ao1wnvGCK)

    The stock list page get list from tik.online service than pipeto akkaseed.

### SignalR Page 
![SignalR feature](https://doc-0c-0c-docs.googleusercontent.com/docs/securesc/ha0ro937gcuc7l7deffksulhg5h7mbp1/n643fsbmgv8sa9pjtf5qea55e53e3t66/1513000800000/04579721958548200358/*/1DY80ZMzVEIAxHxbiNzf_z51sfhg-tsCH)

    Stock discussion feature 
    client1 add new message than message will be broadcast to another immediately.



### Run Application
    using docker-compose build images than up to docker.


### Sample Application on google cloud:

[Goto sample Application](https://www.google.com)

  