export interface Ticket {
    id: number;
    ticketNumber: number;
    description: string;
    status: string;
    assignedTo: string;
    priority: string;
    comments: Comment[];
    userName: string;
}
