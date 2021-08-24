import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { Filter, Sort, SwaggerClient } from '../core/_services/swagger/SwaggerClient.service';

@Injectable({
  providedIn: 'root'
})
export class SettingsCrudsService {

  constructor(private swagger: SwaggerClient) { }

  getAll(controller: string, take = 10, skip = 0, sort = [], filters = []) {
    return this.swagger['api' + controller + 'GetAllGet']
    (take, skip, sort, undefined, undefined, undefined, undefined, undefined, false);
  }

  insertDTO(controller, dto) {
    return this.swagger['api' + controller + 'InsertPost'](dto);
  }

  updateDTO(controller, dto) {
    return this.swagger['api' + controller + 'UpdatePut'](dto);
  }

  getDTOById(controller, id) {
    return this.swagger['api' + controller + 'GetByIdGet'](id);
  }

  deleteDTO(controller, id) {
    return this.swagger['api' + controller + 'DeleteDelete'](id);
  }

  insertRoles(dto) {
    return this.swagger.apiRolesInsertRolePost([dto]);
  }

  updateRoles(dto) {
    return this.swagger.apiRolesUpdateRolePut([dto]);
  }

  toggleTemplate(templateId, active: boolean) {
    return this.swagger.apiToursActiveDisActiveTemplateGet(templateId, active);
  }

  getPermissionsLookup(searchTxt?, take?) {
    return this.swagger.apiPermissionsGetPermissionLookupsGet(searchTxt, take);
  }

  getRolesLookup(searchTxt?, take?) {
    return this.swagger.apiRolesGetRoleLookupsGet(searchTxt, take);
  }

  getTemplatesLookup(searchTxt?, take?) {
    return this.swagger.apiToursGetTemplatesGet(searchTxt, take);
  }

  getAgentsLookup(searchTxt?, take?) {
    return this.swagger.apiAccountGetAgentLookupsGet(searchTxt, take);
  }

  getCheckPointsLookup(searchTxt?, take?) {
    return this.swagger.apiCheckPointsGetCheckPointLookupsGet(searchTxt, take);
  }

  printQr(sectionId) {
    const canvas = document.getElementById(sectionId).querySelector('canvas');
    const printContents = `
    <div style="display:flex;align-items: center;flex-direction:column;">
      <img style="width:400px" src="${canvas.toDataURL('image/png')}" alt="QR" />
      <h1 style="font-size: 64px;">Scan Me</h1>
    </div>`;
    const html = `</head><body id="print" onload="setTimeout(function(){window.print();},1000);<!--window.close()-->">${printContents}</html>`;
    let popup;
    if (window) {
      // check if user browser is chrome.
      if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {

        popup = window.open('', '_blank',
          'width=600,height=600,scrollbars=no,menubar=no,toolbar=no,'
          + 'location=no,status=no,titlebar=no');

        popup.window.focus();
        popup.document.write('<!DOCTYPE html><html><head>'
          + html);

        // Emit message that says print window is Opened!!
        popup.window.postMessage('Print Window is opened');
        // popup.window.addEventListener('message', (msg) => this.eBuss.emit('printWindowOpen', true));

        popup.onbeforeunload = function (event) {
          popup.document.close();
          popup.close();
          return '.\n';
        };
        popup.onabort = function (event) {
          popup.document.close();
          popup.close();
        };
      } else if (!!window['MSInputMethodContext'] && !!document['documentMode']) {
        // console.log('is IE print()');
        popup = window.open('', '_blank', 'width=800,height=600');
        popup.document.open();
        popup.document.write(
          '<html><head>'
          + html
        );

        // Emit message that says print window is Opened!!
        popup.window.addEventListener('load', function () {
          // console.log('load success');
          popup.postMessage('Print Window is opened', '*');
        });
        // popup.window.addEventListener('message', (msg) => this.eBuss.emit('printWindowOpen', true));

        setTimeout(() => popup.print(), 1000);
        popup.document.close();
      } else {
        popup = window.open('', '_blank', 'width=800,height=600');
        popup.document.open();
        popup.document.write('<html><head>'
          + html);

        // Emit message that says print window is Opened!!
        popup.window.postMessage('Print Window is opened');
        // popup.window.addEventListener('message', (msg) => this.eBuss.emit('printWindowOpen', true));

        // setTimeout(() => popup.print(), 1000);
        popup.document.close();
      }

      popup.document.close();
    }
  }
}
