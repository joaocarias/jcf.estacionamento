import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HomePageComponent } from './pages/home/home-page/home-page.component';
import { HeaderDefaultComponent } from './components/shared/header-default/header-default.component';
import { SpinnerComponent } from './components/shared/spinner/spinner.component';
import { HttpClientModule } from '@angular/common/http';
import { ConnectionService } from './services/connection/connection.service';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    HeaderDefaultComponent,
    SpinnerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    ConnectionService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
