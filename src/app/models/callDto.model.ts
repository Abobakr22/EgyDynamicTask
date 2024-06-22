export interface CallDto {
    callTitle: string;
    description?: string;
    callDate?: Date;
    callType?: string;
    isCompleted?: boolean;
    isIncoming?: boolean;
    enteredBy?: string;
    entryDate?: Date;
    lastModifiedBy?: string;
    lastModifiedDate?: Date;
    customerId?: number;
    projectId?: number;
    employeeId?: number;
  }