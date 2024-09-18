import React from 'react';
import ReactDOM from 'react-dom/client';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import { MainLayout } from './layouts/Main/MainLayout';
import './index.css';
import { AuthLayout } from './layouts/Auth/AuthLayout';
import { Login } from './pages/Login/Login';
import { RequireAuth } from './helpers/RequireAuth';
import { Provider } from 'react-redux';
import { store } from './store/store';
import { Main } from './pages/Main/Main';
import { AdminLayout } from './layouts/Admin/AdminLayout';
import { AdminMain } from './pages/Admin/AdminMain';

const router = createBrowserRouter([
	{
		path: '/',
		element: <RequireAuth><MainLayout /></RequireAuth>,
		children: [
			{
				path: '/',
				element: <Main />
			}
		]
	},
	{
		path: '/admin',
		element: <RequireAuth><AdminLayout /></RequireAuth>,
		children: [
			{
				path: '/admin',
				element: <AdminMain />
			}
		]
	},
	{
		path: '/auth',
		element: <AuthLayout />,
		children: [
			{
				path: 'login',
				element: <Login />
			}
		]
	}
]);

ReactDOM.createRoot(document.getElementById('root')!).render(
	<React.StrictMode>
		<Provider store={store}>
			<RouterProvider router={router} />
		</Provider>
	</React.StrictMode>
);