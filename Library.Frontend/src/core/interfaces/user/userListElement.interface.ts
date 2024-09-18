import { Guid } from 'guid-typescript';

export interface UserListElement {
    id: Guid;
    lastName: string;
    firstName: string;
    roleName: string;
}