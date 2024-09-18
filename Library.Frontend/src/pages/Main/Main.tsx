import styles from './Main.module.css';

export function Main() {
	return (
		<div className={styles['main-info']}>
			<p>
				Вы находитесь в сервисе "Библиотека".
			</p>
		</div>
	);
}