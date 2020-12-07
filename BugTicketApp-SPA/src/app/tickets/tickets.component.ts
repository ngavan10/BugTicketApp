import { Component, OnInit } from '@angular/core';
import { TicketService } from '../services/ticket.service';
import { AlertifyService } from '../services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Ticket } from '../_models/Ticket';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { User } from '../_models/User';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css']
})
export class TicketsComponent implements OnInit {

  tickets: Ticket[];
  users: User[];
  modalRef: BsModalRef;
  ticket: Ticket;
  index;
  tic: any;
  comm: any;
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
    this.loadUsers();
  }

  loadTickets() {
    return this.ticketService.getTickets().subscribe(ticket =>
      this.tickets = ticket

      );
  }

  loadUsers() {
    return this.ticketService.getUsers().subscribe(user => 
      this.users = user);
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

createCommentModal(index: any, modal, ticketId: number) {
  this.index = index;
  this.comm = { ...this.comments[index], ticketId};
  this.modalRef = this.modalService.show(modal, Object.assign({}, { class: 'gray modal-lg'}));
}

createComment(comm) {
  this.comments[this.index] = comm;
  this.ticketService.createComment(this.comm).subscribe(() => {
    this.rowSelected = false;
    this.close();
  }, error => {
    this.alertify.error(error);
  })
}

create(tick) {
  this.tickets[this.index] = tick;
  this.ticketService.createTicket(this.tic).subscribe(() =>{
    this.loadTickets();
    this.close();
  }, error => {
    this.alertify.error(error);
  });
}

close(): void {
  this.modalRef.hide();
}





}
