import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/Models/usuario.model';
import { Security } from 'src/app/Utils/security';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit {
  public user?: Usuario | null;
  constructor(private router: Router) {

  }

  ngOnInit(): void {
    this.user = Security.getUser();
  }

  logout(): void {
    Security.clear();
    this.router.navigate(['/login']);
  }
}
