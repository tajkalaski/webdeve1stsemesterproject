import { AuthService } from './../services/auth.service';
import { QuestionCategory } from './../models/question-category';
import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { AssessmentService } from '../services/assessment.service';
import { FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Company } from '../models/company';
import { QuestionCategoryService } from '../services/question-category.service';
import AssessmentQuestion from '../models/assessment-question';
import Assessment from '../models/assessment';

@Component({
  selector: 'create-assessment',
  templateUrl: './create-assessment.component.html',
  styleUrls: ['./create-assessment.component.scss']
})
export class CreateAssessmentComponent implements OnInit, OnDestroy {
  company: Company;
  assessments: Assessment[] = [];
  assessmentForm: FormGroup;
  questionCategories: QuestionCategory[] = [];
  createSubscription: Subscription;
  subscription: Subscription;

  constructor(
    private auth: AuthService,
    private assessmentService: AssessmentService,
    private questionCategoryService: QuestionCategoryService,
    private titleService: Title,
    private router: Router
  ) { }

  addAssessmentQuestion(assessmentQuestion: AssessmentQuestion): void {
    this.assessmentQuestions.push(new FormControl(assessmentQuestion, [Validators.required]));
  }

  removeAssessmentQuestion(assessmentQuestion: FormControl): void {
    const index = this.assessmentQuestions.controls.indexOf(assessmentQuestion);
    this.assessmentQuestions.removeAt(index);
  }

  createAssessment() {
    if (this.assessmentForm.invalid) {
      return;
    }
    this.assessmentForm.addControl('companyId', new FormControl(this.auth.user.companyId, Validators.required));
    for (const category of this.questionCategories) {
      for (const questionSubCategory of category.questionSubCategories) {
        for (const question of questionSubCategory.questions) {
          for (const subQuestion of question.subQuestions) {
            if (subQuestion.selected) {
              this.addAssessmentQuestion({
                id: null,
                subQuestionId: subQuestion.id,
                assessmentId: null,
                responsiblePerson: null
              });
            }
          }
        }
      }
    }
    this.createSubscription = this.assessmentService.create(this.assessmentForm.value)
      .subscribe(() => this.router.navigate(['/manage-assessments']));
  }

  cancel() {
    if (!confirm('Are you sure you want to cancel the changes?')) {
      return;
    }
    this.router.navigate(['/manage-assessments']);
  }

  ngOnInit() {
    this.titleService.setTitle('Respaunce | Create assessment');

    this.assessmentForm = new FormGroup({
      title: new FormControl('', Validators.required),
      assessmentQuestions: new FormArray([]),
    });

    this.subscription = this.questionCategoryService.getAll()
      .subscribe((questionCategories: QuestionCategory[]) => this.questionCategories = questionCategories);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    if (this.createSubscription) { this.createSubscription.unsubscribe(); }
  }

  get assessmentQuestions() {
    return this.assessmentForm.get('assessmentQuestions') as FormArray;
  }

  get title() {
    return this.assessmentForm.get('title') as FormControl;
  }
}
