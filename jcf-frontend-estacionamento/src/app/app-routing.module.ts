import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomePageComponent } from "./pages/home/home-page/home-page.component";
import { LoginPageComponent } from "./pages/account/login-page/login-page.component";
import { AuthService } from "./services/account/auth.service";
import { AppHomePageComponent } from "./pages/app-home/app-home-page/app-home-page.component";
import { EstacionamentoPageComponent } from "./pages/estacionamento/estacionamento-page/estacionamento-page.component";
import { EstacionamentoNovoPageComponent } from "./pages/estacionamento/estacionamento-novo-page/estacionamento-novo-page.component";
import { EstacionamentoDetalharPageComponent } from "./pages/estacionamento/estacionamento-detalhar-page/estacionamento-detalhar-page.component";
import { EstacionamentoEditarPageComponent } from "./pages/estacionamento/estacionamento-editar-page/estacionamento-editar-page.component";

const routes: Routes = [

  { path: '', component: HomePageComponent },
  { path: 'login', component: LoginPageComponent },

  { path: 'app',
    canActivate: [AuthService],
    children: [
      { path: '', component: AppHomePageComponent },
      { path: 'estacionamentos',
        children: [
          { path: '', component: EstacionamentoPageComponent },
          { path: 'novo', component: EstacionamentoNovoPageComponent },
          { path: ':id', component: EstacionamentoDetalharPageComponent },
          { path: 'editar/:id', component: EstacionamentoEditarPageComponent }
        ]
      }
    ]
  }
]

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }