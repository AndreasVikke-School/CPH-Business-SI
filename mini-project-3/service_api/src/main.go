package main

import (
	"encoding/json"
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/segmentio/kafka-go"
)

const (
	topic          = "loan-request"
	broker1Address = "kafka:9092"
)

type request struct {
	Amount int64 `json:"amount"`
	Start  int32 `json:"startMonth"`
	End    int32 `json:"endMonth"`
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

func main() {
	router := gin.Default()
	router.POST("/loan/request", requestLoans)

	router.Run("0.0.0.0:8080")
}
