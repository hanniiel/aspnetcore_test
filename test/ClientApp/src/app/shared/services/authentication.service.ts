import { UserForRegistrationDto } from './../../__interfaces/user/UserForRegistrationDto'
import { UserForAuthenticationDto } from './../../__interfaces/user/UserForAuthenticationDto'
import { RegistrationResponseDto } from './../../__interfaces/response/registrationResponseDto'
import { AuthResponseDto } from './../../__interfaces/response/AuthResponseDto'
import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service'
import { Subject } from 'rxjs'
import { JwtHelperService } from '@auth0/angular-jwt'

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService, private jwtHelper: JwtHelperService) { }

  public registerUser = (route: string, body: UserForRegistrationDto) => {
    return this.http.post<RegistrationResponseDto>(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");

    return token!=null && !this.jwtHelper.isTokenExpired(token)
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

  public loginUser = (route: string, body: UserForAuthenticationDto) => {
    return this.http.post<AuthResponseDto>(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }
}
