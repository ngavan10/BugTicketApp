import { User } from './User';

export interface Comment {
    id: number;
    commentId: number;
    description: string;
    ticketId: number;
    user: User;
}
