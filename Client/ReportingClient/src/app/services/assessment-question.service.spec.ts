import { TestBed } from '@angular/core/testing';

import { AssessmentQuestionService } from './assessment-question.service';

describe('AssessmentQuestionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AssessmentQuestionService = TestBed.get(AssessmentQuestionService);
    expect(service).toBeTruthy();
  });
});
