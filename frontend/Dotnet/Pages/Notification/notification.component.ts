import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-notifications',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notification.component.html',
})
export class NotificationsComponent {

  notifications: any[] = [];

  constructor(private router: Router) {
    this.notifications = [
      { id: 1, message: "🎉 Welcome to LMS!", date: "Today" },
      { id: 2, message: "📚 New course added: Angular Basics", date: "Yesterday" },
      { id: 3, message: "✅ You enrolled successfully in ASP.NET Core", date: "2 days ago" },
      { id: 4, message: "🔥 Complete lessons daily to increase your progress!", date: "3 days ago" }
    ];
  }

  clearAll() {
    this.notifications = [];
  }

  goHome() {
    this.router.navigate(['/home']);
  }
}
