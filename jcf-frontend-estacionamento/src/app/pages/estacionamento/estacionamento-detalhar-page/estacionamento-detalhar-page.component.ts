import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { DataService } from "../../../services/data/data.service";
import { Estacionamento } from "src/app/Models/estacionamento.model";

@Component({
  selector: 'app-estacionamento-detalhar-page',
  templateUrl: './estacionamento-detalhar-page.component.html'
})
export class EstacionamentoDetalharPageComponent implements OnInit {
  public estacionamento?: Estacionamento | null;
  
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
      .getEstacionamento(id)
      .subscribe({
        next: (data: any) => {
          this.estacionamento = data.resultado;
        },
        error: (err) => {

        }
      });
  }

  delete(id: any): void {
    var _id = this.activeRoute.snapshot.paramMap.get("id");
    this
      .dataService
      .deleteEstacionamento(_id)
      .subscribe({
        next: (data: any) => {
          this.router.navigate(['/app/estacionamentos']);
        },
        error: (err) => {

        }
      });
  }

 }
