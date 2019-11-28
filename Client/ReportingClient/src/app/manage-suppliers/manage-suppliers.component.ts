import { Subscription } from 'rxjs';
import { SupplierService } from './../services/supplier.service';
import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Company } from '../models/company';
import { map } from 'rxjs/operators';

@Component({
  selector: 'manage-suppliers',
  templateUrl: './manage-suppliers.component.html',
  styleUrls: ['./manage-suppliers.component.scss']
})
export class ManageSuppliersComponent implements OnInit {
  suppliers: Company[];
  subscription: Subscription;

  constructor(
    private auth: AuthService,
    private supplierService: SupplierService,
    private router: Router
  ) { }

  deleteSupplier(supplier: Company) {

  }

  ngOnInit() {
    this.subscription = this.supplierService.getByCompanyId(this.auth.user.companyId)
      .pipe(
        map((suppliers: Company[]) => this.suppliers = suppliers)
        )
      .subscribe();
  }

}
