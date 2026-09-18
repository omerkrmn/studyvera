import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';
import { Landing } from './pages/landing/landing';
import { Pomodoro } from './pages/pomodoro/pomodoro';
import { Sayac } from './pages/sayac/sayac';
import { InfoComponent } from './pages/info.component/info.component';
import { Login } from './pages/auth/login/login';
import { Register } from './pages/auth/register/register';
import { UserLessonProgress } from './pages/user-lesson-progress/user-lesson-progress';
import { Profile } from './pages/profile/profile';
import { UserQuestionStats } from './pages/user-question-stats/user-question-stats';
import { Settings } from './pages/profile/settings/settings';

export const routes: Routes = [
    { path: '', component: Landing },
    { path: 'pomodoro', component: Pomodoro },
    { path: 'sayac', component: Sayac },
    { path: 'info', component: InfoComponent },
    { path: 'profile/user-lesson-progresses', component: UserLessonProgress },
    { path: 'profile/settings', component: Settings, canActivate: [authGuard] },
    { path: 'profile', component: Profile },
    { path: 'profile/solved-questions', component: UserQuestionStats },
    { path: 'auth/login', component: Login },
    { path: 'auth/register', component: Register },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];