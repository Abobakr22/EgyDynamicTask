import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CallService } from '../services/call.service';
import { Call } from '../models/call.model';

@Component({
  selector: 'app-call-operations',
  templateUrl: './call-operations.component.html',
  styleUrl: './call-operations.component.css'
})
export class CallOperationsComponent implements OnInit{

  callForm: FormGroup;
  isEditMode = false;
  callId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private callService : CallService,
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<CallOperationsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { callId: number | null }
  ) {
    this.callForm = this.createForm();
  }

  ngOnInit(): void {
    if (this.data.callId !== null && this.data.callId !== undefined) {
      this.isEditMode = true;
      this.callId = this.data.callId;
      this.loadCall(this.callId);
    } else {
      console.log('Create mode enabled');
    }
  }

  createForm(): FormGroup {
    return this.fb.group({
      callTitle: ['', Validators.required],
      description: [''],
      callDate: [new Date()],
      callType: [''],
      enteredBy: [''],
      entryDate: [new Date()],
      lastModifiedBy: [''],
      lastModifiedDate: [new Date()],
      isCompleted: [false],
      isIncoming: [false],
      customerId: [null],
      projectId: [null],
      employeeId: [null]
    });
  }

  loadCall(id: number): void {
    this.callService.getCall(id).subscribe(
      call => {
        this.callForm.patchValue(call);
        console.log('Call loaded:', call);
      },
      error => {
        console.error('Error loading call', error);
        this.snackBar.open('حدث خطأ أثناء تحميل المكالمة', 'إغلاق', { duration: 15000 });
      }
    );
  }

  saveCall(): void {
    if (this.callForm.valid) {
      const callData = this.callForm.value as Call;

      if (this.isEditMode && this.callId !== null) {
        this.updateCall(this.callId, callData);
      } else {
        this.createCall(callData);
      }
    } else {
      console.log('Form is invalid');
    }
  }

  createCall(call: Call): void {
    this.callService.createCall(call)
      .subscribe(
        response => {
          console.log('Call created successfully:', response);
          this.snackBar.open('تم حفظ المكالمة بنجاح', 'إغلاق', { duration: 15000 });
          this.dialogRef.close(true);
        },
        error => {
          console.error('Error creating call:', error);
        }
      );
  }

  updateCall(id: number, call: Call): void {
    this.callService.updateCall(id, call)
      .subscribe(
        response => {
          console.log('Call updated successfully:', response);
          this.snackBar.open('تم تحديث المكالمة بنجاح', 'إغلاق', { duration: 15000 });
          this.dialogRef.close(true);
        },
        error => {
          console.error('Error updating call:', error);
        }
      );
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}