import { TestBed } from '@angular/core/testing';

import { LegalFormService } from './legal-form.service';

describe('LegalFormService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LegalFormService = TestBed.get(LegalFormService);
    expect(service).toBeTruthy();
  });
});
