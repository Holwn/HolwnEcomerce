import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LoginReqestDto } from "../models/login-request.dto";
import { LoginResponseDto } from "../models/login-response.dto";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";


@Injectable({
    providedIn:'root',
})
export class AuthService{

    constructor(private httClient: HttpClient){
        
    }

    public login(input: LoginReqestDto): Observable <LoginResponseDto>{
        var body = {
            username: input.username,
            password: input.password,
            client_id: environment.oAuthConfig.clientId,
            client_secret: environment.oAuthConfig.dummyClientSecret,
            grant_type: 'password',
            scope: environment.oAuthConfig.scope
        }

        const data = Object.keys(body).map((key,index)=>`${key}=${encodeURIComponent(body[key])}`).join('&');

        return this.httClient.post<LoginResponseDto>(
            environment.oAuthConfig.issuer+'connect/token',
            data,
            { headers: {'Content-Type': 'application/x-www-form-urlencoded'}}
        );
    };
    
}