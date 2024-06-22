import { Component, Inject, OnInit } from '@angular/core';
import { Customer } from '../models/customer.model';
import { CustomerService } from '../services/customer.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-customer-operations',
  templateUrl: './customer-operations.component.html',
  styleUrl: './customer-operations.component.css'
})
export class CustomerOperationsComponent implements OnInit {

  customerForm: FormGroup;
  isEditMode = false;
  customerId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router : Router,
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<CustomerOperationsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { customerId: number | null }
  ) {
    this.customerForm = this.createForm();
  }

  ngOnInit(): void {
    if (this.data.customerId !== null && this.data.customerId !== undefined) {
      this.isEditMode = true;
      this.customerId = this.data.customerId;
      this.loadCustomer(this.customerId);
    }
  }

  createForm(): FormGroup {
    return this.fb.group({
      customerName: ['', Validators.required],
      residence: [''],
      job: [''],
      description: [''],
      nationality: [''],
      customerAddress: [''],
      customerClassification: [''],
      customerCode: [''],
      customerSource: [''],
      enteredBy: [''],
      enteredDate: [new Date()],
      lastModifiedBy: [''],
      lastModifiedDate: [new Date()],
      firstPhone: [''],
      secondaryPhone: [''],
      whatsApp: [''],
      email: [''],
      salesmanId: ['', Validators.required]
    });
  }

  loadCustomer(id: number): void {
    this.http.get<Customer>(`https://localhost:7272/api/Customers/${id}`).subscribe(
      customer => this.customerForm.patchValue(customer),
      error => console.error('Error loading customer', error)
    );
  }

  saveCustomer(): void {
    if (this.customerForm.valid) {
      const customerData = this.customerForm.value as Customer;

      if (this.isEditMode && this.customerId !== null) {
        this.updateCustomer(this.customerId, customerData);
      } else {
        this.createCustomer(customerData);
      }
    } else {
      console.log('Form is invalid');
    }
  }

  createCustomer(customer: Customer): void {
    this.http.post<Customer>('https://localhost:7272/api/Customers', customer)
      .subscribe(
        response => {
          console.log('Customer created successfully:', response);
          this.snackBar.open('تم حفظ العميل بنجاح', 'إغلاق', { duration: 15000 });
          this.dialogRef.close(true);
        },
        error => {
          console.error('Error creating customer:', error);
        }
      );
  }

  updateCustomer(id: number, customer: Customer): void {
    this.http.put<Customer>(`https://localhost:7272/api/Customers/${id}`, customer)
      .subscribe(
        response => {
          console.log('Customer updated successfully:', response);
          this.snackBar.open('تم تحديث العميل بنجاح', 'إغلاق', { duration: 15000 }); 
          this.dialogRef.close(true);
        },
        error => {
          console.error('Error updating customer:', error);
        }
      );
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}