import json
from time import sleep
from json import dumps
from kafka import KafkaConsumer
from kafka import KafkaProducer
import uuid
import os
import random as rand

kafka_consumer = KafkaConsumer('loan-request',
                            group_id='service_bank_'+os.environ.get("BANKID"),
                            bootstrap_servers=['kafka:9092'],
                            api_version=(0, 11, 5))

kafka_producer = KafkaProducer(
                            bootstrap_servers=['kafka:9092'],
                            api_version=(0, 11, 5),
                            value_serializer=lambda x: 
                                dumps(x).encode('utf-8'))

while True:
    msg_pack = kafka_consumer.poll(timeout_ms=500)
    
    for tp, messages in msg_pack.items():
        for msg in messages:
            from_kafka = json.loads(msg.value)
            for i in range(8):
                newReply = {
                    "userId": msg.key.decode("utf-8"),
                    "bankId": os.environ.get("BANKID"),
                    "loanId": str(uuid.uuid4()),
                    "amount": from_kafka["amount"],
                    "monthToPay": rand.randint(from_kafka["startMonth"], from_kafka["endMonth"]),
                    "interest": rand.uniform(2, 10),
                    "AOP": rand.uniform(10, 35)
                }
                kafka_producer.send('loan-reply', value = newReply)