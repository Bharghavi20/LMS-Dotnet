import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EnrollmentService {

  private courses = [
    { courseId: 1, title: "Angular Basics", description: "Learn Angular from scratch" },
    { courseId: 2, title: "ASP.NET Core", description: "Backend API development" },
    { courseId: 3, title: "Oracle DB", description: "Database management" }
  ];

  private enrolledCourses: any[] = [];

  constructor() {
    // ✅ Load enrolled courses from localStorage when service starts
    const saved = localStorage.getItem("enrolledCourses");
    if (saved) {
      this.enrolledCourses = JSON.parse(saved);
    }
  }

  // ✅ return all available courses
  getCourses() {
    return this.courses;
  }

  enrollCourse(course: any) {
    const alreadyEnrolled = this.enrolledCourses.find(c => c.courseId === course.courseId);

    if (alreadyEnrolled) {
      return "⚠️ Already enrolled!";
    }

    this.enrolledCourses.push(course);

    // ✅ Save to localStorage
    localStorage.setItem("enrolledCourses", JSON.stringify(this.enrolledCourses));

    return "✅ Enrolled successfully!";
  }

  getEnrolledCourses() {
    return this.enrolledCourses;
  }

  removeEnrollment(courseId: number) {
    this.enrolledCourses = this.enrolledCourses.filter(c => c.courseId !== courseId);

    // ✅ Update localStorage
    localStorage.setItem("enrolledCourses", JSON.stringify(this.enrolledCourses));
  }
}
