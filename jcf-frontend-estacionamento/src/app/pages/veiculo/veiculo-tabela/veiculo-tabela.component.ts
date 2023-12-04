import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { EstacionamentoVeiculo } from "src/app/Models/estacionamentoVeiculo";
import { Veiculo } from "src/app/Models/veiculo.model";
import { DataService } from "src/app/services/data/data.service";

@Component({
    selector: 'app-veiculo-tabela-page',
    templateUrl: './veiculo-tabela.component.html'
  })
  export class VeiculoTabelaComponent implements OnInit{
    @Input() vagasOcupadasVeiculos: EstacionamentoVeiculo[] = [];

    constructor(private activeRoute: ActivatedRoute,private dataService: DataService){

    }

    ngOnInit(): void {
        
    }
  
    obterTipoVeiculo(tipo: number): string {
        switch (tipo) {
            case 1:
                return 'Moto';
            case 2:
                return 'Carro';
            case 3:
                return 'Van';
            default:
                return 'Tipo desconhecido';
        }
    }

    removerVeiculoEstacionar(id: any) : void {
           
            this
              .dataService
              .deleteRemoverEstacionar(id)
              .subscribe({
                next: (data: any) => {
                  
                },
                error: (err) => {
        
                }
              });
    }
}
