import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { DataService } from "../../../services/data/data.service";
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: 'app-usuario-novo-page',
  templateUrl: './usuario-novo-page.component.html'
})
export class UsuarioNovoPageComponent implements OnInit {
  public form: FormGroup;

  constructor(
    private router: Router,
    private dataService: DataService,
    private fb: FormBuilder
  ) {

    this.form = this.fb.group({

      nome: ['', Validators.compose([
        Validators.minLength(5),
        Validators.maxLength(200),        
        Validators.required
      ])],

      email: ['', Validators.compose([
        Validators.required,
        Validators.email,
        Validators.min(3),
        Validators.max(255)
      ])],

      role: ['', Validators.compose([
        Validators.required
      ])],

    });
  }

  ngOnInit(): void {
    
  }
  
  submit(): void {
      this
        .dataService
        .postUsuario(this.form.value)
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
