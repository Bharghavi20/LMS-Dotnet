import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-progress',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './progress.component.html',
})
export class ProgressComponent {

  progressData = [
    { courseName: "Angular Basics", completedLessons: 3, totalLessons: 10 },
    { courseName: "ASP.NET Core", completedLessons: 5, totalLessons: 8 },
    { courseName: "Oracle DB", completedLessons: 2, totalLessons: 6 }
  ];

  getPercentage(completed: number, total: number): number {
    return Math.round((completed / total) * 100);
  }
}
