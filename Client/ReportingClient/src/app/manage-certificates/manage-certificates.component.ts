import { Component, OnInit, OnDestroy } from '@angular/core';
import { CompanyCertificate } from '../models/company-certificate';
import { Subscription } from 'rxjs';
import { CompanyCertificateService } from '../services/company-certificate.service';
import { CompanyService } from '../services/company.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddCertificateComponent } from '../add-certificate/add-certificate.component';
import { switchMap, map } from 'rxjs/operators';

@Component({
  selector: 'manage-certificates',
  templateUrl: './manage-certificates.component.html',
  styleUrls: ['./manage-certificates.component.scss']
})
export class ManageCertificatesComponent implements OnInit, OnDestroy {
  certificates: CompanyCertificate[];
  subscription: Subscription;
  deleteSubscription: Subscription;

  constructor(
    private companyCertificateService: CompanyCertificateService,
    private companyService: CompanyService,
    private modalService: NgbModal,
  ) { }

  addCertificate() {
    const modalRef = this.modalService.open(AddCertificateComponent, { backdrop: 'static' });
    modalRef.result.then((certificate: CompanyCertificate) => {
      if (certificate) {
        this.certificates.push(certificate);
      }
    });
  }

  deleteCertificate(certificate: CompanyCertificate): void {
    if (!confirm('Are you sure you want to delete this certificate?')) {
      return;
    }
    this.deleteSubscription = this.companyCertificateService.delete(certificate.id)
      .subscribe(() => {
        const index = this.certificates.indexOf(certificate);
        this.certificates.splice(index);
      });
  }

  ngOnInit() {
    this.subscription = this.companyCertificateService.getByCompany()
      .pipe(
        map(supplierCertificates => {
          this.certificates = supplierCertificates;
        })
      )
      .subscribe();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    if (this.deleteSubscription) { this.deleteSubscription.unsubscribe(); }
  }
}
