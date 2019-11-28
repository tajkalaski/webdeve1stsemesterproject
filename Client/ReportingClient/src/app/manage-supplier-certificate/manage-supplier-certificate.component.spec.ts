import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageSupplierCertificateComponent } from './manage-supplier-certificate.component';

describe('ManageSupplierCertificateComponent', () => {
  let component: ManageSupplierCertificateComponent;
  let fixture: ComponentFixture<ManageSupplierCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageSupplierCertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageSupplierCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
