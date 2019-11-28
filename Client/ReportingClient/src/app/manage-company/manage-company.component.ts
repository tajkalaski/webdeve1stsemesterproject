import { TypeOfOwnershipService } from './../services/type-of-ownership.service';
import { LegalFormService } from './../services/legal-form.service';
import { ApplicationUser } from './../models/application-user';
import { AuthService } from './../services/auth.service';
import { Subscription } from 'rxjs';
import { CompanyService } from './../services/company.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Company } from '../models/company';
import { Title } from '@angular/platform-browser';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { switchMap, map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'manage-company',
  templateUrl: './manage-company.component.html',
  styleUrls: ['./manage-company.component.scss']
})
export class ManageCompanyComponent implements OnInit, OnDestroy {
  company: Company;
  subscription: Subscription;
  updateSubscription: Subscription;
  companyForm: FormGroup;
  legalForms: any[] = []; // TO DO: Create legal form custom type
  typesOfOwnership: any[] = []; // TO DO: Create type of ownership custom type

  constructor(
    private auth: AuthService,
    private legalFormService: LegalFormService,
    private typeOfOwnershipService: TypeOfOwnershipService,
    private router: Router,
    private titleService: Title,
    private companyService: CompanyService
  ) { }

  updateCompany() {
    if (this.companyForm.invalid) return;
    this.companyForm.value.legalFormId = this.legalForm.value.id
    this.companyForm.value.typeOfOwnershipId = this.typeOfOwnership.value.id
    this.updateSubscription = this.companyService.update(this.company.id, this.companyForm.value).subscribe((company: Company) => this.company = company);
  }

  cancel() {
    if (!confirm("Are you sure you want to cancel the changes?")) return;
    this.name.setValue(this.company.name);
    this.vatin.setValue(this.company.vatin);
    this.address.setValue(this.company.address);
    this.postalCode.setValue(this.company.postalCode);
    this.city.setValue(this.company.city);
    this.country.setValue(this.company.country);
    this.financialYearStart.setValue(this.company.financialYearStart);
    this.financialYearEnd.setValue(this.company.financialYearEnd);
    this.annualRevenue.setValue(this.company.annualRevenue);
    this.employees.setValue(this.company.employees);
    this.legalForm.setValue(this.legalForms.find(form => form.id == this.company.legalFormId));
    this.typeOfOwnership.setValue(this.typesOfOwnership.find(type => type.id == this.company.typeOfOwnershipId));
  }

  ngOnInit() {
    this.titleService.setTitle("Respaunce | Company profile");

    this.companyForm = new FormGroup({
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

    this.subscription = this.auth.user$
      .pipe(
        switchMap((user: ApplicationUser) => this.companyService.getById(user.companyId)),
        switchMap((company: Company) => {
          this.company = company;
          this.name.setValue(this.company.name);
          this.vatin.setValue(this.company.vatin);
          this.address.setValue(this.company.address);
          this.postalCode.setValue(this.company.postalCode);
          this.city.setValue(this.company.city);
          this.country.setValue(this.company.country);
          this.financialYearStart.setValue(this.company.financialYearStart);
          this.financialYearEnd.setValue(this.company.financialYearEnd);
          this.annualRevenue.setValue(this.company.annualRevenue);
          this.employees.setValue(this.company.employees);
          return this.legalFormService.getAll();
        }),
        switchMap(forms => {
          this.legalForms = forms;
          this.legalForm.setValue(this.legalForms.find(form => form.id == this.company.legalFormId));
          return this.typeOfOwnershipService.getAll();
        }),
        map(types => {
          this.typesOfOwnership = types;
          this.typeOfOwnership.setValue(this.typesOfOwnership.find(type => type.id == this.company.typeOfOwnershipId));
        })
      )
      .subscribe();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    if (this.updateSubscription) this.updateSubscription.unsubscribe();
  }

  get name() {
    return this.companyForm.get('name') as FormControl;
  }

  get vatin() {
    return this.companyForm.get('vatin') as FormControl;
  }

  get address() {
    return this.companyForm.get('address') as FormControl;
  }

  get postalCode() {
    return this.companyForm.get('postalCode') as FormControl;
  }

  get city() {
    return this.companyForm.get('city') as FormControl;
  }

  get country() {
    return this.companyForm.get('country') as FormControl;
  }

  get legalForm() {
    return this.companyForm.get('legalFormId') as FormControl;
  }

  get typeOfOwnership() {
    return this.companyForm.get('typeOfOwnershipId') as FormControl;
  }

  get financialYearStart() {
    return this.companyForm.get('financialYearStart') as FormControl;
  }

  get financialYearEnd() {
    return this.companyForm.get('financialYearEnd') as FormControl;
  }

  get annualRevenue() {
    return this.companyForm.get('annualRevenue') as FormControl;
  }

  get employees() {
    return this.companyForm.get('employees') as FormControl;
  }
}
