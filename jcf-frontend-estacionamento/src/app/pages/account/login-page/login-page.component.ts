import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Usuario } from "src/app/Models/usuario.model";
import { Security } from "src/app/Utils/security";
import { DataService } from "src/app/services/data/data.service";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html'
})
export class LoginPageComponent implements OnInit {
  public form: FormGroup;
  public messageError: string;

  constructor(
    private router: Router,
    private dataService: DataService,
    private fb: FormBuilder
  ) 
  {
    this.form = this.fb.group({
      username: ['', Validators.compose([
        Validators.minLength(5),
        Validators.maxLength(200),
        Validators.email,
        Validators.required
      ])],
      senha: ['', Validators.compose([
        Validators.minLength(6),
        Validators.maxLength(16),
        Validators.required
      ])]
    });

    this.messageError = "";
  }

  ngOnInit(): void {
    
  }

  submit(): void {
    this
      .dataService
      .authenticate(this.form.value)
      .subscribe({
        next: (data: any) => {
          console.log(data);
          this.setUser(new Usuario(data.resultado.usuario.Id, 
                                    data.resultado.usuario.nome,
                                    data.resultado.usuario.email, 
                                    null, 
                                    data.resultado.usuario.primeiroNome,
                                    data.resultado.role),
                        data.resultado.token);
        },
        error: (err) => {
          console.log(err);
          this.messageError = err.error.erroMensagens.map(erro => `-> ${erro.mensagem}`).join('\n');
          Security.clear();
        }
      });      
  }

  setUser(user: Usuario, token: string) {
    Security.set(user, token);
    this.messageError = "";
    this.router.navigate(['/app']);
  }
}
