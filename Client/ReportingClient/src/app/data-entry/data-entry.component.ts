import { DataEntry } from './../models/data-entry';
import { Subscription } from 'rxjs';
import { DataEntryService } from './../services/data-entry.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { map, switchMap } from 'rxjs/operators';
import { AddDataEntryComponent } from '../add-data-entry/add-data-entry.component';
import { QuestionService } from '../services/question.service';
import { Question } from '../models/question';
import { EditDataEntryComponent } from '../edit-data-entry/edit-data-entry.component';

@Component({
  selector: 'data-entry',
  templateUrl: './data-entry.component.html',
  styleUrls: ['./data-entry.component.scss']
})
export class DataEntryComponent implements OnInit, OnDestroy {
  dataEntries: DataEntry[];
  subscription: Subscription;
  questions: Question[];
  deleteSubscription: Subscription;
  updateSubscription: Subscription;


  constructor(
    private auth: AuthService,
    private dataEntryService: DataEntryService,
    private modalService: NgbModal,
    private questionService: QuestionService

  ) { }

  createDataEntry() {
    const modalRef = this.modalService.open(AddDataEntryComponent, { backdrop: 'static' });
    modalRef.result.then((dataEntry: DataEntry) => {
      if (dataEntry) {
        this.dataEntries.push(dataEntry);
      }
    });
  }

  updateDataEntry(dataEntry: DataEntry) {
    // if (this.dataEntryForm.invalid) return;
    const modalRef = this.modalService.open(EditDataEntryComponent, { backdrop: 'static' });
    modalRef.componentInstance.dataEntry = dataEntry;
    modalRef.result.then((dataEntry: DataEntry) => {
      if (dataEntry) {
       //this.updateSubscription = this.dataEntryService.update(this.datantry.id, this.dataForm.value).subscribe((dataentry: DataEntry) => this.dataentry = dataentry);
      }
    });
   
  }

  deleteDataEntry(dataentry: DataEntry): void {
    if (!confirm('Are you sure you want to delete this certificate?')) {
      return;
    }
    this.deleteSubscription = this.dataEntryService.delete(dataentry.id)
      .subscribe(() => {
        const index = this.dataEntries.indexOf(dataentry);
        this.dataEntries.splice(index);
      });
  }


  ngOnInit() {
    this.subscription = this.dataEntryService.getAll()
      .pipe(
        map(dataEntries => {
          this.dataEntries = dataEntries;
        })
      )
      .subscribe();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
