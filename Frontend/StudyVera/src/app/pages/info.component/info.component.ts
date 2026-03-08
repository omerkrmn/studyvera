import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Meta, Title } from '@angular/platform-browser';

@Component({
  selector: 'app-info',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit {

  constructor(
    private titleService: Title,
    private metaService: Meta
  ) {}

  ngOnInit(): void {
    this.titleService.setTitle('Study Vera Nedir? | Ücretsiz Dijital Sınava Hazırlık Koçu');

    this.metaService.addTags([
      { name: 'description', content: 'Study Vera, KPSS, YKS ve LGS öğrencilerinin eksik konularını tespit eden, soru takibi yapan ve verimliliği artıran tamamen ücretsiz bir dijital koçluk platformudur.' },
      { name: 'keywords', content: 'study vera, sınav koçu, ders takip programı, eksik konu tespiti, kpss hazırlık, yks takip, ücretsiz sınav hazırlık, pomodoro sayacı' },
      { name: 'author', content: 'Study Vera Team' },
      { name: 'robots', content: 'index, follow' },
      
      { property: 'og:type', content: 'website' },
      { property: 'og:title', content: 'Study Vera: Veriyi Stratejiye Dönüştüren Sınav Koçun' },
      { property: 'og:description', content: 'Eksiklerini veriyle tespit et, zamanını Pomodoro ile yönet. Sınav yolculuğunda Study Vera yanında!' },
      { property: 'og:url', content: 'https://studyvera.com/info' },
      
      { name: 'twitter:card', content: 'summary_large_image' },
      { name: 'twitter:title', content: 'Study Vera ile Sınava Akıllı Hazırlan' },
      { name: 'twitter:description', content: 'Sadece çalışarak değil, doğru çalışarak kazan. Ücretsiz konu ve soru takip platformu.' }
    ]);
  }
}