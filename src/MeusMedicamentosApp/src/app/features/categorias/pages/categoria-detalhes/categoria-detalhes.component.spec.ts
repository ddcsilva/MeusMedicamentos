import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriaDetalhesComponent } from './categoria-detalhes.component';

describe('CategoriaDetalhesComponent', () => {
  let component: CategoriaDetalhesComponent;
  let fixture: ComponentFixture<CategoriaDetalhesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CategoriaDetalhesComponent]
    });
    fixture = TestBed.createComponent(CategoriaDetalhesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
