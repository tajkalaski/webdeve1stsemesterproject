import { AddCertificateComponent } from '../add-certificate/add-certificate.component';
import { ActivatedRoute } from '@angular/router';
import { CompanyCertificateService } from '../services/company-certificate.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { CompanyCertificate } from '../models/company-certificate';
import { map, switchMap } from 'rxjs/operators';
import { CompanyService } from '../services/company.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';

@Component({
  selector: 'manage-supplier-certificates',
  templateUrl: './manage-supplier-certificates.component.html',
  styleUrls: ['./manage-supplier-certificates.component.scss']
})
export class ManageSupplierCertificatesComponent implements OnInit, OnDestroy {
  supplierCertificates: CompanyCertificate[];
  id: string;
  supplierName: string;
  subscription: Subscription;
  deleteSubscription: Subscription;

  constructor(
    private companyCertificateService: CompanyCertificateService,
    private companyService: CompanyService,
    private modalService: NgbModal,
    private route: ActivatedRoute
  ) { }

  addCertificate() {
    const modalRef = this.modalService.open(AddCertificateComponent, { backdrop: 'static' });
    modalRef.result.then((certificate: CompanyCertificate) => {
      if (certificate) {
        this.supplierCertificates.push(certificate);
      }
    });
  }

  deleteCertificate(certificate: CompanyCertificate) {
    if (!confirm('Are you sure you want to delete this certificate?')) {
      return;
    }
    this.deleteSubscription = this.companyCertificateService.delete(certificate.id)
      .subscribe(() => {
        const index = this.supplierCertificates.indexOf(certificate);
        this.supplierCertificates.splice(index);
      });
  }


  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');

    this.subscription = this.companyCertificateService.getBySupplier(this.id)
      .pipe(
        switchMap(supplierCertificates => {
          this.supplierCertificates = supplierCertificates;
          return this.companyService.getById(this.id);
        }),
        map(supplier => this.supplierName = supplier.name)
      )
      .subscribe(() => console.log(this.supplierCertificates));
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
    if (this.deleteSubscription) { this.deleteSubscription.unsubscribe(); }
  }

}
