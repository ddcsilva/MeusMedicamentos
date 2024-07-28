import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriaCriarComponent } from './categoria-criar.component';

describe('CategoriaCriarComponent', () => {
  let component: CategoriaCriarComponent;
  let fixture: ComponentFixture<CategoriaCriarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CategoriaCriarComponent]
    });
    fixture = TestBed.createComponent(CategoriaCriarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
