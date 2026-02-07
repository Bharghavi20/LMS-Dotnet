import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-courses',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin.course.html',
})
export class AdminCoursesComponent implements OnInit {

  courses: any[] = [];

  // Form Fields
  courseTitle: string = "";
  courseDescription: string = "";
  courseCategory: string = "";
  coursePrice: number = 0;

  // Edit Mode
  editMode: boolean = false;
  editCourseId: number | null = null;

  ngOnInit(): void {
    this.loadCourses();
  }

  loadCourses() {
    this.courses = JSON.parse(localStorage.getItem("courses") || "[]");
  }

  saveCourses() {
    localStorage.setItem("courses", JSON.stringify(this.courses));
  }

  addCourse() {
    if (!this.courseTitle || !this.courseDescription || !this.courseCategory) {
      alert("⚠️ Please fill all fields!");
      return;
    }

    const newCourse = {
      id: this.courses.length > 0 ? this.courses[this.courses.length - 1].id + 1 : 1,
      title: this.courseTitle,
      description: this.courseDescription,
      category: this.courseCategory,
      price: this.coursePrice
    };

    this.courses.push(newCourse);
    this.saveCourses();

    alert("✅ Course Added Successfully!");
    this.resetForm();
  }

  deleteCourse(courseId: number) {
    if (confirm("Are you sure you want to delete this course?")) {
      this.courses = this.courses.filter(c => c.id !== courseId);
      this.saveCourses();
      alert("🗑 Course Deleted Successfully!");
    }
  }

  editCourse(course: any) {
    this.editMode = true;
    this.editCourseId = course.id;

    this.courseTitle = course.title;
    this.courseDescription = course.description;
    this.courseCategory = course.category;
    this.coursePrice = course.price;
  }

  updateCourse() {
    if (this.editCourseId === null) return;

    const index = this.courses.findIndex(c => c.id === this.editCourseId);

    if (index !== -1) {
      this.courses[index].title = this.courseTitle;
      this.courses[index].description = this.courseDescription;
      this.courses[index].category = this.courseCategory;
      this.courses[index].price = this.coursePrice;

      this.saveCourses();
      alert("✅ Course Updated Successfully!");
    }

    this.resetForm();
  }

  resetForm() {
    this.courseTitle = "";
    this.courseDescription = "";
    this.courseCategory = "";
    this.coursePrice = 0;

    this.editMode = false;
    this.editCourseId = null;
  }
}
