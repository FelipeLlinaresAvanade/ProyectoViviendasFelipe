import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
  <nav class='navbar navbar-expand navbar-light bg-light'>
    <a class='navbar-brand'>{{pageTitle}}</a>
    <ul class='nav nav-pills'>
      <li><a class='nav-link' routerLink='/welcome'>Inicio</a></li>
      <li><a class='nav-link' routerLink='/viviendas'>Lista de Viviendas</a></li>
      <li><a class='nav-link' routerLink='/contacto'>Contacto</a></li>
    </ul>
  </nav>
  <div class='container'>
    <router-outlet></router-outlet>
  </div>
  `
})
export class AppComponent {
  pageTitle = 'Web de Viviendas Felipe';
}
