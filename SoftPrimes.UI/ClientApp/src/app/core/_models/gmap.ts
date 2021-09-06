export interface Marker {
  lat: number;
  lng: number;
  label?: string;
  current?: Object;
  next?: Object[];
  draggable?: boolean;
}

export interface MapSettings {
  zoom?: number;
  zoomControl?: boolean;
  lat: number;
  lng: number;
}
