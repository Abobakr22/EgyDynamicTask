import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CustomerComponent } from './customer/customer.component';
import { CallComponent } from './call/call.component';
import { CustomerOperationsComponent } from './customer-operations/customer-operations.component';
import { CallOperationsComponent } from './call-operations/call-operations.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  
  { path: 'customer', component: CustomerComponent },
  { path: 'customer/new', component: CustomerOperationsComponent }, 
  { path: 'customer/:id/edit', component: CustomerOperationsComponent },

  { path: 'call', component: CallComponent },
  { path: 'call/new', component: CallOperationsComponent }, 
  { path: 'call/:id/edit', component: CallOperationsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
