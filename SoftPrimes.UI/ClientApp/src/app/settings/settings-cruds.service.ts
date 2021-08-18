import { Injectable } from '@angular/core';
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
}