import { QuestionService } from './../services/question.service';
import { QuestionSubcategoryService } from './../services/question-subcategory.service';
import { DataEntry } from './../models/data-entry';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { DataEntryService } from '../services/data-entry.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { map, switchMap } from 'rxjs/operators';
import { SubQuestion } from '../models/sub-question';
import { Question } from '../models/question';
import { Subscription } from 'rxjs';

@Component({
  selector: 'edit-data-entry',
  templateUrl: './edit-data-entry.component.html',
  styleUrls: ['./edit-data-entry.component.scss']
})
export class EditDataEntryComponent implements OnInit, OnDestroy{
  @Input() dataEntry: DataEntry;
  editDataForm: FormGroup;
  questions: Question[];
  subscription: Subscription;

  constructor(

    private dataEntryService: DataEntryService,
    private questionService: QuestionService,
    private modal: NgbActiveModal,
  ) { }

  editCertificate() {
    if (this.editDataForm.invalid) { alert('Fix errors'); }
    this.dataEntryService.create(this.editDataForm.value)
      .subscribe(() => this.modal.close(this.editDataForm.value));
  }

  ngOnInit() {
    this.editDataForm = new FormGroup({
      dataId: new FormControl(undefined, Validators.required),
      dataFrom: new FormControl(this.dataEntry.from, Validators.required),
      dataTo: new FormControl(this.dataEntry.to, Validators.required),
      dataValue: new FormControl(this.dataEntry.value),
      dataUnit: new FormControl(undefined)
    });

    this.subscription = this.questionService.getAll()
      .pipe(
        map((questions: Question[]) => {
          this.questions = questions;
          console.log(questions);
        } )
      )
      .subscribe();
  }

  dismissModal() {
    this.modal.dismiss();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
