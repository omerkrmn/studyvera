import { Injectable } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root',
})
export class SeoService {
  constructor(private meta: Meta, private title: Title) {}

  updateMeta(title: string, desc: string) {
    this.title.setTitle(title);
    this.meta.updateTag({ name: 'description', content: desc });
  }
}
