import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { UsersBaseResponse } from '../../core/interfaces/user/userListBaseResponse.interface';
import { PREFIX } from '../../helpers/API';
import axios from 'axios';
import { UserListElement } from '../../core/interfaces/user/userListElement.interface';
import { CreateUserRequest, CreateUserResponse } from '../../core/interfaces/user/createUserResponse.interface';

export interface UsersState {
    users: UserListElement[];
}

const initialState: UsersState = {
	users: []
};

export const getUsersList = createAsyncThunk('users/list',
	async (params: {jwt: string | null}) => {
		const {data} = await axios.get<UsersBaseResponse>(`${PREFIX}/Users/list`, {
			headers: {
				'Authorization': `Bearer ${params.jwt}`
			}
		});

		return data.entities;
	}
);

export const createUser = createAsyncThunk('users/createUser',
	async (params: CreateUserRequest) => {
		const { data } = await axios.post<CreateUserResponse>(`${PREFIX}/Admin/CreateUser`, {
			...params
		});
		return data;
	}
);

export const userSlice = createSlice({
	name: 'users',
	initialState,
	reducers: {},
	extraReducers: (builder) => {
		builder
			.addCase(getUsersList.fulfilled, (state, action: PayloadAction<UserListElement[]>) => {
				state.users = action.payload;
			});
	}
});

export default userSlice.reducer;
export const userActions = userSlice.actions;