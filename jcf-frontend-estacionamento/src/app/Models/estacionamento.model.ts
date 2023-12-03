export class Estacionamento {
    constructor(
        public id: string | null,
        public nome: string,
        public totalVagasMoto: number,
        public totalVagasCarro: number,
        public totalVagasGrandes: number      
    ) { }
  }