import { Guid } from 'guid-typescript';

export interface CreateUserResponse {
    id: Guid;
}

export interface CreateUserRequest {
    lastName: string;
    firstName: string; 
    email: string;
    password: string; 
    roleId: Guid;
}