export enum ReservableObjectType {
  Desk = 0,
  ParkingSpace = 1
}

export interface ReservableObject {
  id: number;
  name: string;
  type: ReservableObjectType;
  isAvailable: boolean;
  locationId: number;
  positionX?: number | null;
  positionY?: number | null;
}
