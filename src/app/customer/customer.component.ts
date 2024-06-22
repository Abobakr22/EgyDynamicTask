import { Component, OnInit } from '@angular/core';
import { Customer } from '../models/customer.model';
import { CustomerService } from '../services/customer.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CustomerOperationsComponent } from '../customer-operations/customer-operations.component';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent implements OnInit{

  customers: Customer[] = [];
  filteredCustomers: Customer[] = [];

  displayedColumns: string[] = [
    'customerId', 'customerName', 'residence', 'description', 'job',
    'enteredBy', 'enteredDate', 'lastModifiedBy', 'lastModifiedDate',
    'customerSource', 'customerClassification', 'customerAddress',
    'firstPhone', 'secondaryPhone', 'whatsApp', 'email', 'customerCode',
    'nationality', 'salesManName', 'actions'
  ];


constructor (private customerService: CustomerService , private router : Router , private dialog: MatDialog){
}

ngOnInit(): void {
  this.loadCustomers();
}

loadCustomers(): void {
  this.customerService.getCustomers().subscribe(
    (customers: Customer[]) => {
      this.customers = customers;
    },
    error => {
      console.error('Error fetching customers', error);
    }
  );
}

openDialog(customerId?: number): void {
  const dialogRef = this.dialog.open(CustomerOperationsComponent, {
    width: '90%',
    height: '90%',
    maxWidth: '1200px',
    data: { customerId: customerId ?? null }
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result) {
      this.loadCustomers();
    }
  });
}

reloadTable(): void {
  this.loadCustomers();
}

applyFilter(event: Event): void {
  const filterValue = (event.target as HTMLInputElement).value.toLowerCase();
  this.filteredCustomers = this.customers.filter(customer =>
    Object.values(customer).some(value =>
      value.toString().toLowerCase().includes(filterValue)
    )
  );
}

downloadCSV(): void {
  const csvData = this.convertToCSV(this.filteredCustomers);
  const blob = new Blob([csvData], { type: 'text/csv' });
  const url = window.URL.createObjectURL(blob);
  const a = document.createElement('a');
  a.setAttribute('style', 'display:none');
  a.href = url;
  a.download = 'customers.csv';
  document.body.appendChild(a);
  a.click();
}

convertToCSV(data: any[]): string {
  const headers = Object.keys(data[0]);
  const csvRows = [];
  csvRows.push(headers.join(','));
  for (const row of data) {
    const values = headers.map(header => row[header]);
    csvRows.push(values.join(','));
  }
  return csvRows.join('\n');
}


// toggleColumnsDialog(): void {
//   const dialogRef = this.dialog.open(ToggleColumnsDialogComponent, {
//     width: '250px',
//     data: { displayedColumns: this.displayedColumns }
//   });

//   dialogRef.afterClosed().subscribe(result => {
//     if (result) {
//       this.displayedColumns = result;
//     }
//   });
// }


editCustomer(customerId: number): void {
  this.router.navigate(['/customer', customerId, 'edit']);
}

deleteCustomer(customerId: number): void {
  if (!customerId) {
    console.error('Invalid customerId:', customerId);
    return;
  }
  if (confirm('Are you sure you want to delete this customer?')) {
  this.customerService.deleteCustomer(customerId).subscribe(
    () => {
      console.log('Customer deleted successfully');
      this.loadCustomers(); 
    },
    error => {
      console.log('Error deleting customer', error);
    }
  );
}
}
}
