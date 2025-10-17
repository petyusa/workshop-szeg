import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Reservation, CreateReservationRequest, AvailabilityResponse } from '../models/reservation';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5074';

  createReservation(request: CreateReservationRequest): Observable<Reservation> {
    return this.http.post<Reservation>(`${this.apiUrl}/api/reservations`, request);
  }

  getMyReservations(): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(`${this.apiUrl}/api/reservations/my`);
  }

  checkAvailability(objectId: number, start: string, end: string): Observable<AvailabilityResponse> {
    return this.http.get<AvailabilityResponse>(
      `${this.apiUrl}/api/reservations/object/${objectId}/check?start=${start}&end=${end}`
    );
  }

  deleteReservation(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/reservations/${id}`);
  }
}
