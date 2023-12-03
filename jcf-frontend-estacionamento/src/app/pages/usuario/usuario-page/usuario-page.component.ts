import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Usuario } from "src/app/Models/usuario.model";
import { DataService } from "src/app/services/data/data.service";

@Component({
    selector: 'app-usuario-page',
    templateUrl: './usuario-page.component.html'
  })
  export class UsuarioPageComponent implements OnInit{
    usuarios: Usuario[] | null;

    constructor( 
      private router: Router,
      private dataService: DataService)
    {

    }

    ngOnInit(): void {
      this
      .dataService
      .getUsuarios()
      .subscribe({
        next: (data: any) => {
          this.usuarios = data.resultado;
        },
        error: (err) => {
          console.log(err);
        }
      }); 
    }
    
  }