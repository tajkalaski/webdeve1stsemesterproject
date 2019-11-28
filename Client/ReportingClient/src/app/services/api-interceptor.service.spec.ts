import { TestBed } from '@angular/core/testing';

import { APIInterceptorService } from './apiinterceptor.service';

describe('APIInterceptorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: APIInterceptorService = TestBed.get(APIInterceptorService);
    expect(service).toBeTruthy();
  });
});
