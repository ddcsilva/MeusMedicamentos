import { Route } from '@angular/router';
import { CategoriaListComponent } from './components/categoria-list/categoria-list.component';

export const categoriasRoutes: Route[] = [
    { path: '', component: CategoriaListComponent },
    // { path: 'detail/:id', component: CategoryDetailComponent },
    // { path: 'edit/:id', component: CategoryFormComponent },
    // { path: 'create', component: CategoryFormComponent }
];
