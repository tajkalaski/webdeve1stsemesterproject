import { SupplierService } from './../services/supplier.service';
import { TypeOfOwnershipService } from './../services/type-of-ownership.service';
import { LegalFormService } from './../services/legal-form.service';
import { Subscription } from 'rxjs';
import { CompanyService } from './../services/company.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Company } from '../models/company';
import { Title } from '@angular/platform-browser';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { switchMap, map } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { MasterDataRequestComponent } from '../master-data-request/master-data-request.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'manage-supplier',
  templateUrl: './manage-supplier.component.html',
  styleUrls: ['./manage-supplier.component.scss']
})
export class ManageSupplierComponent implements OnInit, OnDestroy {
  supplier: Company;
  subscription: Subscription;
  updateSubscription: Subscription;
  supplierForm: FormGroup;
  legalForms: any[] = []; // TO DO: Create legal form custom type
  typesOfOwnership: any[] = []; // TO DO: Create type of ownership custom type
  id: string;

  constructor(
    private supplierService: SupplierService,
    private legalFormService: LegalFormService,
    private typeOfOwnershipService: TypeOfOwnershipService,
    private modalService: NgbModal,
    private router: Router,
    private route: ActivatedRoute,
    private titleService: Title,
    private companyService: CompanyService
  ) { }

  requestDataFromSupplier() {
    const modalRef = this.modalService.open(MasterDataRequestComponent, { backdrop: 'static' } );
    modalRef.componentInstance.name = this.name.value;
    modalRef.componentInstance.vatin = this.vatin.value;
  }

  updateSupplier() {
    if (this.supplierForm.invalid) { return; }
    this.supplierForm.value.legalFormId = this.legalForm.value.id;
    this.supplierForm.value.typeOfOwnershipId = this.typeOfOwnership.value.id;
    this.updateSubscription = this.companyService.update(this.supplier.id, this.supplierForm.value)
      .subscribe((supplier: Company) => this.supplier = supplier);
  }

  cancel() {
    if (confirm('Are you sure you want to cancel the changes?')) { this.router.navigate(['/suppliers']); }
    this.name.setValue(this.supplier.name);
    this.vatin.setValue(this.supplier.vatin);
    this.address.setValue(this.supplier.address);
    this.postalCode.setValue(this.supplier.postalCode);
    this.city.setValue(this.supplier.city);
    this.country.setValue(this.supplier.country);
    this.financialYearStart.setValue(this.supplier.financialYearStart);
    this.financialYearEnd.setValue(this.supplier.financialYearEnd);
    this.annualRevenue.setValue(this.supplier.annualRevenue);
    this.employees.setValue(this.supplier.employees);
    this.legalForm.setValue(this.legalForms.find(form => form.id === this.supplier.legalFormId));
    this.typeOfOwnership.setValue(this.typesOfOwnership.find(type => type.id === this.supplier.typeOfOwnershipId));
  }

  ngOnInit() {
    this.titleService.setTitle('Respaunce | Supplier profile');

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

    this.id = this.route.snapshot.paramMap.get('id');

    this.subscription = this.supplierService.getById(this.id)
      .pipe(
        switchMap((supplier: Company) => {
          this.supplier = supplier;
          this.name.setValue(this.supplier.name);
          this.vatin.setValue(this.supplier.vatin);
          this.address.setValue(this.supplier.address);
          this.postalCode.setValue(this.supplier.postalCode);
          this.city.setValue(this.supplier.city);
          this.country.setValue(this.supplier.country);
          this.financialYearStart.setValue(this.supplier.financialYearStart);
          this.financialYearEnd.setValue(this.supplier.financialYearEnd);
          this.annualRevenue.setValue(this.supplier.annualRevenue);
          this.employees.setValue(this.supplier.employees);
          return this.legalFormService.getAll();
        }),
        switchMap(forms => {
          this.legalForms = forms;
          this.legalForm.setValue(this.legalForms.find(form => form.id === this.supplier.legalFormId));
          return this.typeOfOwnershipService.getAll();
        }),
        map(types => {
          this.typesOfOwnership = types;
          this.typeOfOwnership.setValue(this.typesOfOwnership.find(type => type.id === this.supplier.typeOfOwnershipId));
        })
      )
      .subscribe();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    if (this.updateSubscription) { this.updateSubscription.unsubscribe(); }
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
