import json
import redis
from kafka import KafkaConsumer

kafka_consumer = KafkaConsumer(group_id='cache_service', bootstrap_servers='kafka:9092')
kafka_consumer.subscribe(['loan-reply'])

while True:
    try:
        message = kafka_consumer.poll(timeout_ms=1000)

        from_kafka = json.load(message.value().decode())

        r = redis.StrictRedis(host='redis', port=6379, db=0, charset='utf-8', decode_responses=True, password='P@ssword!')
        r.incr(from_kafka["userId"], 1)
        for k, v in from_kafka.items():
            r.hset('{0}:{1}'.format(from_kafka["userId"], r.get(from_kafka["userId"])), k, v)
        print(r.hgetall('{0}:{1}'.format(from_kafka["userId"], r.get(from_kafka["userId"]))))
    except:
        print('Failed')
    finally:
        kafka_consumer.close()