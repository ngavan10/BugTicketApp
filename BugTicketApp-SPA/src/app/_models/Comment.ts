import { User } from './User';

export interface Comment {
    id: number;
    commentId: number;
    description: string;
    user: User;
}
