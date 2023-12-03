import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Estacionamento } from "src/app/Models/estacionamento.model";
import { DataService } from "src/app/services/data/data.service";

@Component({
    selector: 'app-estacionamento-page',
    templateUrl: './estacionamento-page.component.html'
  })
  export class EstacionamentoPageComponent implements OnInit{
    estacionamentos: Estacionamento[] | null;

    constructor( 
      private router: Router,
      private dataService: DataService)
    {

    }

    ngOnInit(): void {
      this
      .dataService
      .getEstacionamentos()
      .subscribe({
        next: (data: any) => {
          this.estacionamentos = data.resultado;
        },
        error: (err) => {
          console.log(err);
        }
      }); 
    }
    
  }