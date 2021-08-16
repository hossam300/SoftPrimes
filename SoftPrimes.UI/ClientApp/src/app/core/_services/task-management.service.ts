import { SwaggerClient } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TaskManagementService {

  constructor(
    private swagger: SwaggerClient
  ) { }

  getAllTourAgents(take = 10, skip = 0, sort = [], filters = []) {
    return this.swagger.apiTourAgentsGetAllGet(take, skip, sort, undefined, undefined, undefined, undefined, undefined, false);
  }

  editTourAgent(id) {
    return this.swagger.apiTourAgentsGetByIdGet(id);
  }

  deleteTourAgent(id: number) {
    return this.swagger.apiTourAgentsDeleteDelete(id);
  }
}
