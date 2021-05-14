import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IUserReg } from 'src/app/packXPrez-interfaces/userReg';

@Injectable({
  providedIn: 'root'
})
export class UserRegService {

  constructor(private http: HttpClient) { }
  userRegistration(regObj: IUserReg): Observable<number> {
    let tempvar = this.http.post<number>('https://localhost:44383/api/user/UserRegistration', regObj).pipe(catchError(this.errorHandler));
    return tempvar;
  }
  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error");
  }
}
