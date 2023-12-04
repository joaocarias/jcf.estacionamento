import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, timer } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { Apiconfig } from 'src/app/Utils/apiconfig';

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {
  private apiUrl = Apiconfig.getApiUrl();

  constructor(private http: HttpClient) { }

  testConnection(): Observable<boolean> {
    const testUrl = `${this.apiUrl}/api/Estacionamento/TestarConexao`;  

    return this.http.get(testUrl).pipe(
      map(() => true),
      catchError(() => of(false))
    );
  }

  testConnectionPeriodically(intervalSeconds: number): Observable<boolean> {
    return timer(0, intervalSeconds * 1000).pipe(
      switchMap(() => this.testConnection())
    );
  }
}
