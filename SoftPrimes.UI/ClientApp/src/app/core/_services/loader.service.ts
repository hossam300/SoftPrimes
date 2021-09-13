import { Injectable, Renderer2, RendererFactory2 } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  renderer: Renderer2;
  constructor(
    private rendererFactory: RendererFactory2
  ) {
    this.renderer = this.rendererFactory.createRenderer(null, null);
  }

  addLoader() {
    this.renderer.addClass(document.getElementById('loader'), 'd-flex');
  }

  removeLoader() {
    this.renderer.removeClass(document.getElementById('loader'), 'd-flex');
  }

}
