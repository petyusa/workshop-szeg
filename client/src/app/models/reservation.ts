export interface Reservation {
  id: number;
  reservableObjectId: number;
  objectName: string;
  objectType: string;
  startDateTime: string;
  endDateTime: string;
  createdAt: string;
}

export interface CreateReservationRequest {
  reservableObjectId: number;
  startDateTime: string;
  endDateTime: string;
}

export interface AvailabilityResponse {
  available: boolean;
}
