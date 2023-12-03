import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { DataService } from "../../../services/data/data.service";
import { Usuario } from "src/app/Models/usuario.model";

@Component({
  selector: 'app-usuario-detalhar-page',
  templateUrl: './usuario-detalhar-page.component.html'
})
export class UsuarioDetalharPageComponent implements OnInit {
  public usuario?: Usuario | null;
  
  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private dataService: DataService
  ) 
  {
    
  }

  ngOnInit(): void {

    var id = this.activeRoute.snapshot.paramMap.get("id");
    this
      .dataService
      .getUsuario(id)
      .subscribe({
        next: (data: any) => {
          this.usuario = data.resultado;
        },
        error: (err) => {

        }
      });
  }

  delete(id: any): void {
    var _id = this.activeRoute.snapshot.paramMap.get("id");
    this
      .dataService
      .deleteUsuario(_id)
      .subscribe({
        next: (data: any) => {
          this.router.navigate(['/app/usuarios']);
        },
        error: (err) => {

        }
      });
  }
 }
