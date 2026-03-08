import { Component } from '@angular/core';
import { SeoService } from '../../services/seo.service';
import { NgForOf } from "../../../../node_modules/@angular/common/types/_common_module-chunk";

@Component({
  selector: 'app-landing',
  imports: [],
  templateUrl: './landing.html',
  styleUrl: './landing.css',
})
export class Landing {
  constructor(private seo: SeoService) {}

  ngOnInit() {
    this.seo.updateMeta(
      'StudyVera - Matematiksel Veri Odaklı Sınav Hazırlık',
      'YKS ve KPSS öğrencileri için matematiksel model destekli çalışma planlayıcı.'
    );
  }

}

