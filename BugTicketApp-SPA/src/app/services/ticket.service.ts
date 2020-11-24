import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Ticket } from '../_models/Ticket';
import { Observable, Subject } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  private refreshData = new Subject<Ticket[]>();

  get refresh() {
    return this.refreshData;
  }

  getTickets() {
    return this.http.get<Ticket[]>(this.baseUrl + 'ticket');
  }

  getTicketById(ticketNumber: any): Observable<Ticket> {
    return this.http.get<Ticket>(this.baseUrl + 'ticket' + '/' + ticketNumber);
  }

  createTicket(ticket: Ticket) {
      return this.http.post(this.baseUrl + 'ticket' + '/', ticket)
      .pipe(
        tap(() => this.refreshData.next()));
    };
  }

