import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnrollmentService } from '../../services/enrollment.service';

@Component({
  selector: 'app-enrollment',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './enrollment.component.html',
})
export class EnrollmentComponent implements OnInit {

  enrollments: any[] = [];

  constructor(private enrollService: EnrollmentService) {}

  ngOnInit(): void {
    this.enrollments = this.enrollService.getEnrolledCourses();
  }

  remove(courseId: number) {
    this.enrollService.removeEnrollment(courseId);
    this.enrollments = this.enrollService.getEnrolledCourses(); // refresh
  }
}
