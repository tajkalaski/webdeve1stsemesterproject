import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageComplianceComponent } from './manage-compliance.component';

describe('ManageComplianceComponent', () => {
  let component: ManageComplianceComponent;
  let fixture: ComponentFixture<ManageComplianceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageComplianceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageComplianceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
