import { Component, Inject, Injectable, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatLegacyDialogRef as MatDialogRef, MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/legacy-dialog';
import * as moment from 'moment';
import { Observable } from 'rxjs';
import { range } from 'rxjs';
import { Staff } from '../../../model/staff.model';
import { StaffService } from '../../../services/staff.service';

@Component({
  selector:'app-appointment-dialog',
  templateUrl: './appointment-dialog.component.html',
  styleUrls: ['./appointment-dialog.component.css']
})
export class AppointmentDialogComponent implements OnInit {

  form!: FormGroup;
  minDate = new Date();
  allStaff: Staff[] = [];
  numbers: number[] = [];
  start: number = 0;
  end: number = 0;

  constructor(private dialogRef: MatDialogRef<AppointmentDialogComponent>, @Inject(MAT_DIALOG_DATA) data : any, private staffService: StaffService, private fb: FormBuilder) {
    this.start = data.start;
    this.end = data.end;
   
  }

  ngOnInit(): void {
   
    for (let i = 15; i <= 60; i++){ this.numbers.push(i); }

    this.staffService.getAll().subscribe(res => {
      this.allStaff = res;
    });

    this.form = this.fb.group({
      dateTime: [null],
      duration: [''],
      staff: [0]
    });
  }

  public save() {
    if (this.form?.value.dateTime.hour() > this.start && this.form?.value.dateTime.hour() < this.end) {
      this.dialogRef.close(this.form?.value);
    }
  }

  public close() {
    this.dialogRef.close();
  }
}
