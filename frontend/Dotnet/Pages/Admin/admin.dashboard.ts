import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './admin.dashboard.html'
})
export class AdminDashboardComponent {

  totalCourses = 3;
  totalLessons = 9;
  totalStudents = 12;
  totalEnrollments = 18;

  recentEnrollments = [
    { student: "Ravi", course: "Angular", date: "2026-02-01" },
    { student: "Anjali", course: "ASP.NET Core", date: "2026-02-02" },
    { student: "Kiran", course: "Oracle DB", date: "2026-02-03" },
  ];

  constructor(private router: Router) {}

  goToCourses() {
    this.router.navigate(['/admin/courses']);
  }

  goToLessons() {
    this.router.navigate(['/admin/lessons']);
  }

  goToStudents() {
    this.router.navigate(['/admin/students']);
  }

  goToEnrollments() {
    this.router.navigate(['/admin/enrollments']);
  }
}
