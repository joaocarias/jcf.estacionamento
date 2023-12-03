import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { DataService } from "../../../services/data/data.service";
import { FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: 'app-estacionamento-editar-page',
  templateUrl: './estacionamento-editar-page.component.html'
})
export class EstacionamentoEditarPageComponent implements OnInit {
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
  
        totalVagasMoto: [''],
  
        totalVagasCarro: [''],
  
        totalVagasGrandes: [''],

      });
 
}

  ngOnInit(): void {
    var id = this.activatedRouter.snapshot.paramMap.get("id");

    this
    .dataService
    .getEstacionamento(id)
    .subscribe({
      next: (data: any) => {

        var _estacionamento = data.resultado;

    this.form = this.fb.group({

        id: [_estacionamento.id],

        nome: [_estacionamento.nome, Validators.compose([
          Validators.minLength(5),
          Validators.maxLength(200),        
          Validators.required
        ])],
  
        totalVagasMoto: [_estacionamento.totalVagasMoto, Validators.compose([
          Validators.required,
          Validators.min(0),
          Validators.max(99)
        ])],
  
        totalVagasCarro: [_estacionamento.totalVagasCarro, Validators.compose([
          Validators.required, Validators.min(0),
          Validators.max(99)
        ])],
  
        totalVagasGrandes: [_estacionamento.totalVagasGrandes, Validators.compose([
          Validators.required, Validators.min(0),
          Validators.max(99)
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
        .putEstacionamento(this.form.value)
          .subscribe({
            next: (data: any) => {
              console.log(data);
              this.router.navigate(['/app/estacionamentos']);
            },
            error: (err) => {
              console.log(err);
            }
          });
  }  
}
