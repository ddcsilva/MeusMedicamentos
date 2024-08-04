import { TestBed } from '@angular/core/testing';

import { AutenticacaoService } from './autenticacao.service';

describe('AutenticacaoService', () => {
  let autenticacaoService: AutenticacaoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    autenticacaoService = TestBed.inject(AutenticacaoService);
  });

  it('should be created', () => {
    expect(autenticacaoService).toBeTruthy();
  });
});
