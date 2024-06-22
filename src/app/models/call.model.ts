
export interface Call {
  callId?: number;
  callTitle: string;
  description?: string;
  callDate?: Date;
  callType?: string;
  enteredBy?: string;
  entryDate?: Date;
  lastModifiedBy?: string;
  lastModifiedDate?: Date;
  isCompleted?: boolean;
  isIncoming?: boolean;
  customerName?: string;
  employeeName?: string;
  projectName?: string;
  customerId: number;
  projectId: number;
  employeeId: number;
  }