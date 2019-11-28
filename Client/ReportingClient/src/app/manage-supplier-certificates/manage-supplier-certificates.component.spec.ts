import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageSupplierCertificatesComponent as ManageSupplierCertificatesComponent } from './manage-supplier-certificates.component';

describe('ManageCertificatesComponent', () => {
  let component: ManageSupplierCertificatesComponent;
  let fixture: ComponentFixture<ManageSupplierCertificatesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageSupplierCertificatesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageSupplierCertificatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
