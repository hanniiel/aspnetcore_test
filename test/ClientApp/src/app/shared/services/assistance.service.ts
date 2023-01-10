import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { EnvironmentUrlService } from './environment-url.service'
import { Subject } from 'rxjs'

import { AssistanceForRegistrationDto } from './../../__interfaces/assistance/AssistanceForRegistrationDto'
import { AssistanceResponseDto } from './../../__interfaces/response/AssistanceResponseDto'

@Injectable({
  providedIn: 'root'
})
export class AssistanceService {

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }

  public assistance = (route: string, body: AssistanceForRegistrationDto) => {
    return this.http.post<AssistanceResponseDto>(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
