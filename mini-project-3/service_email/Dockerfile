FROM python:3.8

RUN pip install kafka-python
RUN pip install fpdf

COPY . .

CMD ["python", "-u", "main.py"]