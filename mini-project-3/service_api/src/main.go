package main

import (
	"io/ioutil"
	"log"
	"os"
	"strconv"

	"github.com/gin-gonic/gin"
	"github.com/go-redis/redis/v8"
	"github.com/segmentio/kafka-go"
	"github.com/streadway/amqp"
)

var configuration Configuration

func requestLoans(c *gin.Context) {
	userId := c.Param("userId")

	jsonData, err := ioutil.ReadAll(c.Request.Body)
	failOnError(err, "Failed to get JSON data from body")

	w := kafka.NewWriter(kafka.WriterConfig{
		Brokers: configuration.Kafka.Brokers,
		Topic:   configuration.Kafka.RequestTopic,
	})

	err = w.WriteMessages(c, kafka.Message{
		Key:   []byte(userId),
		Value: []byte(jsonData),
	})
	failOnError(err, "Failed to write message to kafka topic")

	c.Writer.Header().Set("Access-Control-Allow-Origin", "*")
	c.Data(200, "application/json; charset=utf-8", []byte(jsonData))
}

func selectLoan(c *gin.Context) {
	jsonData, err := ioutil.ReadAll(c.Request.Body)
	go failOnError(err, "Failed to get JSON data from body")

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

	c.Writer.Header().Set("Access-Control-Allow-Origin", "*")
	c.Data(200, "application/json; charset=utf-8", []byte(jsonData))
}

func getLoans(c *gin.Context) {
	userId := c.Param("userId")
	var loans []Loan

	rdb := redis.NewClient(&redis.Options{
		Addr:     configuration.Redis.Broker,
		Password: "",
		DB:       0,
	})

	keys, cursor, err := rdb.Scan(c, 0, userId+":*", 0).Result()
	failOnError(err, "Failed to scan Reddis")
	for _, key := range keys {
		values, err := rdb.HGetAll(c, key).Result()
		failOnError(err, "Failed to get by key")
		amount, _ := strconv.ParseFloat(values["amount"], 32)
		monthToPay, _ := strconv.Atoi(values["monthToPay"])
		interest, _ := strconv.ParseFloat(values["interest"], 32)
		aop, _ := strconv.ParseFloat(values["AOP"], 32)
		loans = append(loans, Loan{
			UserID:     values["userId"],
			BankID:     values["bankId"],
			LoanID:     values["loanId"],
			Amount:     amount,
			MonthToPay: monthToPay,
			Interest:   interest,
			Aop:        aop,
		})
	}

	for cursor > 0 {
		keys, cursor, err = rdb.Scan(c, cursor, userId+":*", 0).Result()
		failOnError(err, "Failed to scan Reddis")
		for _, key := range keys {
			values, err := rdb.HGetAll(c, key).Result()
			failOnError(err, "Failed to get by key")
			amount, _ := strconv.ParseFloat(values["amount"], 32)
			monthToPay, _ := strconv.Atoi(values["monthToPay"])
			interest, _ := strconv.ParseFloat(values["interest"], 32)
			aop, _ := strconv.ParseFloat(values["aop"], 32)
			loans = append(loans, Loan{
				UserID:     values["userId"],
				BankID:     values["bankId"],
				LoanID:     values["loanId"],
				Amount:     amount,
				MonthToPay: monthToPay,
				Interest:   interest,
				Aop:        aop,
			})
		}
	}

	c.Writer.Header().Set("Access-Control-Allow-Origin", "*")
	c.IndentedJSON(200, loans)
}

func failOnError(err error, msg string) {
	if err != nil {
		log.Panicf("%s: %s", msg, err)
	}
}

func main() {
	router := gin.Default()
	router.POST("/loan/request/:userId", requestLoans)
	router.POST("/loan/select/:userId", selectLoan)
	router.GET("/loan/get/:userId", getLoans)

	if len(os.Args) >= 2 {
		configuration = getConfig(os.Args[1])
	} else {
		configuration = getConfig("dev")
	}
	router.Run("0.0.0.0:8080")
}
