import { ChangeDetectionStrategy, Component, output, input, signal, computed } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReservableObject } from '../../models/reservable-object';

@Component({
  selector: 'app-reservation-dialog',
  imports: [FormsModule],
  templateUrl: './reservation-dialog.html',
  styleUrl: './reservation-dialog.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ReservationDialogComponent {
  readonly reservableObject = input.required<ReservableObject>();
  readonly close = output<void>();
  readonly reserve = output<{ startDateTime: string; endDateTime: string }>();

  protected readonly startDateTime = signal('');
  protected readonly endDateTime = signal('');
  protected readonly useDuration = signal(true);
  protected readonly durationHours = signal(1);
  protected readonly durationMinutes = signal(0);

  // Validation errors
  protected readonly startDateError = computed(() => {
    const start = this.startDateTime();
    if (!start) return '';

    const startDate = new Date(start);
    const now = new Date();
    
    if (isNaN(startDate.getTime())) return 'Invalid date/time';
    if (startDate < now) return 'Start time cannot be in the past';
    
    return '';
  });

  protected readonly endDateError = computed(() => {
    const start = this.startDateTime();
    const end = this.endDateTime();
    
    if (!start || !end) return '';

    const startDate = new Date(start);
    const endDate = new Date(end);
    
    if (isNaN(endDate.getTime())) return 'Invalid date/time';
    if (endDate <= startDate) return 'End time must be after start time';
    
    const diffMinutes = (endDate.getTime() - startDate.getTime()) / 60000;
    if (diffMinutes < 15) return 'Minimum duration is 15 minutes';
    if (diffMinutes > 1440) return 'Maximum duration is 24 hours';
    
    return '';
  });

  protected readonly durationError = computed(() => {
    if (!this.useDuration()) return '';
    
    const hours = this.durationHours();
    const minutes = this.durationMinutes();
    const totalMinutes = hours * 60 + minutes;
    
    if (totalMinutes < 15) return 'Minimum duration is 15 minutes';
    if (totalMinutes > 1440) return 'Maximum duration is 24 hours';
    
    return '';
  });

  protected readonly isFormValid = computed(() => {
    return this.startDateTime() &&
           this.endDateTime() &&
           !this.startDateError() &&
           !this.endDateError() &&
           !this.durationError();
  });

  protected onStartDateTimeChange(value: string) {
    this.startDateTime.set(value);
    if (this.useDuration() && value) {
      this.calculateEndDateTime();
    }
  }

  protected onDurationChange(hours: number | string, minutes: number | string) {
    const h = typeof hours === 'string' ? parseInt(hours) || 0 : hours;
    const m = typeof minutes === 'string' ? parseInt(minutes) || 0 : minutes;
    
    this.durationHours.set(h);
    this.durationMinutes.set(m);
    
    if (this.startDateTime()) {
      this.calculateEndDateTime();
    }
  }

  private calculateEndDateTime() {
    const start = new Date(this.startDateTime());
    if (isNaN(start.getTime())) return;

    const totalMinutes = this.durationHours() * 60 + this.durationMinutes();
    const end = new Date(start.getTime() + totalMinutes * 60000);
    
    // Format to datetime-local format
    const year = end.getFullYear();
    const month = String(end.getMonth() + 1).padStart(2, '0');
    const day = String(end.getDate()).padStart(2, '0');
    const hours = String(end.getHours()).padStart(2, '0');
    const minutes = String(end.getMinutes()).padStart(2, '0');
    
    this.endDateTime.set(`${year}-${month}-${day}T${hours}:${minutes}`);
  }

  protected onSubmit() {
    if (this.isFormValid()) {
      this.reserve.emit({
        startDateTime: this.startDateTime(),
        endDateTime: this.endDateTime()
      });
    }
  }

  protected onCancel() {
    this.close.emit();
  }
}
