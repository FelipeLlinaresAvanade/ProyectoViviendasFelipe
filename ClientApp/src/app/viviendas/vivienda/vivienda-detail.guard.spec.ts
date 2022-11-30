import { TestBed } from '@angular/core/testing';

import { ViviendaDetailGuard } from './vivienda-detail.guard';

describe('ViviendaDetailGuard', () => {
  let guard: ViviendaDetailGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(ViviendaDetailGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
