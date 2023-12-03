import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HomePageComponent } from './pages/home/home-page/home-page.component';
import { HeaderDefaultComponent } from './components/shared/header-default/header-default.component';
import { SpinnerComponent } from './components/shared/spinner/spinner.component';
import { HttpClientModule } from '@angular/common/http';
import { ConnectionService } from './services/connection/connection.service';
import { LoginPageComponent } from './pages/account/login-page/login-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppHomePageComponent } from './pages/app-home/app-home-page/app-home-page.component';
import { AuthService } from './services/account/auth.service';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { EstacionamentoMsgNovoComponent } from './pages/estacionamento/estacionamento-msg-novo/estacionamento-msg-novo.component';
import { EstacionamentoPageComponent } from './pages/estacionamento/estacionamento-page/estacionamento-page.component';
import { EstacionamentoNovoPageComponent } from './pages/estacionamento/estacionamento-novo-page/estacionamento-novo-page.component';
import { EstacionamentoDetalharPageComponent } from './pages/estacionamento/estacionamento-detalhar-page/estacionamento-detalhar-page.component';
import { EstacionamentoEditarPageComponent } from './pages/estacionamento/estacionamento-editar-page/estacionamento-editar-page.component';
import { UsuarioDetalharPageComponent } from './pages/usuario/usuario-detalhar-page/usuario-detalhar-page.component';
import { UsuarioEditarPageComponent } from './pages/usuario/usuario-editar-page/usuario-editar-page.component';
import { UsuarioNovoPageComponent } from './pages/usuario/usuario-novo-page/usuario-novo-page.component';
import { UsuarioPageComponent } from './pages/usuario/usuario-page/usuario-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    HeaderDefaultComponent,
    SpinnerComponent,
    LoginPageComponent,
    AppHomePageComponent,
    NavbarComponent,
    EstacionamentoMsgNovoComponent,
    EstacionamentoPageComponent,
    EstacionamentoNovoPageComponent,
    EstacionamentoDetalharPageComponent,
    EstacionamentoEditarPageComponent,
    UsuarioDetalharPageComponent,
    UsuarioEditarPageComponent,
    UsuarioNovoPageComponent,
    UsuarioPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    ConnectionService,
    AuthService 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
