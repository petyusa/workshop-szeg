import { ChangeDetectionStrategy, Component, OnInit, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReservationService } from '../../services/reservation.service';
import { Reservation } from '../../models/reservation';

@Component({
  selector: 'app-my-reservations',
  imports: [CommonModule],
  templateUrl: './my-reservations.html',
  styleUrl: './my-reservations.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MyReservationsComponent implements OnInit {
  private readonly reservationService = inject(ReservationService);
  
  protected readonly reservations = signal<Reservation[]>([]);
  protected readonly loading = signal(false);
  protected readonly error = signal<string | null>(null);

  ngOnInit() {
    this.loadReservations();
  }

  private async loadReservations() {
    this.loading.set(true);
    this.error.set(null);

    try {
      const reservations = await this.reservationService.getMyReservations().toPromise();
      this.reservations.set(reservations || []);
    } catch (err) {
      this.error.set('Failed to load reservations');
      console.error('Error loading reservations:', err);
    } finally {
      this.loading.set(false);
    }
  }

  protected async deleteReservation(id: number) {
    if (!confirm('Are you sure you want to cancel this reservation?')) {
      return;
    }

    try {
      await this.reservationService.deleteReservation(id).toPromise();
      await this.loadReservations();
    } catch (err) {
      this.error.set('Failed to cancel reservation');
      console.error('Error cancelling reservation:', err);
    }
  }

  protected formatDateTime(dateStr: string): string {
    return new Date(dateStr).toLocaleString();
  }
}
