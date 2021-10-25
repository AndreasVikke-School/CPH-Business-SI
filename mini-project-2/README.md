# Mini Project 2

## How to run
```
docker-compose up
```
This command will take some time to complete, be patient.

## The team

Developed by Andreas Zoega Vesterborg Vikke, Asger Hermind Sørensen, Martin Eli Frederiksen og William Sehested Huusfeldt. 

## Assignment
1. Extend the students information system by adding new services that process:
    *  teachers’ data
    * exams and exam dates

2. Enable the clients of the application to see
    * list of students who have passed their exam on System Integration, together with
    their grades
    * number of students who haven’t completed the Mini Project 2.
3. Deploy and orchestrate your microservices application in appropriate environment,
such as the Netflix family of deployment services

## Introduction

We ended up redesigning the project, thereby there's some elements that are not included in this version of the project. Courses are gone, and bookService only validates ISBN numbers, there is no functionality to add or get books.

## Project architecture
```GC: Grpc Client | GS: Grpc Server | MS: MicroService | S: Service | EF: EntityFramework | Trans: transformer```
![image](Architecture.jpg "Architecture overview")


## WebAPI & endpoints: 

Base curl url: [http://localhost:8000](http://localhost:8000)

Swagger: [http://localhost:8000/swagger/index.html](http://localhost:8000/swagger/index.html)

## Student:

add student: ```/students/add```

get all students: ```/students/get/all```

get student by id: ```/students/get/{id}```

## Book

Validate ISBN10: ``` /ISBNValidator/10/{isbn} ```

Validate ISBN13: ``` /ISBNValidator/13/{isbn} ```

## Exam

add exam: ```/exams/add```

get all exams: ```/exams/all```

get exam by id: ```/exams/{id}```

## Grade

add grade: ```/grade/add```

get all grades: ```/grade/all```

get grade by id: ```/grade/get/{id}```

get students that passed exam ``` /grade/passed ```

get students that failed exam ``` /grade/failed ```

## Teacher

add teacher: ```/teachers/add```

get all teachers: ```/teachers/all```

get teacher by id: ```/teachers/get/{id}```

