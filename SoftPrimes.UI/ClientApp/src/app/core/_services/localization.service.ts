import { Injectable, Renderer2, RendererFactory2 } from '@angular/core';
import { SwaggerClient } from './swagger/SwaggerClient.service';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Router } from '@angular/router';
import { StoreService } from './store.service';
import { Title } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class LocalizationService {
  private renderer: Renderer2;

  private isArabic = new BehaviorSubject<boolean>(false);
  isArabic$ = this.isArabic.asObservable();

  constructor(
    private translate: TranslateService,
    private rendererFactory: RendererFactory2,
    private swagger: SwaggerClient,
    private titleService: Title,
    private router: Router
    ) {
    this.renderer = rendererFactory.createRenderer(null, null);
    this.init();
  }

  init() {
    this.translate.setDefaultLang('ar');
    const localization_ar = localStorage.getItem('localization_ar');
    const localization_en = localStorage.getItem('localization_en');
    // const lastUpdateDate = localStorage.getItem('localization_lastUpdateDate');

    // if saved in localStorage use it
    if (localization_ar) {
      this.translate.setTranslation('ar', JSON.parse(localization_ar));
    }
    if (localization_en) {
      this.translate.setTranslation('en', JSON.parse(localization_en));
    }

    // this.swagger.apiLocalizationGetLastUpDateTimeGet().subscribe(value => {

    //   // check if date changed
    //   if (lastUpdateDate !== value.toString()) {
    //     localization_ar = null;
    //     localization_en = null;
    //     localStorage.setItem('localization_lastUpdateDate', value.toString());
    //   }

    //   // get Arabic localization
    //   if (!localization_ar) {
    //     this.swagger.apiLocalizationJsonGet('ar').subscribe(loc_ar => {
    //       localStorage.setItem('localization_ar', loc_ar);
    //       this.translate.setTranslation('ar', JSON.parse(loc_ar));
    //     });
    //   }

    //   // get English localization
    //   if (!localization_en) {
    //     this.swagger.apiLocalizationJsonGet('en').subscribe(loc_en => {
    //       localStorage.setItem('localization_en', loc_en);
    //       this.translate.setTranslation('en', JSON.parse(loc_en));
    //     });
    //   }
    // });

    const culture = localStorage.getItem('culture');
    if (culture) {
      this.translate.use(culture);
      if (culture === 'ar') {
        this.isArabic.next(true);
        this.renderer.addClass(document.body, 'rtl');
      } else {
        this.isArabic.next(false);
        this.renderer.removeClass(document.body, 'rtl');
      }
    } else {
      localStorage.setItem('culture', 'en');
      this.renderer.addClass(document.body, 'rtl');
      this.isArabic.next(true);
      this.translate.use('ar');
    }
  }

  // reload to same url
  reloadPage() {
    const $r$ = window.location.pathname;
    this.router.navigate([$r$]).then(() => {
      this.router.navigate([$r$]);
      window.location.reload();
    });
  }

  changeLocal() {
    const currentLang = localStorage.getItem('culture');
    if (currentLang === 'en') {
      this.translate.use('ar');
      localStorage.setItem('appTitle', 'مسار 4');
      localStorage.setItem('culture', 'ar');
      this.swagger['apiCultureUpdateCultureSessionPost']('ar').subscribe(_ => {
        this.renderer.addClass(document.body, 'rtl');
        this.isArabic.next(true);
        // set application title
        this.titleService.setTitle(this.translate.instant('MasarIV'));
        if (!this.router.url.includes('login')) { this.reloadPage(); }
      });  // to avoid hijri date in server
    } else {
      this.translate.use('en');
      localStorage.setItem('appTitle', 'Masar IV');
      localStorage.setItem('culture', 'en');
      this.swagger['apiCultureUpdateCultureSessionPost']('en').subscribe(_ => {
        this.renderer.removeClass(document.body, 'rtl');
        this.isArabic.next(false);
        // set application title
        this.titleService.setTitle(this.translate.instant('MasarIV'));
        if (!this.router.url.includes('login')) { this.reloadPage(); }
      }); // to avoid hijri date in server
    }
  }

  setLocal(local) {
    local = local === 'ar' ? 'en' : 'ar';
    localStorage.setItem('culture', local);
    this.changeLocal();
  }
}
