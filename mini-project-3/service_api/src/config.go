package main

import (
	"encoding/json"
	"os"
)

type Configuration struct {
	Kafka struct {
		RequestTopic string   `json:"request_topic"`
		Brokers      []string `json:"brokers"`
	} `json:"kafka"`
	Rabbitmq struct {
		LoanQueue string `json:"loan_queue"`
		Broker    string `json:"broker"`
		Username  string `json:"username"`
		Password  string `json:"password"`
	} `json:"rabbitmq"`
	Redis struct {
		Broker   string `json:"broker"`
		Password string `json:"password"`
	} `json:"redis"`
}

func getConfig(env string) Configuration {
	file, _ := os.Open("config/" + env + "_conf.json")
	defer file.Close()

	decoder := json.NewDecoder(file)
	configuration := Configuration{}
	err := decoder.Decode(&configuration)
	go failOnError(err, "Failed to Decode Env File")
	return configuration
}
