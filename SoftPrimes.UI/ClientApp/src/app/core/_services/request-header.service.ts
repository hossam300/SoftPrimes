import { Injectable, OnInit } from '@angular/core';
import { TokenStoreService } from './token-store.service';
import { AuthTokenType } from '../_models/auth-token-type';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
  })
  export class RequestHeaderService {

  constructor(private tokenStoreService: TokenStoreService) {
    }

    getHeaders(): HttpHeaders {
      const accessToken = this.tokenStoreService.getRawAuthToken(AuthTokenType.AccessToken);
      let headers: HttpHeaders = new HttpHeaders();
      headers = headers.append('Cache-Control', 'no-cache');
      headers = headers.append('Pragma', 'no-cache');
      headers = headers.append('Authorization', `Bearer ${accessToken}`);

      return headers;
    }
  }
