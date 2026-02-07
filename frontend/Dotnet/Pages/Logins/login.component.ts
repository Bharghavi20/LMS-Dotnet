import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
})
export class LoginComponent {

  username: string = "";
  password: string = "";
  message: string = "";

  constructor(private router: Router) {}

  login() {

  if (this.username === "" || this.password === "") {
    this.message = "⚠️ Please enter username and password";
    return;
  }

  let users = JSON.parse(localStorage.getItem("users") || "[]");

  console.log("Stored Users:", users); // ✅ DEBUG

  const foundUser = users.find((u: any) =>
    u.username === this.username && u.password === this.password
  );

  if (foundUser) {
  this.message = "✅ Login Successful!";

  localStorage.setItem("loggedInUser", JSON.stringify(foundUser));

  if (foundUser.role.toLowerCase() === "admin") {
    this.router.navigate(['/admin/dashboard']);
  } else {
    this.router.navigate(['/home']);
  }


  } else {
    this.message = "❌ Invalid username or password";
  }
}


}
