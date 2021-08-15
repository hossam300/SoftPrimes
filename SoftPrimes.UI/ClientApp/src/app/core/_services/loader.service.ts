import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  constructor() { }

  addLoader() {
    document.getElementsByTagName('body')[0].className = document.getElementsByTagName('body')[0].className.concat(" isLoading-blockUI");
  }

  removeLoader() {
    document.getElementsByTagName('body')[0].className = document.getElementsByTagName('body')[0].className.replace("isLoading-blockUI", '')
  }

}
