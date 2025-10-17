import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReservableObjectService } from '../../services/reservable-object.service';
import { ReservableObject, ReservableObjectType } from '../../models/reservable-object';

@Component({
  selector: 'app-floor-plan',
  templateUrl: './floor-plan.html',
  styleUrls: ['./floor-plan.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule],
})
export class FloorPlanComponent {
  protected readonly objectService = inject(ReservableObjectService);
  protected readonly ReservableObjectType = ReservableObjectType;
  protected readonly selectedObject = signal<ReservableObject | null>(null);

  // Grid size for the floor plan (10x10 grid)
  protected readonly gridSize = 10;
  protected readonly cellSize = 60; // pixels

  protected selectObject(object: ReservableObject): void {
    this.selectedObject.set(object);
  }

  protected clearSelection(): void {
    this.selectedObject.set(null);
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

  protected getPositionedObjects(): ReservableObject[] {
    return this.objectService.objects().filter(obj => 
      obj.positionX != null && obj.positionY != null
    );
  }

  protected getObjectStyle(object: ReservableObject): any {
    if (object.positionX == null || object.positionY == null) {
      return {};
    }
    return {
      left: `${object.positionX * this.cellSize}px`,
      top: `${object.positionY * this.cellSize}px`,
    };
  }
}
