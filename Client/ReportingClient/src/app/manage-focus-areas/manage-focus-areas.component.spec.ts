import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageFocusAreasComponent } from './manage-focus-areas.component';

describe('ManageFocusAreasComponent', () => {
  let component: ManageFocusAreasComponent;
  let fixture: ComponentFixture<ManageFocusAreasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageFocusAreasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageFocusAreasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
