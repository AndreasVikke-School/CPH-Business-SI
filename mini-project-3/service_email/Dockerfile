FROM python:3.8

RUN pip install kafka-python
RUN pip install fpdf
RUN pip install urllib3
RUN pip install xmltodict

COPY . .

CMD ["python", "-u", "main.py"]