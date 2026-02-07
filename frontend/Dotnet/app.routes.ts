import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/Logins/login.component';
import { RegisterComponent } from './Pages/Register/register.component';
import { HomeComponent } from './Pages/Home/home.component';
import { CoursesComponent } from './Pages/Courses/course.component';
import { EnrollmentComponent } from './Pages/Enrollment/enrollment.component';
import { LessonsComponent } from './Pages/Lesson/lessen.component';
import { ProgressComponent } from './Pages/Progress/progress.component';
import { ProfileComponent } from './Pages/Profile/profile.component';
import { NotificationsComponent } from './Pages/Notification/notification.component';

import { AdminDashboardComponent } from './Pages/Admin/admin.dashboard';
import { AdminCoursesComponent } from './Pages/Admin/admin.course';

export const routes: Routes = [
  { path: '', redirectTo: 'register', pathMatch: 'full' },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'courses', component: CoursesComponent },
  { path: 'enrollments', component: EnrollmentComponent },
  { path: 'lessons/:courseId', component: LessonsComponent },
  { path: 'progress', component: ProgressComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'notifications', component: NotificationsComponent },

  // ✅ ADMIN ROUTES (Option 2)
  { path: 'admin', component: AdminDashboardComponent },
  { path: 'admin/courses', component: AdminCoursesComponent }
];
