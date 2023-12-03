import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { DataService } from "../../../services/data/data.service";
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: 'app-estacionamento-novo-page',
  templateUrl: './estacionamento-novo-page.component.html'
})
export class EstacionamentoNovoPageComponent implements OnInit {
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

      totalVagasMoto: ['', Validators.compose([
        Validators.required,
        Validators.min(0),
        Validators.max(99)
      ])],

      totalVagasCarro: ['', Validators.compose([
        Validators.required, Validators.min(0),
        Validators.max(99)
      ])],

      totalVagasGrandes: ['', Validators.compose([
        Validators.required, Validators.min(0),
        Validators.max(99)
      ])],

    });
  }

  ngOnInit(): void {
    
  }
  
  submit(): void {
      this
        .dataService
        .postEstacionamento(this.form.value)
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
