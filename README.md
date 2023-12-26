# MQTT Dashboard

This is a project consisting of an MQTT collector and a Web application to display the data collected. 
The application connects to an MQTT broker, collects data and stores them to a database. The user can connect to web interface and have a real time 
It's a cut-down version of a real project which for obvious reasons many features have been removed, but still remains fully functional to be used as it is, or as a base for further developing.

## Definitions

<details>

<summary>MQTT</summary>

**MQTT** stands for *Message Queuing Telemetry Transport*. It is a lightweight messaging protocol for use in cases where clients need a small code footprint and are connected to unreliable networks or networks with limited bandwidth resources. It is primarily used for machine-to-machine (M2M) communication or Internet of Things types of connections.

MQTT runs on top of TCP/IP using a PUSH/SUBSCRIBE topology. In MQTT architecture, there are two types of systems: clients and brokers. A broker is the server that the clients communicate with. The broker receives communications from clients and sends those communications on to other clients. Clients do not communicate directly with each other, but rather connect to the broker. Each client may be either a publisher, a subscriber, or both.

MQTT is an event-driven protocol. There is no periodic or ongoing data transmission. This keeps transmission to a minimum. A client only publishes when there is information to be sent, and a broker only sends out information to subscribers when new data arrives.

Messages within MQTT are published as *topics*. Topics are structures in a hierarchy using the slash (/) character as delimiter. This structure resembles that of a directory tree on a computer file system. A structure such as sensors/OilandGas/Pressure/ allows a subscriber to specify that it should only be sent data from clients that publish to the Pressure topic, or for a broader view, perhaps all data from clients that publish to any sensors/OilandGas topic. Topics are not explicitly created in MQTT. If a broker receives data published to a topic that does not currently exist, the topic is simply created, and clients may subscribe to the new topic.

***Security***

The original goal of the MQTT protocol was to make the smallest and most efficient data transmission possible over expensive, unreliable communication lines. As such, security was not a primary concern during the design and implementation of MQTT.

However, later some security options added and are available at the cost of more data transmission and a larger footprint.

**Username and password** – MQTT does allow usernames and passwords for a client to establish a connection with a broker. Unfortunately, in order to keep the overhead light, the usernames and passwords are transmitted in clear text. 

**SSL/TLS** – Running on top of TCP/IP, the obvious solution for securing transmissions between clients and brokers is the implementation of SSL/TLS. Unfortunately, this adds substantial overhead to the otherwise lightweight communications.

</details>

<details>

<summary>WebSocket API</summary>

The WebSocket API is an advanced technology that makes it possible to open a two-way interactive communication session between the user's browser and a server. With this API, you can send messages to a server and receive event-driven responses without having to poll the server for a reply.

</details>

<details>
<summary>Signal R</summary>

ASP.NET SignalR is a library for ASP.NET developers that simplifies the process of adding real-time web functionality to applications. Real-time web functionality is the ability to have server code push content to connected clients instantly as it becomes available, rather than having the server wait for a client to request new data.

SignalR can be used to add any sort of "real-time" web functionality to your ASP.NET application. While chat is often used as an example, you can do a whole lot more. Any time a user refreshes a web page to see new data, or the page implements long polling to retrieve new data, it is a candidate for using SignalR. Examples include dashboards and monitoring applications, collaborative applications (such as simultaneous editing of documents), job progress updates, and real-time forms.

SignalR also enables completely new types of web applications that require high frequency updates from the server, for example, real-time gaming.

SignalR provides a simple API for creating server-to-client remote procedure calls (RPC) that call JavaScript functions in client browsers (and other client platforms) from server-side .NET code. SignalR also includes API for connection management (for instance, connect and disconnect events), and grouping connections.

SignalR handles connection management automatically, and can broadcast messages to all connected clients simultaneously, like a chat room. It can also send messages to specific clients. The connection between the client and server is persistent, unlike a classic HTTP connection, which is re-established for each communication.

SignalR supports "server push" functionality, in which server code can call out to client code in the browser using Remote Procedure Calls (RPC), rather than the request-response model common on the web today.

SignalR applications can scale out to thousands of clients using built-in, and third-party scale-out providers.

![Signal R example ](Images/signalR.png)

</details>

## Application Structure

### Windows Service

The service is based on *BackgroundService* class which provides a way to run a long running job trivially and so to focus on the business logic inside that service. This service can be hosted a windows service (our implementation) or as a azure function.

This service will act as a MQTT client. It will connect to a broker and stay connected as long a it runs. Whenever a new alarm arrives it will inform the server through a REST endpoint (at Azure could change to Message Bus). Concurrently it will save the alarm to the database. This can be either a SQL Server database or a Mongo Db (at Azure Cosmos DB).

The format of the MQTT message would be in the form:

```
{
	"$schema": "http://json-schema.org/draft-04/schema#",
	"title": "MQQT Message",
	"description": "A product from Acme's catalog",
	"type": "object",
	"properties": {
		"applicationNSId": {
			"desscription": "The application that we are working on",
			"type": "string",
			"format": "uuid"
		},
		"siteId": {
			"desription": "The site that the sensor belongs to",
			"type": "string",
			"format": "uuid"
		},
		"sensorNSId": {
			"description": "The sensor that the alarm is sendt from",
			"type": "string",
			"format": "uuid"
		},
		"time": {
			"description": "The exact time the alarm was generated",
			"type": "string",
			"format": "date-time"
		},
		"alertStatus": {
			"description": "The status of the alarm",
			"type": "integer",
			"enum": [0, 1, 2, 3]
		},
		"latitude": {
			"type": "number",
			"description": "The latitude of the sensor"
			"minimum": -90,
			"maximum": 90
		},
		"longitude": {
			"type": "number",
			"description": "The longitude of the sensor"
			"minimum": -180,
			"maximum": 180
		},
		"temperature": {
			"type": "number",
			"description": "The temperature at the time of measurement"
		},
		"humidity": {
			"type": "number",
			"description": "The humidity at the time of measurement"
			"minimum": 0,
			"maximum": 100
		},
		"airPressure": {
			"type": "number",
			"description": "The air pressure at the time of measurement at mm Hg"
		}
	}
}
```

It sould be noted that the the alarm status takes the following values:
* 0: No Alarm
* 1: First phase alarm (might exists an alarm, but might not)
* 2: Second phase alarm (there is definitive an alarm)
* 3: Unspecified state



### ASP Core Backend



### Angular Front End