import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IUser } from 'src/app/packXPrez-interfaces/user';
import { IAddress } from 'src/app/packXPrez-interfaces/addresses';
import { IRegistration } from 'src/app/packXPrez-interfaces/registration';
@Injectable({
  providedIn: 'root'
})
export class RegisterShipmentService {
  role: string;
  constructor(private http: HttpClient) { }

  validateUserr(emailId: string, password: string): Observable<string> {
    var userObj: IUser;
    userObj = { emailId: emailId, password: password };
    let tempvar = this.http.post<string>('https://localhost:44383/api/user/UserValidation', userObj).pipe(catchError(this.errorHandler));
    return tempvar;
  }
  serviceValidation(num: number, num1: number): Observable<number> {
    var param: string = '?num=' + num + '&num1=' + num1;
    let temp = this.http.get<number>('https://localhost:44383/api/user/ServiceValidation' + param).pipe(catchError(this.errorHandler));
    return temp;
  }
  getAddresses(emailId: string): Observable<IAddress[]> {
    var param1: string = '?emailId=' + emailId
    let temp1 = this.http.get<IAddress[]>('https://localhost:44383/api/user/GetAddresses' + param1).pipe(catchError(this.errorHandler));
    return temp1;
  }
  shipmentRegistration(regObj: IRegistration): Observable<number> {
    let temp2 = this.http.post<number>('https://localhost:44383/api/user/RegisterShipment', regObj).pipe(catchError(this.errorHandler));
    return temp2;
  }
  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error");
  }
}
