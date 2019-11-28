import { TestBed, async, inject } from '@angular/core/testing';

import { NonSupplierAuthGuard } from './non-supplier-auth.guard';

describe('NonSupplierAuthGuardGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NonSupplierAuthGuard]
    });
  });

  it('should ...', inject([NonSupplierAuthGuard], (guard: NonSupplierAuthGuard) => {
    expect(guard).toBeTruthy();
  }));
});
