import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Estacionamento } from "src/app/Models/estacionamento.model";
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

  getEstacionamentos() {
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
}
