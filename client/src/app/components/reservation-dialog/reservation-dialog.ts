import { ChangeDetectionStrategy, Component, output, input, signal } from '@angular/core';
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

  protected onSubmit() {
    if (this.startDateTime() && this.endDateTime()) {
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
