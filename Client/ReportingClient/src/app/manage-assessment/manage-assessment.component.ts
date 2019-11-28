import { SubQuestion } from './../models/sub-question';
import { UserService } from './../services/user.service';
import { Title } from '@angular/platform-browser';
import { AssessmentService } from './../services/assessment.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { Company } from '../models/company';
import { QuestionCategory } from '../models/question-category';
import { Subscription } from 'rxjs';
import { QuestionCategoryService } from '../services/question-category.service';
import { Router, ActivatedRoute } from '@angular/router';
import AssessmentQuestion from '../models/assessment-question';
import Assessment from '../models/assessment';
import { switchMap, map } from 'rxjs/operators';
import { AssessmentQuestionService } from '../services/assessment-question.service';
import { AuthService } from '../services/auth.service';
import { ApplicationUser } from '../models/application-user';

@Component({
  selector: 'manage-assessment',
  templateUrl: './manage-assessment.component.html',
  styleUrls: ['./manage-assessment.component.scss']
})
export class ManageAssessmentComponent implements OnInit, OnDestroy {
  id: string;
  company: Company;
  assessment: Assessment;
  users: ApplicationUser[];
  addedAssessmentQuestionResources: AssessmentQuestion[] = [];
  deletedAssessmentQuestionResources: AssessmentQuestion[] = [];
  assessmentForm: FormGroup;
  questionCategories: QuestionCategory[] = [];
  updateSubscription: Subscription;
  subscription: Subscription;

