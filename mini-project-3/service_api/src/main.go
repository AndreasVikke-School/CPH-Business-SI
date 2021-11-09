package main

import (
	"io/ioutil"
	"log"
	"os"

	"github.com/gin-gonic/gin"
	"github.com/segmentio/kafka-go"
	"github.com/streadway/amqp"
)

var configuration Configuration

func requestLoans(c *gin.Context) {
	jsonData, err := ioutil.ReadAll(c.Request.Body)
	failOnError(err, "Failed to get JSON data from body")

	w := kafka.NewWriter(kafka.WriterConfig{
		Brokers: configuration.Kafka.Brokers,
		Topic:   configuration.Kafka.RequestTopic,
	})

	err = w.WriteMessages(c, kafka.Message{
		Key:   []byte("loan-request"),
		Value: []byte(jsonData),
	})
	failOnError(err, "Failed to write message to kafka topic")

	c.Data(200, "application/json; charset=utf-8", []byte(jsonData))
}

func selectLoan(c *gin.Context) {
	jsonData, err := ioutil.ReadAll(c.Request.Body)
	failOnError(err, "Failed to get JSON data from body")

	conn, err := amqp.Dial("amqp://" + configuration.Rabbitmq.Username + ":" + configuration.Rabbitmq.Password + "@" + configuration.Rabbitmq.Broker)
	failOnError(err, "Failed to connect to RabbitMQ")
	defer conn.Close()

	ch, err := conn.Channel()
	failOnError(err, "Failed to open RabbitMQ channel")
	defer ch.Close()

	err = ch.Publish(
		"",                               // exchange
		configuration.Rabbitmq.LoanQueue, // routing key
		false,                            // mandatory
		false,                            // immediate
		amqp.Publishing{
			ContentType: "text/plain",
			Body:        []byte(jsonData),
		})
	failOnError(err, "Failed to Publish to RabbitMQ queue")

	c.Data(200, "application/json; charset=utf-8", []byte(jsonData))
}

func failOnError(err error, msg string) {
	if err != nil {
		log.Fatalf("%s: %s", msg, err)
	}
}

func main() {
	router := gin.Default()
	router.POST("/loan/request", requestLoans)
	router.POST("/loan/select", selectLoan)

	configuration = getConfig(os.Args[1])

	router.Run("0.0.0.0:8080")
}
