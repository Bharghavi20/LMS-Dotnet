import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-lessons',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lesson.component.html',
})
export class LessonsComponent implements OnInit {

  courseId: number = 0;
  lessons: any[] = [];
  selectedLesson: any = null;

  quizSubmitted: boolean = false;
  score: number = 0;
  totalQuestions: number = 0;

  allLessons = [

    // ==========================
    // COURSE 1: ANGULAR
    // ==========================
    {
      lessonId: 1,
      courseId: 1,
      title: "Angular Introduction",
      content: "Welcome to Angular Basics! Learn what Angular is and why it is used.",
      completed: false,
      tasks: [
        "Install Node.js and Angular CLI",
        "Create Angular project using ng new",
        "Run project using ng serve"
      ],
      quiz: [
        {
          question: "Angular is mainly used for?",
          options: ["Frontend", "Backend", "Database", "Operating System"],
          answer: "Frontend",
          selectedAnswer: ""
        }
      ]
    },
    {
      lessonId: 2,
      courseId: 1,
      title: "Angular Components",
      content: "Components are the building blocks of Angular applications.",
      completed: false,
      tasks: [
        "Create a new component using ng generate component",
        "Use selector inside app.component.html",
        "Add CSS styling to component"
      ],
      quiz: [
        {
          question: "Which decorator is used to create a component?",
          options: ["@NgModule", "@Component", "@Injectable", "@Pipe"],
          answer: "@Component",
          selectedAnswer: ""
        }
      ]
    },
    {
      lessonId: 3,
      courseId: 1,
      title: "Angular Routing",
      content: "Routing helps navigation between multiple pages in Angular.",
      completed: false,
      tasks: [
        "Create app-routing.module.ts",
        "Add routes for pages",
        "Use routerLink in navbar"
      ],
      quiz: [
        {
          question: "Which directive is used for routing?",
          options: ["href", "routerLink", "routeTo", "navigate"],
          answer: "routerLink",
          selectedAnswer: ""
        }
      ]
    },

    // ==========================
    // COURSE 2: ASP.NET CORE
    // ==========================
    {
      lessonId: 4,
      courseId: 2,
      title: "ASP.NET Core Introduction",
      content: "ASP.NET Core is a cross-platform framework used to build modern web apps and APIs.",
      completed: false,
      tasks: [
        "Install .NET SDK",
        "Create project using dotnet new webapi",
        "Run API using dotnet run"
      ],
      quiz: [
        {
          question: "ASP.NET Core is used for?",
          options: ["Frontend only", "Backend APIs", "Database", "Android Apps"],
          answer: "Backend APIs",
          selectedAnswer: ""
        }
      ]
    },
    {
      lessonId: 5,
      courseId: 2,
      title: "Controllers in ASP.NET Core",
      content: "Controllers handle HTTP requests and return responses in Web API applications.",
      completed: false,
      tasks: [
        "Create a controller using [ApiController]",
        "Add GET method returning a list",
        "Test API using Postman"
      ],
      quiz: [
        {
          question: "Which attribute is used for API controllers?",
          options: ["[Controller]", "[ApiController]", "[HttpService]", "[RouteConfig]"],
          answer: "[ApiController]",
          selectedAnswer: ""
        }
      ]
    },
    {
      lessonId: 6,
      courseId: 2,
      title: "Entity Framework Core",
      content: "EF Core is an ORM that allows database operations using C# code instead of SQL queries.",
      completed: false,
      tasks: [
        "Install EF Core package",
        "Create DbContext class",
        "Run migration using dotnet ef migrations add"
      ],
      quiz: [
        {
          question: "EF Core is mainly used for?",
          options: ["UI Design", "Database Operations", "Routing", "Authentication only"],
          answer: "Database Operations",
          selectedAnswer: ""
        }
      ]
    },

    // ==========================
    // COURSE 3: ORACLE DATABASE
    // ==========================
    {
      lessonId: 7,
      courseId: 3,
      title: "Oracle DB Introduction",
      content: "Oracle Database is a powerful relational database management system widely used in enterprises.",
      completed: false,
      tasks: [
        "Install Oracle Database or Oracle XE",
        "Install SQL Developer tool",
        "Connect to Oracle using username/password"
      ],
      quiz: [
        {
          question: "Oracle is a type of?",
          options: ["Programming Language", "Database", "Framework", "Browser"],
          answer: "Database",
          selectedAnswer: ""
        }
      ]
    },
    {
      lessonId: 8,
      courseId: 3,
      title: "Oracle SQL Basics",
      content: "Learn basic SQL queries like SELECT, INSERT, UPDATE, DELETE in Oracle.",
      completed: false,
      tasks: [
        "Create a sample table using CREATE TABLE",
        "Insert data using INSERT INTO",
        "Fetch data using SELECT query"
      ],
      quiz: [
        {
          question: "Which command is used to fetch data?",
          options: ["SELECT", "INSERT", "UPDATE", "DELETE"],
          answer: "SELECT",
          selectedAnswer: ""
        }
      ]
    },
    {
      lessonId: 9,
      courseId: 3,
      title: "PL/SQL Introduction",
      content: "PL/SQL is Oracle's procedural language extension for SQL.",
      completed: false,
      tasks: [
        "Write a simple PL/SQL block",
        "Use BEGIN and END keywords",
        "Print output using DBMS_OUTPUT.PUT_LINE"
      ],
      quiz: [
        {
          question: "PL/SQL is used for?",
          options: ["Frontend UI", "Procedural database programming", "CSS styling", "Angular routing"],
          answer: "Procedural database programming",
          selectedAnswer: ""
        }
      ]
    }
  ];

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.courseId = Number(this.route.snapshot.paramMap.get("courseId"));
    this.lessons = this.allLessons.filter(l => l.courseId === this.courseId);
  }

  openLesson(lesson: any) {
    this.selectedLesson = lesson;

    // Reset quiz state
    this.quizSubmitted = false;
    this.score = 0;
    this.totalQuestions = 0;

    // Reset selected answers
    if (this.selectedLesson.quiz) {
      this.selectedLesson.quiz.forEach((q: any) => {
        q.selectedAnswer = "";
      });
    }
  }

  // ✅ FIX: Add this method (missing in your code)
  markLessonComplete() {
    if (this.selectedLesson) {
      this.selectedLesson.completed = true;
    }
  }

  selectAnswer(question: any, option: string) {
    if (!this.quizSubmitted) {
      question.selectedAnswer = option;
    }
  }

  submitQuiz() {
    this.score = 0;
    this.totalQuestions = this.selectedLesson.quiz.length;

    this.selectedLesson.quiz.forEach((q: any) => {
      if (q.selectedAnswer === q.answer) {
        this.score++;
      }
    });

    this.quizSubmitted = true;
  }

  getResultStatus() {
    let percentage = (this.score / this.totalQuestions) * 100;
    return percentage >= 50 ? "PASS ✅" : "FAIL ❌";
  }
}
