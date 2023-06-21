import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffHomepageComponent } from './staff-homepage.component';

describe('StaffHomepageComponent', () => {
  let component: StaffHomepageComponent;
  let fixture: ComponentFixture<StaffHomepageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StaffHomepageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StaffHomepageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
