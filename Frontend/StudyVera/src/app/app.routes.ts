import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';
import { Landing } from './pages/landing/landing';
import { Pomodoro } from './pages/pomodoro/pomodoro';
import { InfoComponent } from './pages/info.component/info.component';
import { Login } from './pages/auth/login/login';
import { Register } from './pages/auth/register/register';
import { UserLessonProgress } from './pages/user-lesson-progress/user-lesson-progress';

export const routes: Routes = [
    { path: '', component: Landing },
    { path: 'pomodoro', component: Pomodoro },
    { path: 'info', component: InfoComponent },
    {path: 'profile/user-lesson-progresses', component: UserLessonProgress},
    { path: 'auth/login', component: Login },
    { path: 'auth/register', component: Register },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];