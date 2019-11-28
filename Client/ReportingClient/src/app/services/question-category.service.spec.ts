import { TestBed } from '@angular/core/testing';

import { QuestionCategoryService } from './question-category.service';

describe('QuestionCategoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: QuestionCategoryService = TestBed.get(QuestionCategoryService);
    expect(service).toBeTruthy();
  });
});
