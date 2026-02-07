import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnrollmentService } from '../../services/enrollment.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './course.component.html',
})
export class CoursesComponent {

  message: string = "";

  courses = [
    { courseId: 1, title: "Angular Basics", description: "Learn Angular from scratch" },
    { courseId: 2, title: "ASP.NET Core", description: "Backend API development" },
    { courseId: 3, title: "Oracle DB", description: "Database management" }
  ];

  constructor(private enrollService: EnrollmentService) {}

  enroll(course: any) {
    this.message = this.enrollService.enrollCourse(course);
  }
}
