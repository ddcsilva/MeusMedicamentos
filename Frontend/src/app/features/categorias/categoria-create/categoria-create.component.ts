import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoriaService } from '../services/categoria.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FeathericonsModule } from '../../../icons/feathericons/feathericons.module';

@Component({
  selector: 'app-categoria-create',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule, MatIconModule, FeathericonsModule],
  templateUrl: './categoria-create.component.html',
  styleUrl: './categoria-create.component.scss'
})
export class CategoriaCreateComponent implements OnInit {
  categoriaForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private categoriaService: CategoriaService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.categoriaForm = this.fb.group({
      nome: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.categoriaForm.valid) {
      const novaCategoria = {
        nome: this.categoriaForm.value.nome
      };
      this.categoriaService.adicionarCategoria(novaCategoria).subscribe({
        next: () => this.router.navigate(['/categorias']),
        error: (err) => console.error('Erro ao criar categoria', err)
      });
    }
  }

  cancelar(): void {
    this.router.navigate(['/categorias']);
  }
}
