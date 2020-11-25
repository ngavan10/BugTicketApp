import { Component, OnInit } from '@angular/core';
import { TicketService } from '../services/ticket.service';
import { AlertifyService } from '../services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Ticket } from '../_models/Ticket';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css']
})
export class TicketsComponent implements OnInit {

  tickets: Ticket[];
  modalRef: BsModalRef;
  ticket: Ticket;
  index;
  tic: any;
  comment;
  commentList: any = [];
  comments: Comment[];
  rowSelected = false;

  constructor(private ticketService: TicketService,
              private alertify: AlertifyService,
              private route: ActivatedRoute,
              private modalService: BsModalService) { }

  ngOnInit() {
    this.loadTickets();
  }

  loadTickets() {
    return this.ticketService.getTickets().subscribe(ticket =>
      this.tickets = ticket

      );
  }

  getComments(ticketNumber: number) {
    this.commentList = [];
    this.rowSelected = true;
    this.ticketService.getTicketById(ticketNumber).subscribe(tick => {
      this.ticket = tick;
      this.comments = tick.comments;
      this.commentList.push(tick.comments);
  });

}

createRow(index: any, modal) {
  this.index = index;
  this.tic = { ...this.tickets[index]};
  this.modalRef = this.modalService.show(modal, Object.assign({}, { class: 'gray modal-lg'}));
}

create(tick) {
  this.tickets[this.index] = tick;
  this.ticketService.createTicket(this.tic).subscribe(() =>{
    this.close();
  }, error => {
    this.alertify.error(error);
  });
}

close(): void {
  this.modalRef.hide();
}





}
