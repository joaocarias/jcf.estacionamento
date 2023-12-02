import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, timer } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {
  private apiUrl = 'https://localhost:7020';  

  constructor(private http: HttpClient) { }

    apiUrlDefault(): string {
        return this.apiUrl;
    }

  testConnection(): Observable<boolean> {
    const testUrl = `${this.apiUrl}/api/Home/TestarConexao`;  

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
