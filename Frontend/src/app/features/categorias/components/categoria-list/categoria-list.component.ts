import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { CategoriaService } from '../../services/categoria.service';
import { Categoria } from '../../models/categoria.model';
import { ApiResponse } from '../../../../core/models/api-response.model';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { CommonModule, NgIf } from '@angular/common';

@Component({
  selector: 'app-categoria-list',
  standalone: true,
  imports: [CommonModule, MatPaginatorModule, MatTableModule, MatCheckboxModule, MatButtonModule, MatCardModule, MatMenuModule, NgIf],
  templateUrl: './categoria-list.component.html',
  styleUrls: ['./categoria-list.component.scss']
})
export class CategoriaListComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['nome', 'status', 'action'];
  dataSource = new MatTableDataSource<Categoria>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private categoriaService: CategoriaService) { }

  ngOnInit(): void {
    this.carregarCategorias();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  carregarCategorias(): void {
    this.categoriaService.obterCategorias().subscribe((response: ApiResponse<Categoria[]>) => {
      if (response.success && response.data) {
        this.dataSource.data = response.data;
      } else {
        console.error('Erro ao carregar categorias:', response.errors);
      }
    });
  }

  aplicarFiltro(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
