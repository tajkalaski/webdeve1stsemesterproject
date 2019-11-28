import { MasterDataRequestComponent } from './../master-data-request/master-data-request.component';
import { SupplierService } from './../services/supplier.service';
import { TypeOfOwnershipService } from './../services/type-of-ownership.service';
import { LegalFormService } from './../services/legal-form.service';
import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Company } from '../models/company';
import { Title } from '@angular/platform-browser';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { switchMap, map } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'create-supplier',
  templateUrl: './create-supplier.component.html',
  styleUrls: ['./create-supplier.component.scss']
})
export class CreateSupplierComponent implements OnInit, OnDestroy {
  supplier: Company;
  subscription: Subscription;
  supplierForm: FormGroup;
  legalForms: any[] = []; // TO DO: Create legal form custom type
  typesOfOwnership: any[] = []; // TO DO: Create type of ownership custom type

  constructor(
    private supplierService: SupplierService,
    private legalFormService: LegalFormService,
    private typeOfOwnershipService: TypeOfOwnershipService,
    private modalService: NgbModal,
    private router: Router,
    private titleService: Title,
  ) { }

  requestDataFromSupplier() {
    const modalRef = this.modalService.open(MasterDataRequestComponent, { backdrop: 'static' } );
    modalRef.componentInstance.name = this.name.value;
    modalRef.componentInstance.vatin = this.vatin.value;
  }

  createSupplier() {
    if (this.supplierForm.invalid) { return; }
    this.supplierForm.value.legalFormId = this.legalForm.value.id;
    this.supplierForm.value.typeOfOwnershipId = this.typeOfOwnership.value.id;
    this.supplierService.create(this.supplierForm.value)
      .subscribe((supplier: Company) => {
        this.router.navigate(['/suppliers', supplier.id]);
      });
  }

  cancel() {
    if (!confirm('Are you sure you want to cancel the changes?')) {
      return;
    }
    this.router.navigate(['/suppliers']);
  }

  ngOnInit() {
    this.titleService.setTitle('Respaunce | Create supplier');

    this.supplierForm = new FormGroup({
      name: new FormControl('', Validators.required),
      vatin: new FormControl('', Validators.required),
      address: new FormControl('', Validators.required),
      postalCode: new FormControl('', Validators.required),
      city: new FormControl('', Validators.required),
      country: new FormControl('', Validators.required),
      legalFormId: new FormControl('', Validators.required),
      typeOfOwnershipId: new FormControl('', Validators.required),
      financialYearStart: new FormControl('', Validators.required),
      financialYearEnd: new FormControl('', Validators.required),
      annualRevenue: new FormControl('', Validators.required),
      employees: new FormControl('', Validators.required),
    });

    this.subscription = this.legalFormService.getAll()
      .pipe(
        switchMap(forms => {
          this.legalForms = forms;
          this.legalForm.setValue(this.legalForms.find(form => form.id === ''));
          return this.typeOfOwnershipService.getAll();
        }),
        map(types => {
          this.typesOfOwnership = types;
          this.typeOfOwnership.setValue(this.typesOfOwnership.find(type => type.id === ''));
        })
      )
      .subscribe();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  get name() {
    return this.supplierForm.get('name') as FormControl;
  }

  get vatin() {
    return this.supplierForm.get('vatin') as FormControl;
  }

  get address() {
    return this.supplierForm.get('address') as FormControl;
  }

  get postalCode() {
    return this.supplierForm.get('postalCode') as FormControl;
  }

  get city() {
    return this.supplierForm.get('city') as FormControl;
  }

  get country() {
    return this.supplierForm.get('country') as FormControl;
  }

  get legalForm() {
    return this.supplierForm.get('legalFormId') as FormControl;
  }

  get typeOfOwnership() {
    return this.supplierForm.get('typeOfOwnershipId') as FormControl;
  }

  get financialYearStart() {
    return this.supplierForm.get('financialYearStart') as FormControl;
  }

  get financialYearEnd() {
    return this.supplierForm.get('financialYearEnd') as FormControl;
  }

  get annualRevenue() {
    return this.supplierForm.get('annualRevenue') as FormControl;
  }

  get employees() {
    return this.supplierForm.get('employees') as FormControl;
  }
}
