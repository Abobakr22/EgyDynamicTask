import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerOperationsComponent } from './customer-operations.component';

describe('CustomerOperationsComponent', () => {
  let component: CustomerOperationsComponent;
  let fixture: ComponentFixture<CustomerOperationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CustomerOperationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomerOperationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
