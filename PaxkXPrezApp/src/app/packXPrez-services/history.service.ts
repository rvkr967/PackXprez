import { Injectable } from '@angular/core';
import { IHistory } from '../packXPrez-interfaces/packagehistory';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {
  history: IHistory[];
  constructor(private http: HttpClient) { }
  getHistory(email: string): Observable<IHistory[]> {
    let tempvar = this.http.get<IHistory[]>('https://localhost:44383/api/user/GetPackageHistory?emailId=' + email).pipe(catchError(this.errorHandler));
    return tempvar;
  }
  getStatus(number: number): Observable<string> {
    let temp = this.http.get<string>('https://localhost:44383/api/user/GetOrderStatus?num=' + number).pipe(catchError(this.errorHandler));
    return temp;
  }
  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error");
  }
}
