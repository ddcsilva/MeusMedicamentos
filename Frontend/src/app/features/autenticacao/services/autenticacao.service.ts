import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Login } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AutenticacaoService {
  private urlApi = `${environment.urlApi}/usuarios`;

  constructor(private http: HttpClient) { }

  efetuarLogin(login: Login): Observable<any> {
    return this.http.post<any>(`${this.urlApi}/login`, login);
  }

  definirToken(token: string): void {
    localStorage.setItem('jwtToken', token);
  }

  obterToken(): string | null {
    return localStorage.getItem('jwtToken');
  }

  limparToken(): void {
    localStorage.removeItem('jwtToken');
  }
}
