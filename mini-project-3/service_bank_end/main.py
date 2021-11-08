import json
from time import sleep
from json import dumps
from kafka import KafkaConsumer
from kafka import KafkaProducer
import uuid
import os
import random as rand
import pika

sleep(30)
kafka_producer = KafkaProducer(
                            bootstrap_servers=['kafka:9092'],
                            api_version=(0, 11, 5),
                            value_serializer=lambda x: 
                                dumps(x).encode('utf-8'))

credentials = pika.PlainCredentials('admin', 'P@ssword!')
connection = pika.BlockingConnection(pika.ConnectionParameters('rabbitmq', 5672, '/', credentials, heartbeat=0))
channel = connection.channel()

def callback(ch,method,properties,body):
    data = json.loads(body)
    if(data["bankId"] == os.environ.get("BANKID")):
        ch.basic_ack(delivery_tag = method.delivery_tag)
        kafka_producer.send('loan-email', value = data)
        print("Data sent to: loan-email")
    else:
        ch.basic_reject(delivery_tag = method.delivery_tag, requeue = True)
    
channel.basic_consume(queue='Loan Queue',
                      on_message_callback=callback)

channel.start_consuming()