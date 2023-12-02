import { Component } from '@angular/core';

@Component({
  selector: 'app-header-default',
  templateUrl: './header-default.component.html'
})
export class HeaderDefaultComponent {
  public title: string;

  constructor() {
    this.title = "JCF Estacionamento";
  }

}
