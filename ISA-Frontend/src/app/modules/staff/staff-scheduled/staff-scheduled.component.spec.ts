import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffScheduledComponent } from './staff-scheduled.component';

describe('StaffScheduledComponent', () => {
  let component: StaffScheduledComponent;
  let fixture: ComponentFixture<StaffScheduledComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StaffScheduledComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StaffScheduledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
