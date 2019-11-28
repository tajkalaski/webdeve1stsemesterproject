import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilityToolsComponent } from './utility-tools.component';

describe('UtilityToolsComponent', () => {
  let component: UtilityToolsComponent;
  let fixture: ComponentFixture<UtilityToolsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UtilityToolsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UtilityToolsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
