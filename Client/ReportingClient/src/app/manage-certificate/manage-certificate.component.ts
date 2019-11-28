import { Component, OnInit } from '@angular/core';
import { CompanyCertificateService } from '../services/company-certificate.service';
import { CompanyService } from '../services/company.service';
import { ActivatedRoute } from '@angular/router';
import { switchMap, map } from 'rxjs/operators';
import { CompanyCertificate } from '../models/company-certificate';

@Component({
  selector: 'manage-certificate',
  templateUrl: './manage-certificate.component.html',
  styleUrls: ['./manage-certificate.component.scss']
})
export class ManageCertificateComponent implements OnInit {
  companyCertificate: CompanyCertificate;
  id: string;

  constructor(
    private companyCertificateService: CompanyCertificateService,
    private companyService: CompanyService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');

    this.companyCertificateService.getById(this.id)
      .pipe(
        map(companyCertificate => {
          this.companyCertificate = companyCertificate;
        })
      )
      .subscribe(() => console.log(this.companyCertificate));
  }

}
