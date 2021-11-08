FROM python:3.8

RUN apt-get update
RUN apt-get install -y netcat
RUN pip install kafka-python
RUN pip install redis

COPY . .

CMD ["python", "-u", "main.py"]