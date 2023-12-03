import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { DataService } from "../../../services/data/data.service";
import { FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: 'app-usuario-editar-page',
  templateUrl: './usuario-editar-page.component.html'
})
export class UsuarioEditarPageComponent implements OnInit {
  public form: FormGroup;

  constructor(
    private router: Router,
    private dataService: DataService,
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
  ) {
    
    this.form = this.fb.group({

        id: [''],

        nome: [''],
  
        role: [''],
  
        email: [''],
  
      });
 
}

  ngOnInit(): void {
    var id = this.activatedRouter.snapshot.paramMap.get("id");

    this
    .dataService
    .getUsuario(id)
    .subscribe({
      next: (data: any) => {

        var _usuario = data.resultado;

    this.form = this.fb.group({

        id: [_usuario.id],

            nome: [_usuario.nome, Validators.compose([
              Validators.minLength(5),
              Validators.maxLength(200),        
              Validators.required
            ])],
      
            email: [_usuario.email, Validators.compose([
              Validators.required,
              Validators.email,
              Validators.min(3),
              Validators.max(255)
            ])],
      
            role: [_usuario.role, Validators.compose([
              Validators.required
            ])],
      
          });

    },
      error: (err) => {

      }
    });
  }
  
  submit(): void {
      this
        .dataService
        .putUsuario(this.form.value)
          .subscribe({
            next: (data: any) => {
              console.log(data);
              this.router.navigate(['/app/usuarios']);
            },
            error: (err) => {
              console.log(err);
            }
          });
  }  
}
