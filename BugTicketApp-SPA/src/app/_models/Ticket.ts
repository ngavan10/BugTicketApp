export interface Ticket {
    id: number;
    ticketNumber: number;
    description: string;
    status: string;
    priority: string;
    comments: Comment[];
    userName: string;
}
