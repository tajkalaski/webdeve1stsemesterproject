import { TestBed } from '@angular/core/testing';

import { QuestionSubcategoryService } from './question-subcategory.service';

describe('QuestionSubcategoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: QuestionSubcategoryService = TestBed.get(QuestionSubcategoryService);
    expect(service).toBeTruthy();
  });
});
