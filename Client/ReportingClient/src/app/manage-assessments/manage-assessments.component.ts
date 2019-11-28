import { Observable, of, Subscription } from 'rxjs';
import { AuthService } from './../services/auth.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { AssessmentService } from '../services/assessment.service';
import { map, switchMap } from 'rxjs/operators';
import { Title } from '@angular/platform-browser';
import Assessment from '../models/assessment';

@Component({
  selector: 'manage-assessments',
  templateUrl: './manage-assessments.component.html',
  styleUrls: ['./manage-assessments.component.scss']
})
export class ManageAssessmentsComponent implements OnInit, OnDestroy {
  assessments: Assessment[] = []; // TO DO: Create assessment custom type
  subscription: Subscription;
  deleteSubscription: Subscription;

  constructor(
    private assessmentService: AssessmentService,
    private titleService: Title
  ) { }

  sortBy(key) {
    this.assessments.sort((n1, n2) => {
      if (n1[key] > n2[key]) return 1;
      if (n1[key] < n2[key]) return -1;
      return 0;
    });
  }

  deleteAssessment(assessment: Assessment) {
    confirm("Are you sure you want to delete " + assessment.title + "?");
    this.deleteSubscription = this.assessmentService.delete(assessment.id).subscribe(() => {
      let index = this.assessments.indexOf(assessment);
      this.assessments.splice(index, 1);
    });
  }

  ngOnInit() {
    this.titleService.setTitle("Respaunce | Manage assessments");

    this.subscription = this.assessmentService.getByCompany().subscribe(assessments => {
      this.assessments = assessments;
      //console.log("Assessments: ", this.assessments);
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    if (this.deleteSubscription) this.deleteSubscription.unsubscribe();
  }
}
