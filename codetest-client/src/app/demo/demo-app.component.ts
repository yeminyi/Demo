import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-demo-app',
  template: `<app-sidenav></app-sidenav>`,
  styles: [
  ]
})
export class DemoAppComponent implements OnInit {

  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon('baseline-menu', sanitizer.bypassSecurityTrustResourceUrl('/assets/baseline-menu-24px.svg'));    
  
  }


  ngOnInit(): void {
  }

}
