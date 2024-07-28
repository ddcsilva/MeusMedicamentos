// src/app/features/categorias/categorias.module.ts
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { CategoriasRoutingModule } from './categorias-routing.module';
import { CategoriaListarComponent } from './pages/categoria-listar/categoria-listar.component';
import { CategoriaDetalhesComponent } from './pages/categoria-detalhes/categoria-detalhes.component';
import { CategoriaCriarComponent } from './pages/categoria-criar/categoria-criar.component';
import { CategoriaEditarComponent } from './pages/categoria-editar/categoria-editar.component';

@NgModule({
  declarations: [
    CategoriaListarComponent,
    CategoriaDetalhesComponent,
    CategoriaCriarComponent,
    CategoriaEditarComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CategoriasRoutingModule
  ]
})
export class CategoriasModule { }
