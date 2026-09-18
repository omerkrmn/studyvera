import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Title } from '@angular/platform-browser';
import { ApplicationParametersComponent } from './application-parameters/application-parameters';
import { PomodoroSettingsComponent } from './pomodoro-settings/pomodoro-settings';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [
    CommonModule,
    ApplicationParametersComponent,
    PomodoroSettingsComponent
  ],
  templateUrl: './settings.html',
  styleUrls: ['./settings.css']
})
export class Settings implements OnInit {
  private titleService = inject(Title);

  activeTab: 'application-parameters' | 'pomodoro-settings' = 'application-parameters';

  ngOnInit() {
    this.titleService.setTitle('Ayarlar | StudyVera');
  }

  changeTab(tab: 'application-parameters' | 'pomodoro-settings') {
    this.activeTab = tab;
  }
}
