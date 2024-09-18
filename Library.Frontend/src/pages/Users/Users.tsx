export function Users() {
	const navigate = useNavigate();
	const [activities, setActivities] = useState<Activity[] | undefined>([]);
	const {role} = useSelector((s: RootState) => s.user);
	const { getActivities } = useActivityRepository();


	useEffect(() => {
		const fetchData = async () => {
			const activities = await getActivities();
			setActivities(activities);
		};
		fetchData();
	}, []);

	return (
		<div className={styles['activities-page']}>
			<div className={styles['header']}>
				<Headling>Мероприятия</Headling>
				{role == Role.Manager ? <><Button onClick={() => navigate('/admin/addActivity')}>Добавить мероприятие</Button></> : <></>}
			</div>
			<div className={styles['activities']}>
				{
					activities?.map(el => (
						<ActivityCard activity={el} isAdmin={role == Role.Manager}/>
					))
				}
			</div>
			
		</div>
	);
}