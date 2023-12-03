export class Usuario {
    constructor(
        public _id: string | null,
        public nome: string,
        public email: string,
        public senha: string | null,
        public primeiroNome: string = ""      
    ) { }
  }