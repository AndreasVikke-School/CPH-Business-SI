import json
import time
import redis
from kafka import KafkaConsumer

kafka_consumer = KafkaConsumer('loan-reply',
                            group_id='chache_service',
                            bootstrap_servers=['kafka:9092'],
                            api_version=(0, 11, 5))

while True:
    msg_pack = kafka_consumer.poll(timeout_ms=500)

    for tp, message in msg_pack.items():
        from_kafka = json.loads(message[0].value)

        r = redis.StrictRedis(host='redis', port=6379, db=0, charset='utf-8', decode_responses=True, password='P@ssword!')
        r.incr(from_kafka["userId"], 1)
        for k, v in from_kafka.items():
            r.hset('{0}:{1}'.format(from_kafka["userId"], r.get(from_kafka["userId"])), k, v)
        print(r.hgetall('{0}:{1}'.format(from_kafka["userId"], r.get(from_kafka["userId"]))))