  constructor(
    private assessmentService: AssessmentService,
    private assessmentQuestionService: AssessmentQuestionService,
    private questionCategoryService: QuestionCategoryService,
    private userService: UserService,
    private titleService: Title,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  addAssessmentQuestion(assessmentQuestion: AssessmentQuestion): void {
    this.assessmentFormQuestions.push(new FormControl(assessmentQuestion, [Validators.required]));
  }

  removeAssessmentQuestion(assessmentQuestion: FormControl): void {
    const index = this.assessmentFormQuestions.controls.indexOf(assessmentQuestion);
    this.assessmentFormQuestions.removeAt(index);
  }

  updateAssessment() {
    if (this.assessment === this.assessmentForm.value) {
      return alert('You didn\'t make any changes.');
    }
    if (this.assessmentForm.invalid) { return alert('Fix errors'); }
    if (!confirm('Are you sure you want to save the changes?')) { // TO DO: Implement modal service to create visualy appealing pop-ups
      return;
    }

    if (this.assessment.assessmentQuestions.length === 0) {
      this.createAssessmentTemplate(
        this.questionCategories,
        this.assessmentFormQuestions,
        this.assessment,
        this.addedAssessmentQuestionResources
      );
    } else {
      if (this.assessment.assessmentQuestions !== this.assessmentFormQuestions.value) {
        this.modifyExistingAssessmentTemplate(
          this.questionCategories,
          this.assessmentFormQuestions,
          this.assessment,
          this.addedAssessmentQuestionResources,
          this.deletedAssessmentQuestionResources
        );
      }
    }

    if (this.addedAssessmentQuestionResources.length !== 0) {
      // tslint:disable-next-line: max-line-length
      this.updateSubscription = this.assessmentQuestionService.createMany(this.addedAssessmentQuestionResources)
        .pipe(
          switchMap(() => {
            this.addedAssessmentQuestionResources = [];
            return this.assessmentService.update(this.assessment.id, this.assessmentForm.value);
          }),
          map((assessment: Assessment) => {
            this.assessment = assessment;
            this.updateAssessmentTemplate(this.questionCategories, this.assessmentFormQuestions, this.assessment);
            console.log('Updated assessment: ', this.assessmentForm.value);
          })
        )
        .subscribe();
    }

    if (this.deletedAssessmentQuestionResources.length !== 0) {
      // tslint:disable-next-line: max-line-length
      this.updateSubscription = this.assessmentQuestionService.deleteMany(this.deletedAssessmentQuestionResources)
        .pipe(
          switchMap(() => {
            this.deletedAssessmentQuestionResources = [];
            return this.assessmentService.update(this.assessment.id, this.assessmentForm.value);
          }),
          map((assessment: Assessment) => {
            this.assessment = assessment;
            this.updateAssessmentTemplate(this.questionCategories, this.assessmentFormQuestions, this.assessment);
            console.log('Updated assessment: ', this.assessmentForm.value);
          })
        )
        .subscribe();
    }
    this.updateSubscription = this.assessmentService.update(this.assessment.id, this.assessmentForm.value)
      .subscribe();
  }

  cancel() {
    if (confirm('Are you sure you want to cancel the changes?')) { // TO DO: Implement modal service to create visually appealing pop-ups
      this.router.navigate(['/manage-assessments']);
    }
  }

  ngOnInit() {
    this.titleService.setTitle('Respaunce | Manage assessment');

    this.id = this.route.snapshot.paramMap.get('id');

    this.assessmentForm = new FormGroup({
      title: new FormControl('', Validators.required),
      assessmentQuestions: new FormArray([]),
    });

    this.subscription = this.questionCategoryService.getAll()
      .pipe(
        switchMap((questionCategories: QuestionCategory[]) => {
          this.questionCategories = questionCategories;
          return this.userService.getByCompany();
        }),
        switchMap((users: ApplicationUser[]) => {
          this.users = users;
          return this.assessmentService.getById(this.id);
        }),
        map((assessment: Assessment) => {
          this.assessment = assessment;
          this.selectSubQuestions(this.questionCategories, this.assessmentFormQuestions, this.assessment.assessmentQuestions);
          this.title.setValue(assessment.title);
        })
      )
      .subscribe();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    if (this.updateSubscription) { this.updateSubscription.unsubscribe(); }
  }

  get title() {
    return this.assessmentForm.get('title') as FormControl;
  }

  get assessmentFormQuestions() {
    return this.assessmentForm.get('assessmentQuestions') as FormArray;
  }
  approveResponsible(test) {
    console.log(test);
  }


  onResponsibleSelect(selectedResponsible: ApplicationUser, subQuestion: SubQuestion) {
  subQuestion.responsiblePerson = selectedResponsible;
}

  private selectSubQuestions(
    questionCategories: QuestionCategory[],
    questionsFormArray: FormArray,
    assessmentQuestions: AssessmentQuestion[]
  ): void {
    for (const category of questionCategories) {
      for (const questionSubCategory of category.questionSubCategories) {
        for (const question of questionSubCategory.questions) {
          for (const subQuestion of question.subQuestions) {
            for (const assessmentQuestion of assessmentQuestions) {
              if (subQuestion.id === assessmentQuestion.subQuestionId) {
                subQuestion.selected = true;
                const responsiblePerson = this.users.find(u => u.email === assessmentQuestion.responsiblePerson.email);
                if (responsiblePerson) {
                  subQuestion.responsiblePerson = responsiblePerson;
                }
                questionsFormArray.push(new FormControl(assessmentQuestion, [Validators.required]));
              }
            }
          }
        }
      }
    }
  }

  private createAssessmentTemplate(
    questionCategories: QuestionCategory[],
    assessmentQuestionsFormArray: FormArray,
    assessment: Assessment,
    adddedAssessmentQuestions: AssessmentQuestion[]
  ): void {
    for (const category of questionCategories) {
      for (const questionSubCategory of category.questionSubCategories) {
        for (const question of questionSubCategory.questions) {
          for (const subQuestion of question.subQuestions) {
            if (subQuestion.selected) {
              const newAssessmentQuestion: AssessmentQuestion = {
                id: null,
                subQuestionId: question.id,
                assessmentId: assessment.id,
                responsiblePerson: subQuestion.responsiblePerson
              };
              assessmentQuestionsFormArray.push(new FormControl(newAssessmentQuestion, [Validators.required]));
              adddedAssessmentQuestions.push(newAssessmentQuestion);
            }
          }
        }
      }
    }
  }

  private modifyExistingAssessmentTemplate(
    questionCategories: QuestionCategory[],
    assessmentQuestionsFormArray: FormArray,
    assessment: Assessment,
    addedAssessmentQuestions: AssessmentQuestion[],
    deletedAssessmentQuestions: AssessmentQuestion[]
  ): void {
    for (const category of questionCategories) {
      for (const questionSubCategory of category.questionSubCategories) {
        for (const question of questionSubCategory.questions) {
          for (const subQuestion of question.subQuestions) {
            const assessmentQuestion = assessment.assessmentQuestions.find(aq => aq.subQuestionId === subQuestion.id);
            if (subQuestion.selected) {
              if (!assessmentQuestion) {
                const newAssessmentQuestion: AssessmentQuestion = {
                  id: null,
                  subQuestionId: subQuestion.id,
                  assessmentId: assessment.id,
                  responsiblePerson: subQuestion.responsiblePerson
                };
                assessmentQuestionsFormArray.push(new FormControl(newAssessmentQuestion, [Validators.required]));
                addedAssessmentQuestions.push(newAssessmentQuestion);
              }
            } else {
              if (!!assessmentQuestion) {
                const assessmentQuestionControl = assessmentQuestionsFormArray.controls.find(
                  faq => faq.value.subQuestionId === subQuestion.id
                ) as FormControl;
                const index = assessmentQuestionsFormArray.controls.indexOf(assessmentQuestionControl);
                assessmentQuestionsFormArray.removeAt(index);
                deletedAssessmentQuestions.push(assessmentQuestionControl.value);
              }
            }
          }
        }
      }
    }
  }

  private updateAssessmentTemplate(
    questionCategories: QuestionCategory[],
    assessmentQuestionsFormArray: FormArray,
    assessment: Assessment
  ): void {
    for (const category of questionCategories) {
      for (const questionSubCategory of category.questionSubCategories) {
        for (const question of questionSubCategory.questions) {
          for (const subQuestion of question.subQuestions) {
            for (const assessmentQuestion of assessment.assessmentQuestions) {
              if (subQuestion.id === assessmentQuestion.subQuestionId) {
                subQuestion.selected = true;
                if (!assessmentQuestionsFormArray.controls.find(afq => afq.value.id === assessmentQuestion.id)) {
                  const assessmentQuestionFormControl = assessmentQuestionsFormArray.controls.find(
                    afq => afq.value.subQuestionId === assessmentQuestion.subQuestionId
                  );
                  assessmentQuestionFormControl.setValue(assessmentQuestion);
                }
              }
            }
          }
        }
      }
    }
  }
}

