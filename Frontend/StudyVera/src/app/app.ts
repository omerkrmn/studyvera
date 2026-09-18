import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./components/navbar.component/navbar.component";
import { GlobalPomodoroBar } from "./components/global-pomodoro-bar/global-pomodoro-bar";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, GlobalPomodoroBar],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('StudyVera');
}
