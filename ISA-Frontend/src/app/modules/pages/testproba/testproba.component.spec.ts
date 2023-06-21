import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestprobaComponent } from './testproba.component';

describe('TestprobaComponent', () => {
  let component: TestprobaComponent;
  let fixture: ComponentFixture<TestprobaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestprobaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestprobaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
