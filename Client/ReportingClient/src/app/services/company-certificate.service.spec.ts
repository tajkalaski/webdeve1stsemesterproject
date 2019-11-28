import { TestBed } from '@angular/core/testing';

import { CompanyCertificateService } from './company-certificate.service';

describe('CompanyCertificateService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CompanyCertificateService = TestBed.get(CompanyCertificateService);
    expect(service).toBeTruthy();
  });
});
