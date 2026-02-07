import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
})
export class HomeComponent {

  username: string = "";

  constructor(private router: Router) {
    this.username = localStorage.getItem("username") || "User";
  }

  goCourses() {
    this.router.navigate(['/courses']);
  }

  goEnrollments() {
    this.router.navigate(['/enrollments']);  // ✅ correct route
  }

  goLessons() {
    this.router.navigate(['/courses']);  // lessons depends on courseId
  }

  goProgress() {
    this.router.navigate(['/progress']);
  }
  goProfile() {
  this.router.navigate(['/profile']);
}

goNotifications() {
  this.router.navigate(['/notifications']);
}


  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
