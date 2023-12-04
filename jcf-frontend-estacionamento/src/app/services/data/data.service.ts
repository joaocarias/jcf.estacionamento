import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Estacionamento } from "src/app/Models/estacionamento.model";
import { Usuario } from "src/app/Models/usuario.model";
import { Apiconfig } from "src/app/Utils/apiconfig";
import { Security } from "src/app/Utils/security";

@Injectable({
  providedIn: 'root'
})
export class DataService {
  public urlApi = Apiconfig.getApiUrl()+"/api";

  constructor(private http: HttpClient) {

  }

  public composeHeaders() {
    const token = Security.getToken();
    const headers = new HttpHeaders().set('Authorization', `bearer ${token}`);
    return headers;
  }

  authenticate(data: any) {
    return this.http.post(`${this.urlApi}/Usuario/Login`, data);
  }

  getEstacionamentos() : Observable<Array<Estacionamento>> {
    return this.http.get<Array<Estacionamento>>(`${this.urlApi}/Estacionamento`, { headers: this.composeHeaders() });
  }

  postEstacionamento(data: any){
    return this.http.post(`${this.urlApi}/Estacionamento`, data, { headers: this.composeHeaders() });
  }

  getEstacionamento (id: any) {   
    return this.http.get<Estacionamento>(`${this.urlApi}/Estacionamento/${id}`, { headers: this.composeHeaders() });
  }

  deleteEstacionamento(id: any){
    return this.http.delete(`${this.urlApi}/Estacionamento/${id}`, { headers: this.composeHeaders() });
  }

  putEstacionamento(data: any) {
    return this.http.put(`${this.urlApi}/Estacionamento/${data.id}`, data, { headers: this.composeHeaders() });
  }

  getUsuarios() {
    return this.http.get<Array<Usuario>>(`${this.urlApi}/Usuario`, { headers: this.composeHeaders() });
  }

  postUsuario(data: any){
    return this.http.post(`${this.urlApi}/Usuario`, data, { headers: this.composeHeaders() });
  }

  getUsuario (id: any) {   
    return this.http.get<Usuario>(`${this.urlApi}/Usuario/${id}`, { headers: this.composeHeaders() });
  }

  deleteUsuario(id: any){
    return this.http.delete(`${this.urlApi}/Usuario/${id}`, { headers: this.composeHeaders() });
  }

  putUsuario(data: any) {
    return this.http.put(`${this.urlApi}/Usuario/${data.id}`, data, { headers: this.composeHeaders() });
  }

  postEstacionar(data: any){
    return this.http.post(`${this.urlApi}/EstacionamentoVeiculo/Estacionar`, data, { headers: this.composeHeaders() });
  }

  deleteRemoverEstacionar(id: any) {
    return this.http.delete(`${this.urlApi}/EstacionamentoVeiculo/Remover/${id}`, { headers: this.composeHeaders() });
  }
}
