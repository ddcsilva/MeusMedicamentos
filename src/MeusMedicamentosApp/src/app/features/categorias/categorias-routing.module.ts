// src/app/features/categorias/categorias-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriaListarComponent } from './pages/categoria-listar/categoria-listar.component';
import { CategoriaCriarComponent } from './pages/categoria-criar/categoria-criar.component';
import { CategoriaEditarComponent } from './pages/categoria-editar/categoria-editar.component';
import { CategoriaDetalhesComponent } from './pages/categoria-detalhes/categoria-detalhes.component';

const routes: Routes = [
  { path: '', component: CategoriaListarComponent },
  { path: 'criar', component: CategoriaCriarComponent },
  { path: 'editar/:id', component: CategoriaEditarComponent },
  { path: ':id', component: CategoriaDetalhesComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoriasRoutingModule { }
