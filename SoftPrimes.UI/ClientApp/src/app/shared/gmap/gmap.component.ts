import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { environment } from 'src/environments/environment';


export interface Marker {
  lat: number;
  lng: number;
  label?: string;
  draggable?: boolean;
}

@Component({
  selector: 'app-gmap',
  templateUrl: './gmap.component.html',
  styleUrls: ['./gmap.component.css']
})
export class GmapComponent implements OnInit {
  // google maps zoom level
  zoom = 12;

  googleApiKey = environment['google-api-key'];
  @Input() viewMode: boolean;
  @Output() markerAdded: EventEmitter<Marker> = new EventEmitter();

  // initial center position for the map
  lat = 24.701284088932535;
  lng = 46.680371285791246;

  @Input() markers = [];

  constructor() {
  }

  ngOnInit() {
    if (this.viewMode) {
      this.markers.push(...[
        {
          lat: 24.712200401496062,
          lng: 46.81564045083031,
          label: 'Kareem AbdelRahaman',
          draggable: true
        },
        {
          lat: 24.647936354282542,
          lng: 46.705090524072496,
          label: 'Ahmed Hussein',
          draggable: false
        },
        {
          lat: 24.71874972966019,
          lng: 46.61582660805687,
          label: 'Hossam Fathy',
          draggable: true
        }
      ]);
    }
  }

  openInFullscreen(el) {
     // if already full screen; exit
    // else go fullscreen
    if (
      document.fullscreenElement
      // ||
      // document.webkitFullscreenElement ||
      // document.mozFullScreenElement ||
      // document.msFullscreenElement
    ) {
      if (document.exitFullscreen) {
        document.exitFullscreen();
        el.classList.remove('full-screen');
      }
      // else if (document.mozCancelFullScreen) {
      //   document.mozCancelFullScreen();
      // } else if (document.webkitExitFullscreen) {
      //   document.webkitExitFullscreen();
      // } else if (document.msExitFullscreen) {
      //   document.msExitFullscreen();
      // }
    } else {
      const element: HTMLElement = el;
      element.classList.add('full-screen');
      if (element.requestFullscreen) {
        element.requestFullscreen();
      }
      // else if (element.mozRequestFullScreen) {
      //   element.mozRequestFullScreen();
      // } else if (element.webkitRequestFullscreen) {
      //   // element.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
      // } else if (element.msRequestFullscreen) {
      //   element.msRequestFullscreen();
      // }
    }
  }

  clickedMarker(label: string, index: number) {
    console.log(`clicked the marker: ${label || index}`);
  }

  getPlaceDetails(id) {
    const proxyurl = 'https://cors-anywhere.herokuapp.com/';
    const url = `https://maps.googleapis.com/maps/api/place/details/json?place_id=${id}&fields=name,rating,formatted_phone_number&key=${this.googleApiKey}`;
    fetch(url, {
      method: 'get',
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'POST, GET, OPTIONS',
        'Access-Control-Allow-Headers': 'X-PINGOTHER, Content-Type'
      }
    })
    .then(response => response.json())
    .then(res => {
      console.log(res, 'place details');
    });
  }

  addMark($event) {
    console.log($event, 'mapClicked');
    if ($event.placeId) {
      // this.getPlaceDetails($event.placeId);
    }
    if (!this.viewMode) {
      this.markers = [{
        lat: $event.coords.lat,
        lng: $event.coords.lng,
        label: '',
        draggable: true
      }];

      this.markerAdded.emit({
        lat: $event.coords.lat,
        lng: $event.coords.lng,
        draggable: true
      });
    }
    const elms = document.getElementsByClassName('map-agent-details');
    for (const key in elms) {
      if (Object.prototype.hasOwnProperty.call(elms, key)) {
        const element = elms[key];
        element.classList.remove('show-details');
      }
    }
  }

  markerDragEnd(m: Marker, $event: MouseEvent) {
    console.log('dragEnd', m, $event);
  }

  showDetails(elm: HTMLElement) {
    elm.classList.add('show-details');
  }

  toggleLabel(elm: HTMLElement) {
    if (elm.classList.contains('show-label')) {
      elm.classList.remove('show-label');
    } else {
      if (!elm.classList.contains('show-details')) {
        elm.classList.add('show-label');
      }
    }
  }

}
