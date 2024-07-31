import { Route } from '@angular/router';
import { CategoriaListComponent } from './components/categoria-list/categoria-list.component';
import { CategoriaCreateComponent } from './categoria-create/categoria-create.component';

export const categoriasRoutes: Route[] = [
    { path: '', component: CategoriaListComponent },
    // { path: 'detail/:id', component: CategoryDetailComponent },
    // { path: 'edit/:id', component: CategoryFormComponent },
    { path: 'criar', component: CategoriaCreateComponent }
];
