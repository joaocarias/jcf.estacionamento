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
  export class AppHomePageComponent implements OnInit {
    estacionamentos: Estacionamento[] | null;
    estacionamento: Estacionamento | null;
    vagasMotos: EstacionamentoVeiculo[] = [];
    vagasCarros: EstacionamentoVeiculo[] = [];
    vagasGrandes: EstacionamentoVeiculo[] = [];
    selectedEstacionamento: string = "0"; 

    public messageError: string;
    public messageSuccess: string;

    constructor(
      private router: Router,
      private dataService: DataService
    )
    {
      
    }

    ngOnInit(): void {
        this.atualizaEstacionamentos();    
    }
    
    atualizaEstacionamentos(){
      this
      .dataService
      .getEstacionamentos()
      .subscribe({
        next: (data: any) => {
          this.estacionamentos = data.resultado;
          
          if(this.estacionamentos.length > 0){
            this.setEstacionamento(this.estacionamentos[0]);
          }          

          this.messageError = "";
        },
        error: (err) => {
          this.messageError = err.error.erroMensagens.map(erro => `-> ${erro}`).join('\n');
        }
      });
    }

    obterTipoVeiculo(tipo: number): string {
      switch (tipo) {
          case 1:
              return  '<span class=\"badge text-bg-primary\">Moto</span>';
          case 2:
              return '<span class=\"badge text-bg-success\">Carro</span>';
          case 3:
              return '<span class=\"badge text-bg-info\">Van</span>';
          default:
              return 'Tipo desconhecido';
      }
    }

    private removerVeiculoEstacionar(id: any) : void {
           
      this
        .dataService
        .deleteRemoverEstacionar(id)
        .subscribe({
          next: (data: any) => {
     
            this.atualizaEstacionamentos();
            
            this.messageError = "";
            this.messageSuccess = "Liberado...";
          },
          error: (err) => {
            this.messageError = err.error.erroMensagens.map(erro => `-> ${erro}`).join('\n');;
            this.messageSuccess = "";
          }
        });
    
     }

     liberarMoto(id: any){
      this.removerVeiculoEstacionar(id);
      
      const indice = this.vagasMotos.findIndex(
        (moto) => moto.id === id
      );
  
      if (indice !== -1) {
        this.vagasMotos.splice(indice, 1);
      }
     }

    liberarCarro(id: any){
      this.removerVeiculoEstacionar(id);
    
      const indice = this.vagasCarros.findIndex(
        (carro) => carro.id === id
      );
  
      if (indice !== -1) {
        this.vagasCarros.splice(indice, 1);
      }
     }

     liberarVan(id: any){
      this.removerVeiculoEstacionar(id);
    
      const indice = this.vagasGrandes.findIndex(
        (van) => van.id === id
      );
  
      if (indice !== -1) {
        this.vagasGrandes.splice(indice, 1);
      }
     }
 
    estacionarMoto(): void{
      var _novo = new EstacionamentoVeiculoDTO(this.estacionamento.id, 1);
      this.estacionar(_novo, 1);     
    }
    
    estacionarCarro(): void{
      var _novo = new EstacionamentoVeiculoDTO(this.estacionamento.id, 2);
      this.estacionar(_novo, 2);
    }
    
    estacionarVan(): void{
      var _novo = new EstacionamentoVeiculoDTO(this.estacionamento.id, 3);
      this.estacionar(_novo, 3); 
           
    }

    private atualizaTabelas(tipo: number, item: EstacionamentoVeiculo)
    {       
       if(tipo === 1){
          this.vagasMotos.push(item);
       }
       else if(tipo === 2){
          this.vagasCarros.push(item);         
       }else if(tipo === 3){
          this.vagasGrandes.push(item);
       }
    }
    
    private estacionar(_novo: EstacionamentoVeiculoDTO, tipo: number){
      this
      .dataService
      .postEstacionar(_novo)
        .subscribe({
          next: (data: any) => {            
           
            this.atualizaEstacionamentos();
           
            // this.atualizaEstacionamento(this.estacionamento.id);
        
            this.messageError = "";
            this.messageSuccess = "Estacinado...";

            // this.dataService.getEstacionamentoVeiculo(data.resultado.id)
            // .subscribe({
            //   next: (data: any) => {
            //     this.atualizaTabelas(tipo, data.resultado);
            //   }              
            // });            
          },
          error: (err) => {           
            this.messageError = err.error.erroMensagens.map(erro => `-> ${erro}`).join('\n');
            this.messageSuccess = "";
          }
        });
    }

    private setEstacionamento(meuEstacionamento: Estacionamento){
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
