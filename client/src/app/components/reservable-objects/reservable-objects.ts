import { ChangeDetectionStrategy, Component, effect, inject, signal } from '@angular/core';
import { LocationService } from '../../services/location.service';
import { ReservableObjectService } from '../../services/reservable-object.service';
import { ReservableObjectType } from '../../models/reservable-object';
import { FloorPlanComponent } from '../floor-plan/floor-plan';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reservable-objects',
  templateUrl: './reservable-objects.html',
  styleUrls: ['./reservable-objects.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, FloorPlanComponent],
})
export class ReservableObjectsComponent {
  protected readonly locationService = inject(LocationService);
  protected readonly objectService = inject(ReservableObjectService);
  protected readonly ReservableObjectType = ReservableObjectType;
  protected readonly viewMode = signal<'list' | 'floorplan'>('list');

  constructor() {
    effect(() => {
      const locationId = this.locationService.selectedLocationId();
      if (locationId !== null) {
        this.objectService.loadObjectsByLocation(locationId);
      } else {
        this.objectService.clearObjects();
      }
    });
  }

  protected getTypeLabel(type: ReservableObjectType): string {
    return type === ReservableObjectType.Desk ? 'Desk' : 'Parking Space';
  }

  protected getTypeIcon(type: ReservableObjectType): string {
    if (type === ReservableObjectType.Desk) {
      return 'M3 7v10a2 2 0 002 2h14a2 2 0 002-2V9a2 2 0 00-2-2h-6l-2-2H5a2 2 0 00-2 2z';
    }
    return 'M19 17h2c.6 0 1-.4 1-1v-3c0-.9-.7-1.7-1.5-1.9C18.7 10.6 16 10 16 10s-1.3-1.4-2.2-2.3c-.5-.4-1.1-.7-1.8-.7H5c-.6 0-1.1.4-1.4.9l-1.4 2.9A3.7 3.7 0 002 12v4c0 .6.4 1 1 1h2';
  }

  protected toggleView(mode: 'list' | 'floorplan'): void {
    this.viewMode.set(mode);
  }

  protected hasFloorPlan(): boolean {
    return this.objectService.objects().some(obj => obj.positionX != null && obj.positionY != null);
  }
}
