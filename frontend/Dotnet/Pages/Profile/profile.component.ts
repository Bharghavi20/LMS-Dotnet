import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.component.html',
})
export class ProfileComponent {

  fullName: string = "";
  username: string = "";
  email: string = "";

  constructor(private router: Router) {
    this.fullName = localStorage.getItem("fullName") || "Student";
    this.username = localStorage.getItem("username") || "user";
    this.email = localStorage.getItem("email") || "student@email.com";
  }

  saveProfile() {
    localStorage.setItem("fullName", this.fullName);
    localStorage.setItem("username", this.username);
    localStorage.setItem("email", this.email);

    alert("✅ Profile Updated Successfully!");
  }

  goHome() {
    this.router.navigate(['/home']);
  }
}
