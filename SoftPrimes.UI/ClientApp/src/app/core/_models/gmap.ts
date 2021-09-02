export interface Marker {
  lat: number;
  lng: number;
  label?: string;
  current?: Object;
  next?: Object[];
  draggable?: boolean;
}
