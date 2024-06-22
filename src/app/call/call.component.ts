import { Component, OnInit } from '@angular/core';
import { Call } from '../models/call.model';
import { CallService } from '../services/call.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CallOperationsComponent } from '../call-operations/call-operations.component';

@Component({
  selector: 'app-call',
  templateUrl: './call.component.html',
  styleUrl: './call.component.css'
})
export class CallComponent implements OnInit {

  calls: Call[] = [];
  filteredCalls: Call[] = [];

  displayedColumns: string[] = [
    'callId', 'callTitle', 'callDate', 'description', 'callType',
    'enteredBy', 'entryDate', 'lastModifiedBy', 'lastModifiedDate',
    'isCompleted', 'isIncoming', 'customerName',
    'employeeName', 'projectName' , 'actions'];

  constructor (private callService : CallService , private router : Router , private dialog: MatDialog){}
  
  ngOnInit(): void {
    this.loadCalls();
  }


loadCalls(): void {
  this.callService.getCalls().subscribe(
    (calls: Call[]) => {
      this.calls = calls;
    },
    error => {
      console.error('Error fetching calls', error);
    }
  );
}

reloadTable(): void {
  this.loadCalls();
}

openDialog(callId: number | null = null): void {
  const dialogRef = this.dialog.open(CallOperationsComponent, {
    width: '90%',
    height: '90%',
    maxWidth: '1200px',
    data: { callId: callId ?? null }
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result) {
      this.loadCalls();
    }
  });
}

applyFilter(event: Event): void {
  const filterValue = (event.target as HTMLInputElement).value.toLowerCase();
  this.filteredCalls = this.calls.filter(call =>
    Object.values(call).some(value =>
      value.toString().toLowerCase().includes(filterValue)
    )
  );
}

downloadCSV(): void {
  const csvData = this.convertToCSV(this.filteredCalls);
  const blob = new Blob([csvData], { type: 'text/csv' });
  const url = window.URL.createObjectURL(blob);
  const a = document.createElement('a');
  a.setAttribute('style', 'display:none');
  a.href = url;
  a.download = 'calls.csv';
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


editCall(callId: number): void {
  this.router.navigate(['/call', callId, 'edit']);
}

deleteCall(callId: number | undefined): void {
  if (!callId) {
    console.error('Invalid callId:', callId);
    return;
  }

  if (confirm('Are you sure you want to delete this call?')) {
    this.callService.deleteCall(callId).subscribe(
      () => {
        console.log('Call deleted successfully');
        this.loadCalls(); // Refresh calls after deletion
      },
      error => console.error('Error deleting call:', error)
    );
  }
}
}