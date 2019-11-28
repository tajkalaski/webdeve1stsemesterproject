import { TestBed } from '@angular/core/testing';

import { TypeOfOwnershipService } from './type-of-ownership.service';

describe('TypeOfOwnershipService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TypeOfOwnershipService = TestBed.get(TypeOfOwnershipService);
    expect(service).toBeTruthy();
  });
});
