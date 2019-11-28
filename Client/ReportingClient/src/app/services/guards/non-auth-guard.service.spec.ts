import { TestBed } from '@angular/core/testing';

import { NonAuthGuard } from './non-auth-guard.service';

describe('NonAuthGuard', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NonAuthGuard = TestBed.get(NonAuthGuard);
    expect(service).toBeTruthy();
  });
});
