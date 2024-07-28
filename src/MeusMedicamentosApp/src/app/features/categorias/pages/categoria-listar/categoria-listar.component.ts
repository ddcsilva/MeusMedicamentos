import { Component, OnInit } from '@angular/core';
import { Categoria } from 'src/app/core/models/categoria.model';
import { CategoriaService } from 'src/app/core/services/categoria.service';

@Component({
  selector: 'app-categoria-listar',
  templateUrl: './categoria-listar.component.html',
  styleUrls: ['./categoria-listar.component.scss']
})
export class CategoriaListarComponent implements OnInit {
  categorias: Categoria[] = [];
  errorMessage: string | null = null;

  constructor(private categoriaService: CategoriaService) { }

  ngOnInit(): void {
    this.categoriaService.obterCategorias().subscribe({
      next: (categorias: Categoria[]) => {
        console.log('Categorias retornadas:', categorias); // Adicione este log
        this.categorias = categorias;
      },
      error: (err) => {
        console.error('Erro ao carregar categorias', err);
        this.errorMessage = 'Erro ao carregar categorias';
      }
    });
  }
}
