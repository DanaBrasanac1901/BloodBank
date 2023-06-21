using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

class LocationSimulator {


	static public void Main(string[] args)
	{
		var factory=new ConnectionFactory
		{
			Uri = new Uri("amqp://guest@localhost:5672")
		};

		using var connection = factory.CreateConnection();
		using var channel = connection.CreateModel();
		channel.QueueDeclare("demo",
					durable: true, //bc we want the msg to stay until its consumed
					exclusive: false,
					autoDelete: false,
					arguments: null
		);

		var message = new { Name = "Producer", Message = "Hello" };   //inst Newtonsoft.Json
		var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

		channel.BasicPublish("", "location-queue", null, body);  //drugi param - routing key
	}

}