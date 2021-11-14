import json
from kafka import KafkaConsumer
import smtplib, ssl, email
from fpdf import FPDF
from email import encoders
from email.mime.base import MIMEBase
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
import urllib3
import xmltodict
import os

kafka_consumer = KafkaConsumer('loan-email',
                            group_id='service_email',
                            bootstrap_servers=['kafka:9092'],
                            api_version=(0, 11, 5))
port = 465  # For SSL

# Create a secure SSL context
context = ssl.create_default_context()




with smtplib.SMTP_SSL("smtp.gmail.com", port, context=context) as server:
    server.login("mombankcorp@gmail.com", os.environ.get("EMAILPW"))

    while True:
        pdf = FPDF()
        pdf.add_page()
        pdf.set_font("Arial", size = 15)
        
        sender_email = "mombankcorp@gmail.com"

        subject = "Contract"
        body = "This is an email with attachment sent from Python"
        ms = MIMEMultipart()
        ms["From"] = sender_email
        ms["Subject"] = subject

        # Add body to email
        ms.attach(MIMEText(body, "plain"))
        msg_pack = kafka_consumer.poll(timeout_ms=500)
        
        for tp, messages in msg_pack.items():
            for msg in messages:
                from_kafka = json.loads(msg.value)
                receiver_email = from_kafka["email"]
                ms["To"] = receiver_email
                print(from_kafka)
                message = f"""\
Subject: Receipt for loan {from_kafka["loanId"]}

Hello {from_kafka["userId"]},

Amount = {from_kafka["amount"]}
months to pay = {from_kafka["monthToPay"]}
interest = {from_kafka["interest"]}
AOP = {from_kafka["AOP"]}

"""
                server.sendmail(sender_email, receiver_email, message)
                
                url = f"http://www.thomas-bayer.com/restnames/name.groovy?name={from_kafka['name']}"
                http = urllib3.PoolManager()
                response = http.request('GET', url)
                data = xmltodict.parse(response.data)
                try:
                    prefix = ""
                    print(data)
                    print(data['restnames']['nameinfo']['male'])
                    if(data['restnames']['nameinfo']['male'] == 'true') : prefix = "Mr."
                    if(data['restnames']['nameinfo']['female'] == 'true') : prefix = "Mrs."
                    if data['restnames']['nameinfo']['male'] == 'true' and data['restnames']['nameinfo']['female'] == 'true' : prefix = "Mx."
                except:
                    print("Invalid name")
                print(prefix)
                # create a cell
                pdf.cell(200, 10, txt = "Contract of loan", ln = 1, align = 'C')
                pdf.cell(200, 10, txt = f"Dear {prefix} {from_kafka['name']}", ln = 2, align = 'l')
                pdf.multi_cell(200, 10, txt = f"This presents the contract of the inquired loan from bank {from_kafka['bankId']} for a total amount of {from_kafka['amount']} ,-. You will have {from_kafka['monthToPay']} months to complete the payment with an interest of {from_kafka['interest']}%.", align = 'l')
                content = pdf.output(f"contract{from_kafka['loanId']}.pdf", 'S')
                
                part = MIMEBase("application", "octet-stream")
                part.set_payload(content)
                encoders.encode_base64(part)
                part.add_header("Content-Disposition", f"attachment; filename= contract{from_kafka['loanId']}.pdf",)
                ms.attach(part)
                text = ms.as_string()

                server.sendmail(sender_email, receiver_email, text)