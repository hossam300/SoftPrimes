import {Injectable, Renderer2, RendererFactory2} from '@angular/core';
import {BehaviorSubject} from 'rxjs/internal/BehaviorSubject';
import { BrowserStorageService } from './browser-storage.service';
import { SwaggerClient } from './swagger/SwaggerClient.service';


@Injectable({
  providedIn: 'root'
})
export class LayoutService {
  private renderer: Renderer2;
  public userCanChangeColor: BehaviorSubject<any> = new BehaviorSubject<any>(false); // parameter to authorize user to change theme

  private sidebarActive = new BehaviorSubject<boolean>(false);
  sidebarActive$ = this.sidebarActive.asObservable();
  isVip$ = new BehaviorSubject<boolean>(false);

  constructor(
    private browserStorageService: BrowserStorageService,
    private rendererFactory: RendererFactory2,
    private swagger: SwaggerClient
  ) {
      this.renderer = rendererFactory.createRenderer(null, null);
  }

  setSidebarStatus(active: boolean) {
    this.sidebarActive.next(active);
  }

  toggleSidebarStatus() {
    this.sidebarActive.next(!this.sidebarActive.value);
  }

  setTheme(themeName: string) {
    const body = document.body;
    const className = body.className;
    body.className = className.split(' ').filter(x => !x.includes('-theme')).join(' ');
    body.classList.add(themeName);
    this.browserStorageService.setLocal('theme', themeName);
  }

  getTheme(themeKey: string = 'defaultTheme') {
    // return this.swagger.apiSystemSettingsGetSystemSettingByCodeGet(themeKey)
  }

  toggleIsLoading(loading: boolean) {
    if (loading) {
      if (!document.getElementsByTagName('body')[0].classList.contains('isLoading')) {
        this.renderer.addClass(document.body, 'isLoading');
      } else { this.renderer.removeClass(document.body, 'isLoading'); }
    } else {
      this.renderer.removeClass(document.body, 'isLoading');
    }
  }

  toggleIsLoadingBlockUI(loading: boolean) {
    if (loading) {
      this.renderer.addClass(document.body, 'isLoading-blockUI');
    } else {
      this.renderer.removeClass(document.body, 'isLoading-blockUI');
    }
  }
}
