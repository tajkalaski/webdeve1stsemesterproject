import { ActivatedRoute } from '@angular/router';
import { CertificateService } from './../services/certificate.service';
import { CompanyCertificateService } from './../services/company-certificate.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Certificate } from '../models/certificate';
import { map } from 'rxjs/operators';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'add-certificate',
  templateUrl: './add-certificate.component.html',
  styleUrls: ['./add-certificate.component.scss']
})
export class AddCertificateComponent implements OnInit {
  addCertificateForm: FormGroup;
  certificates: Certificate[];

  constructor(
    private companyCertificateService: CompanyCertificateService,
    private certificateService: CertificateService,
    private modal: NgbActiveModal
  ) { }

  createCertificate() {
    if (this.addCertificateForm.invalid) { alert('Fix errors'); }
    this.companyCertificateService.create(this.addCertificateForm.value)
      .subscribe(() => this.modal.close(this.addCertificateForm.value));
  }

  dismissModal() {
    this.modal.dismiss();
  }

  ngOnInit() {
    this.addCertificateForm = new FormGroup({
      certificateId: new FormControl(undefined, Validators.required),
      certifiedFrom: new FormControl(new Date().toISOString().substring(0, new Date().toISOString().lastIndexOf('T')), Validators.required),
      certifiedTo: new FormControl(undefined),
      overallRating: new FormControl(undefined),
    });

    this.certificateService.getAll()
    .pipe(
      map((certificates: Certificate[]) => this.certificates = certificates)
    )
    .subscribe();
  }

  get certificateId() {
    return this.addCertificateForm.get('certificateId') as FormControl;
  }

  get certifiedFrom() {
    return this.addCertificateForm.get('certifiedFrom') as FormControl;
  }

  get certifiedTo() {
    return this.addCertificateForm.get('certifiedTo') as FormControl;
  }

  get overallRating() {
    return this.addCertificateForm.get('overallRating') as FormControl;
  }

  get unit() {
    return null;
  }
}
