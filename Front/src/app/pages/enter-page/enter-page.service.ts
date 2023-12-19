import { Injectable } from '@angular/core';
import { HttpClient, HttpParams,  HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class EnterPageService {

  private apiUrl = "http://localhost:5001/Account";

  constructor(private http: HttpClient) { }

  login(username: string, password: string):Observable<HttpResponse<any>> {
    const body = { login: username, password: password };
    console.log("here");
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<HttpResponse<any>>(`${this.apiUrl}/login`, body, { headers : headers, observe: 'response' })
  }

  setSession(authResult: any) {
    console.log(authResult.id_token);
    localStorage.setItem('id_token', authResult.id_token);
}          

logout() {
    localStorage.removeItem("id_token");
}
}
