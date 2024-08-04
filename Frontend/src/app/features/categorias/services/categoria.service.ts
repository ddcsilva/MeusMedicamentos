import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Categoria } from '../models/categoria.model';
import { ApiResponse } from '../../../core/models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {
  private urlApi = `${environment.urlApi}/categorias`;

  constructor(private http: HttpClient) { }

  obterCategorias(): Observable<ApiResponse<Categoria[]>> {
    return this.http.get<ApiResponse<Categoria[]>>(this.urlApi);
  }

  obterCategoriaPorId(id: string): Observable<ApiResponse<Categoria>> {
    return this.http.get<ApiResponse<Categoria>>(`${this.urlApi}/${id}`);
  }

  adicionarCategoria(categoria: any): Observable<ApiResponse<Categoria>> {
    return this.http.post<ApiResponse<Categoria>>(this.urlApi, categoria);
  }

  atualizarCategoria(id: string, categoria: any): Observable<ApiResponse<Categoria>> {
    return this.http.put<ApiResponse<Categoria>>(`${this.urlApi}/${id}`, categoria);
  }

  removerCategoria(id: string): Observable<ApiResponse<any>> {
    return this.http.delete<ApiResponse<any>>(`${this.urlApi}/${id}`);
  }
}
