import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MasterDataRequestComponent } from './master-data-request.component';

describe('MasterDataRequestComponent', () => {
  let component: MasterDataRequestComponent;
  let fixture: ComponentFixture<MasterDataRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MasterDataRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MasterDataRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
