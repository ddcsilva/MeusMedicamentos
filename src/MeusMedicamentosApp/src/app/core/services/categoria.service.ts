import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Categoria, CriarCategoriaDTO, EditarCategoriaDTO } from '../models/categoria.model';

@Injectable({
    providedIn: 'root'
})
export class CategoriaService {
    private apiUrl = 'http://localhost:5233/api/categorias';

    constructor(private http: HttpClient) { }

    obterCategorias(): Observable<Categoria[]> {
        return this.http.get<{ data: Categoria[] }>(this.apiUrl).pipe(
            map(response => response.data)
        );
    }

    obterCategoriaPorId(id: string): Observable<Categoria> {
        return this.http.get<Categoria>(`${this.apiUrl}/${id}`);
    }

    criarCategoria(categoria: CriarCategoriaDTO): Observable<Categoria> {
        return this.http.post<Categoria>(this.apiUrl, categoria);
    }

    editarCategoria(id: string, categoria: EditarCategoriaDTO): Observable<Categoria> {
        return this.http.put<Categoria>(`${this.apiUrl}/${id}`, categoria);
    }

    removerCategoria(id: string): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}
