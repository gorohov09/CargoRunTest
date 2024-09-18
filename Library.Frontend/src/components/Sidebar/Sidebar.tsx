import { NavLink } from 'react-router-dom';
import styles from './Sidebar.module.css';
import cn from 'classnames';
import {
	BookTwoTone
} from '@ant-design/icons';
import { useSelector } from 'react-redux';
import { RootState } from '../../store/store';
import { Role } from '../../core/enums/role.enum';

export function Sidebar() {
	const { fullName, role } = useSelector((s: RootState) => s.user);

	return (
		<div className={styles['sidebar']}>
			<div className={styles['user']}>
				<div className={styles['name']}>{ role !== Role.Admin ? fullName : <>Админ</> }</div>
				<div className={styles['role']}>{ role === Role.Client ? <>(Клиент)</> : role === Role.Librarian ? <>(Библиотекарь)</> : <></>}</div>
			</div>
			<div className={styles['menu']}>
				{
					role == Role.Client && <>
						<NavLink to='/books' className={({ isActive }) => cn(styles['link'], {
							[styles.active]: isActive
						})}>
							<div className={styles['menu-item']}>
								<BookTwoTone className={styles['menu-item-icon']}/>
								<span className={styles['menu-item-text']}>Книги</span>
							</div>    
						</NavLink>
						<NavLink to='/myBlockedBooks' className={({ isActive }) => cn(styles['link'], {
							[styles.active]: isActive
						})}>
							<div className={styles['menu-item']}>
								<BookTwoTone className={styles['menu-item-icon']}/>
								<span className={styles['menu-item-text']}>Мои забронированные книги</span>
							</div>    
						</NavLink>
					</>
				}
				{
					role == Role.Librarian && <>
						<NavLink to='/portfolio' className={({ isActive }) => cn(styles['link'], {
							[styles.active]: isActive
						})}>
							<div className={styles['menu-item']}>
								<BookTwoTone className={styles['menu-item-icon']}/>
								<span className={styles['menu-item-text']}>Книги</span>
							</div>    
						</NavLink>
						<NavLink to='/portfolio' className={({ isActive }) => cn(styles['link'], {
							[styles.active]: isActive
						})}>
							<div className={styles['menu-item']}>
								<BookTwoTone className={styles['menu-item-icon']}/>
								<span className={styles['menu-item-text']}>Клиенты</span>
							</div>    
						</NavLink>
					</>
				}
				{
					role == Role.Admin && <>
						<NavLink to='/users' className={({ isActive }) => cn(styles['link'], {
							[styles.active]: isActive
						})}>
							<div className={styles['menu-item']}>
								<BookTwoTone className={styles['menu-item-icon']}/>
								<span className={styles['menu-item-text']}>Пользователи</span>
							</div>    
						</NavLink>
					</>
				}
			</div>
		</div>
	);
}