import { EstacionamentoVeiculo } from "./estacionamentoVeiculo";

export class Estacionamento {
    constructor(
        public id: string | null,
        public nome: string,
        public totalVagasMoto: number,
        public totalVagasCarro: number,
        public totalVagasGrandes: number,
        public vagasMotoPreenchidas: EstacionamentoVeiculo[] | null,
        public vagasCarroPreenchidas: EstacionamentoVeiculo[] | null,
        public vagasGrandePreenchidas: EstacionamentoVeiculo[] | null,  
        public vagasDisponiveisMoto: number | 0,
        public vagasDisponiveisCarro: number | 0,
        public vagasDisponiveisGrandes: number | 0,
        public totaisVagas: number | 0,
        public totalVagasDispniveis: number | 0
    ) { }
  }