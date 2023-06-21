import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffToolbarComponent } from './staff-toolbar.component';

describe('StaffToolbarComponent', () => {
  let component: StaffToolbarComponent;
  let fixture: ComponentFixture<StaffToolbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StaffToolbarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StaffToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
