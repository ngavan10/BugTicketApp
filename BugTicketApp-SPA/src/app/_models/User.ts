export interface User {
    id: number;
    userName: string;
    created: Date;
    roles?: string[];
}