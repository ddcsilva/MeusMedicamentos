import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriaListarComponent } from './categoria-listar.component';

describe('CategoriaListarComponent', () => {
  let component: CategoriaListarComponent;
  let fixture: ComponentFixture<CategoriaListarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CategoriaListarComponent]
    });
    fixture = TestBed.createComponent(CategoriaListarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
