import { QuestionService } from './../services/question.service';
import { QuestionSubcategoryService } from './../services/question-subcategory.service';
import { DataEntry } from './../models/data-entry';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { DataEntryService } from '../services/data-entry.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { map, switchMap } from 'rxjs/operators';
import { SubQuestion } from '../models/sub-question';
import { Question } from '../models/question';
import { Subscription } from 'rxjs';

@Component({
  selector: 'add-data-entry',
  templateUrl: './add-data-entry.component.html',
  styleUrls: ['./add-data-entry.component.scss']
})
export class AddDataEntryComponent implements OnInit, OnDestroy{
  
  dataEntries: DataEntry[];
  addDataForm: FormGroup;
  questions: Question[];
  subscription: Subscription;

  constructor(

    private dataEntryService: DataEntryService,
    private questionService: QuestionService,
    private modal: NgbActiveModal,
  ) { }

  createCertificate() {
    if (this.addDataForm.invalid) { alert('Fix errors'); }
    this.dataEntryService.create(this.addDataForm.value)
     .subscribe(() => this.modal.close(this.addDataForm.value));
      console.log(this.addDataForm.value);
  }

  ngOnInit() {
    this.addDataForm = new FormGroup({
      dataId: new FormControl(undefined, Validators.required),
      from: new FormControl(new Date().toISOString().substring(0, new Date().toISOString().lastIndexOf('T')), Validators.required),
      to: new FormControl(new Date().toISOString().substring(0, new Date().toISOString().lastIndexOf('T')), Validators.required),
      value: new FormControl(undefined),
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
