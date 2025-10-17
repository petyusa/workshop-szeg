import { Injectable, computed, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReservableObject, ReservableObjectType } from '../models/reservable-object';

@Injectable({
  providedIn: 'root'
})
export class ReservableObjectService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5074';

  readonly objects = signal<ReservableObject[]>([]);
  readonly loading = signal(false);

  readonly desks = computed(() => 
    this.objects().filter(o => o.type === ReservableObjectType.Desk)
  );

  readonly parkingSpaces = computed(() => 
    this.objects().filter(o => o.type === ReservableObjectType.ParkingSpace)
  );

  readonly availableCount = computed(() => 
    this.objects().filter(o => o.isAvailable).length
  );

  readonly totalCount = computed(() => this.objects().length);

  async loadObjectsByLocation(locationId: number): Promise<void> {
    this.loading.set(true);
    try {
      const objects = await this.http
        .get<ReservableObject[]>(`${this.apiUrl}/api/locations/${locationId}/reservable-objects`)
        .toPromise();
      this.objects.set(objects || []);
    } catch (error) {
      console.error('Failed to load reservable objects:', error);
      this.objects.set([]);
    } finally {
      this.loading.set(false);
    }
  }

  clearObjects(): void {
    this.objects.set([]);
  }
}
