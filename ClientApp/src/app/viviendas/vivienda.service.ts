import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, tap, throwError } from "rxjs";
import { IReservaCreate } from "../shared/reserva";

import { IVivienda, IViviendaCreate } from "../shared/vivienda";

@Injectable({
  providedIn: 'root'
})
export class ViviendaService {
  private viviendaUrl = 'api/viviendas';

  constructor(private http: HttpClient) { }

  public viviendas: IVivienda[] = [];

  
  loadViviendas(): Observable<IVivienda[]> {
    return this.http.get<IVivienda[]>(this.viviendaUrl).
      pipe(tap(data => console.log('All', JSON.stringify(data))), catchError(this.handleError));
  }

  loadVivienda(id: number): Observable<IVivienda> {
    return this.http.get<IVivienda>(this.viviendaUrl + '/' + id).
    pipe(tap(data => console.log('All', JSON.stringify(data))), catchError(this.handleError));
  }

  loadViviendaSinReservas(id: number): Observable<IVivienda> {
    return this.http.get<IVivienda>(this.viviendaUrl + '/' + id + '?includeRese').
      pipe(tap(data => console.log('All', JSON.stringify(data))), catchError(this.handleError));
  }

  putViviendaUpdateForm(id:number , viviendaCreate: IViviendaCreate): Observable<any> {
    return this.http.put(this.viviendaUrl + '/update/' + id, viviendaCreate);
  }

  postCreateViviendaForm(viviendaCreate: IViviendaCreate): Observable<any> {

    return this.http.post(this.viviendaUrl, viviendaCreate);

    //return of(userSettings);
  }

  deleteVivienda(id: number): Observable<any> {

    return this.http.delete(this.viviendaUrl + '/' + id);

    //return of(userSettings);
  }

  deleteReserva(idVivienda: number, idReserva: number): Observable<any> {
    return this.http.delete(this.viviendaUrl + '/' + idVivienda + '/reservas/' + idReserva);
  }

  postCreateReservaForm(id: number, reservaCreate: IReservaCreate): Observable<any> {
    return this.http.post(this.viviendaUrl + '/' + id + '/reservas', reservaCreate)
  }

  private handleError(err: HttpErrorResponse) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(() => errorMessage);
  }
}
