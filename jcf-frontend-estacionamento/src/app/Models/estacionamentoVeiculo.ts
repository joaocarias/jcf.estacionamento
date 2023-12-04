import { Estacionamento } from "./estacionamento.model";
import { Veiculo } from "./veiculo.model";

export class EstacionamentoVeiculo {
    constructor(
        public id:  string,
        public estacionamentoId: string,
        public estacionamento: Estacionamento | null,
        public veiculoId: string,
        public veiculo: Veiculo | null,
        public tipo: number,
        public Ocupacao: number
    ) { }
  }