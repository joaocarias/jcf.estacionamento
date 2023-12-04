import { Component, Input, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { Router } from "@angular/router";
import { Estacionamento } from "src/app/Models/estacionamento.model";
import { EstacionamentoVeiculo } from "src/app/Models/estacionamentoVeiculo";
import { EstacionamentoVeiculoDTO } from "src/app/Models/estacionamentoVeiculoDTO.model";
import { DataService } from "src/app/services/data/data.service";

@Component({
    selector: 'app-app-home-page',
    templateUrl: './app-home-page.component.html'
  })
  export class AppHomePageComponent implements OnInit, OnChanges{
    estacionamentos: Estacionamento[] | null;
    estacionamento: Estacionamento | null;
    vagasMotos: EstacionamentoVeiculo[] = [];
    vagasCarros: EstacionamentoVeiculo[] = [];
    vagasGrandes: EstacionamentoVeiculo[] = [];
    selectedEstacionamento: string = "0"; 

    public messageError: string;

    constructor(
      private router: Router,
      private dataService: DataService
    )
    {
      
    }

    estacionamentoDados(){
      const estacionamentoSelecionado = this.estacionamentos.find(e => e.id === this.selectedEstacionamento);
      this.atualizaEstacionamento(estacionamentoSelecionado.id);
      this.setEstacionamento(estacionamentoSelecionado);
    }
  
    ngOnChanges(changes: SimpleChanges): void
    {

    }

    ngOnInit(): void {
      this
      .dataService
      .getEstacionamentos()
      .subscribe({
        next: (data: any) => {
          this.estacionamentos = data.resultado;
          
          if(this.estacionamentos.length == 1){
            this.setEstacionamento(this.estacionamentos[0]);
          }
        },
        error: (err) => {
          console.log(err);
        }
      });       
    }
    
    estacionarMoto(): void{
      console.log("estacionar uma Moto no estacionamento: " + this.estacionamento.id);    
        
      var _novo = new EstacionamentoVeiculoDTO(this.estacionamento.id, 1);
      this.estacionar(_novo);     
    }
    
    estacionarCarro(): void{
      console.log("estacionar uma Carro no estacionamento: " + this.estacionamento.id); 
      
      var _novo = new EstacionamentoVeiculoDTO(this.estacionamento.id, 2);
      this.estacionar(_novo);
    }
    
    estacionarVan(): void{
      console.log("estacionar uma Van no estacionamento: " + this.estacionamento.id);

      var _novo = new EstacionamentoVeiculoDTO(this.estacionamento.id, 3);
      this.estacionar(_novo);      
    }

    private estacionar(_novo: EstacionamentoVeiculoDTO){
      this
      .dataService
      .postEstacionar(_novo)
        .subscribe({
          next: (data: any) => {            
            this.atualizaEstacionamento(this.estacionamento.id);
            this.setEstacionamento(this.estacionamento);
            
            this.messageError = "";
          },
          error: (err) => {           
            this.messageError = err.error.erroMensagens.map(erro => `-> ${erro}`).join('\n');
          }
        });
    }

    setEstacionamento(meuEstacionamento: Estacionamento){
      this.estacionamento = meuEstacionamento;

      this.vagasMotos = meuEstacionamento.vagasMotoPreenchidas;

      this.vagasCarros = meuEstacionamento.vagasCarroPreenchidas;

      this.vagasGrandes = meuEstacionamento.vagasGrandePreenchidas;
    }

    atualizaEstacionamento(id: any) {
      this
      .dataService
      .getEstacionamento(id)
      .subscribe({
        next: (data: any) => {                    
          this.estacionamento = data.resultado;
        },
        error: (err) => {
          return null; 
        }
      });
    }
  }
