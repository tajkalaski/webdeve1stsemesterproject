import { ApplicationUser } from 'src/app/models/application-user';
import { AuthService } from './../services/auth.service';
import { Component, OnInit, Input } from '@angular/core';
import { SupplierService } from '../services/supplier.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Company } from '../models/company';
import { map, switchMap } from 'rxjs/operators';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';

@Component({
  selector: 'master-data-request',
  templateUrl: './master-data-request.component.html',
  styleUrls: ['./master-data-request.component.scss']
})
export class MasterDataRequestComponent implements OnInit {
  @Input() name: string;
  @Input() vatin: string;
  requestSupplierDataForm: FormGroup;

  constructor(
    private supplierService: SupplierService,
    private auth: AuthService,
    private router: Router,
    public modal: NgbActiveModal,
  ) { }

  createSupplierAndSupplierUser() {
    if (this.requestSupplierDataForm.invalid) { return; }
    // TO DO: Create CompanySupplier custom type
    this.supplierService.create({ name: this.supplierName.value, vatin: this.supplierVATIN.value })
      .pipe(
        switchMap(supplier => {
          return this.supplierService.createSupplierRelation({ companyId: this.auth.user.companyId, supplierId: supplier.id });
        }),
        switchMap((supplier: Company) => {
          // TO DO: Create CompanySupplierUser custom type
          return this.supplierService.createSupplierUser(supplier.id,
            { contactPersonEmail: this.email.value, language: '7cef445e-7fae-4514-b7fa-624982cfc130' }
          );
        })
      )
      .subscribe(() => {
        this.router.navigate(['/suppliers']);
      });
  }

  ngOnInit() {
    this.requestSupplierDataForm = new FormGroup({
      supplierName: new FormControl(this.name, Validators.required),
      supplierVATIN: new FormControl(this.vatin, Validators.required),
      email: new FormControl('', Validators.required),
    });
  }

  get supplierName() {
    return this.requestSupplierDataForm.get('supplierName') as FormControl;
  }

  get supplierVATIN() {
    return this.requestSupplierDataForm.get('supplierVATIN') as FormControl;
  }

  get email() {
    return this.requestSupplierDataForm.get('email') as FormControl;
  }
}
