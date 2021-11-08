package main

import (
	"encoding/json"
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/segmentio/kafka-go"
	"github.com/streadway/amqp"
)

const (
	topic          = "loan-request"
	broker1Address = "kafka:9092"
	queue          = "Loan Queue"
	rabbitmq       = "amqp://admin:P@ssword!@rabbitmq:5672/"
)

type request struct {
	Amount int64 `json:"amount"`
	Start  int32 `json:"startMonth"`
	End    int32 `json:"endMonth"`
}

type lselect struct {
	Id       int64   `json:"id"`
	BankId   int64   `json:"bankId"`
	Cpr      string  `json:"cpr"`
	Name     string  `json:"name"`
	Email    string  `json:"email"`
	Amount   int64   `json:"amount"`
	MToP     int32   `json:"monthToPay"`
	Interest float32 `json:"interest"`
	AOP      float32 `json:"aop"`
}

func requestLoans(c *gin.Context) {
	var newRequest request

	if err := c.BindJSON(&newRequest); err != nil {
		return
	}
	jsonRequest, errR := json.Marshal(newRequest)
	if errR != nil {
		return
	}

	w := kafka.NewWriter(kafka.WriterConfig{
		Brokers: []string{broker1Address},
		Topic:   topic,
	})

	err := w.WriteMessages(c, kafka.Message{
		Key:   []byte("loan-request"),
		Value: []byte(jsonRequest),
	})
	if err != nil {
		panic("could not write message " + err.Error())
	}

	c.IndentedJSON(http.StatusOK, newRequest)
}

func selectLoan(c *gin.Context) {
	var newSelect lselect

	if err := c.BindJSON(&newSelect); err != nil {
		return
	}
	jsonSelect, errR := json.Marshal(newSelect)
	if errR != nil {
		return
	}

	conn, err := amqp.Dial(rabbitmq)
	if err != nil {
		panic("Failed " + err.Error())
	}
	defer conn.Close()
	ch, err := conn.Channel()
	if err != nil {
		panic("Failed " + err.Error())
	}
	defer ch.Close()

	err = ch.Publish(
		"",           // exchange
		"Loan Queue", // routing key
		false,        // mandatory
		false,        // immediate
		amqp.Publishing{
			ContentType: "text/plain",
			Body:        []byte(jsonSelect),
		})
	if err != nil {
		panic("Failed " + err.Error())
	}

	c.IndentedJSON(http.StatusOK, newSelect)
}

func main() {
	router := gin.Default()
	router.POST("/loan/request", requestLoans)
	router.POST("/loan/select", selectLoan)

	router.Run("0.0.0.0:8080")
}
