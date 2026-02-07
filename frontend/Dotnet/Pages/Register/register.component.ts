import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
})
export class RegisterComponent {

  fullName: string = "";
  username: string = "";
  email: string = "";
  password: string = "";
  role: string = "Student";   // Student or Admin
  message: string = "";

  constructor(private router: Router) {}

  register() {

  if (!this.fullName || !this.username || !this.email || !this.password) {
    this.message = "⚠️ Please fill all fields!";
    return;
  }

  let users = JSON.parse(localStorage.getItem("users") || "[]");

  // Check if username already exists
  const userExists = users.find((u: any) => u.username === this.username);

  if (userExists) {
    this.message = "❌ Username already exists!";
    return;
  }

  const newUser = {
    fullName: this.fullName,
    username: this.username,
    email: this.email,
    password: this.password,
    role: this.role
  };

  users.push(newUser);

  localStorage.setItem("users", JSON.stringify(users));

  console.log("Users saved:", users); // ✅ DEBUG

  this.message = "✅ Registration Successful! Redirecting to Login...";

  setTimeout(() => {
    this.router.navigate(['/login']);
  }, 1000);
}

}
