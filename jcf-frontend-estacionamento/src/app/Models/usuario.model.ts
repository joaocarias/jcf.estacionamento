export class Usuario {
    constructor(
        public id: string | null,
        public nome: string,
        public email: string,
        public senha: string | null,
        public primeiroNome: string = "",
        public role: string | null,      
    ) { }
  }