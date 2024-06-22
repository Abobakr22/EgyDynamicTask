import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CallOperationsComponent } from './call-operations.component';

describe('CallOperationsComponent', () => {
  let component: CallOperationsComponent;
  let fixture: ComponentFixture<CallOperationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CallOperationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CallOperationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
