# Rabbit MQ (Dotnet Example)
Demonstrates:
1. Using docker to run RabbitMQ.
2. Producer that adds messages to the `hello` queue.
3. Consumer that reads and `Console.WriteLine()`s the messages from the `hello` queue.

---

## Pre-requisites
- [Docker desktop](https://www.docker.com/products/docker-desktop/)
- [dotnet (6.0)](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

---

## Setup
Before running the console apps, make sure the RabbitMQ service is running.

The `docker-compose.yaml` file contains the required configuration to start the RabbitMQ service.

1. Make sure docker desktop and the docker service are running.
2. Run `docker compose up` in a terminal.
   - After the initial pull has completed, the following ports will be bound to the docker image:
     - `5672`: Used for the RabbitMQ broker
     - `15672`: Used for the RabbitMQ management interface (accessible at http://localhost:15672)

> **Note**:
>
> If the Rabbit MQ management interface (http://localhost:15672) asks for credentials, you can use `guest` as the username and `guest` as the password.
>
> As a safety measure, these credentials will only work on `localhost`.
> 
> For more information, refer to the [official RabbitMQ docker image](https://hub.docker.com/_/rabbitmq) documentation page.

To cancel the docker compose session, press `CTRL+C` in the terminal window where the above `docker compose up` command is running.
   - This will unbind the ports listed above

---

## Executing the sample
1. Open a terminal window, follow the steps in the [Setup](#setup) section.
2. Open a second terminal window, to act as the publisher:
   1. Change directory into `producer/`.
   ```cd
   cd ./producer
   ```
   2. Run the following command to send `Example Message 12345` to the queue.
   ```sh
   dotnet run -- Example Message 12345
   ```
3. Open a third terminal window to act as the consumer:
   1. Change directory into `consumer/`.
   ```sh
   cd ./consumer
   ```
   2. Run the following command to start the Consumer. All messages sent to the consumer will be printed to the console window.
   ```sh
   dotnet run
   ```
   3. To exit the consumer, press the `Enter` key.

To send subsequent messages, repeat step `2.2` as required. The messages will be picked up by the consumer while it is running.

---

## References
- https://www.architect.io/blog/2021-01-19/rabbitmq-docker-tutorial/
- https://hub.docker.com/_/rabbitmq
- https://docs.docker.com/compose/