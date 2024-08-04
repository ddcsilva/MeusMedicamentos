import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AutenticacaoService } from '../../services/autenticacao.service';
import { Router } from '@angular/router';
import { FeatherModule } from 'angular-feather';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatCheckboxModule, NgIf, FeatherModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  autenticacaoForm: FormGroup;
  escondido = true;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private autenticacaoService: AutenticacaoService
  ) {
    this.autenticacaoForm = this.fb.group({
      userName: ['', [Validators.required]],
      senha: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  onSubmit() {
    if (this.autenticacaoForm.valid) {
      this.autenticacaoService.efetuarLogin(this.autenticacaoForm.value).subscribe(
        response => {
          if (response.token) {
            this.autenticacaoService.definirToken(response.token);
            this.router.navigate(['/']);
          }
        },
        error => {
          console.error('Erro ao efetuar login', error);
        }
      );
    } else {
      console.log('Formulário inválido');
    }
  }
}
