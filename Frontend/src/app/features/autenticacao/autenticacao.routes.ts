import { Route } from '@angular/router';
import {SignInComponent} from "../../authentication/sign-in/sign-in.component";
export const authRoutes: Route[] = [
    { path: 'sign-in', component: SignInComponent },
    // Outros caminhos relacionados à autenticação
];
