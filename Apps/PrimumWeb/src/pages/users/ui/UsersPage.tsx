import { useEffect, useState } from 'react';
import styles from './UsersPage.module.css';
import { useSelfAdminProfile } from '@/entity/admin/model/useCurrentAdminProfile';
import { UsersCollection } from './UsersCollection';
import { AdminsCollection } from './AdminCollection';

function useDebouncedValue<T>(value: T, delay = 400): T {
    const [debounced, setDebounced] = useState(value);

    useEffect(() => {
        const timer = setTimeout(() => setDebounced(value), delay);
        return () => clearTimeout(timer);
    }, [value, delay]);

    return debounced;
}

export const UsersPage = () => { //TODO: странно работает onMutate
    const [displayName, setDisplayName] = useState('');
    const debouncedDisplayName = useDebouncedValue(displayName, 400);

    const [isAdminCollectionEnabled, setIsAdminCollectionEnabled] = useState(false);
    const { adminProfile } = useSelfAdminProfile();

    return (
        <div className={styles.page}>
            <h1 className={styles.title}>Пользователи</h1>
            <textarea
                className={styles.textarea}
                placeholder="Введите ФИО пользователя"
                value={displayName}
                onChange={(e) => setDisplayName(e.target.value)}
            />
            <div className={styles.filterBar}>
                <button
                    className={`${styles.chip} ${!isAdminCollectionEnabled ? styles.chipActive : ''}`}
                    onClick={() => setIsAdminCollectionEnabled(false)}>
                Все
                </button>
                <button
                    className={`${styles.chip} ${isAdminCollectionEnabled ? styles.chipActive : ''}`}
                    onClick={() => setIsAdminCollectionEnabled(true)}>
                Только администраторы
                </button>
            </div>
            {!isAdminCollectionEnabled ? 
                <UsersCollection displayName={debouncedDisplayName} adminProfile={adminProfile}/> :
                <AdminsCollection displayName={debouncedDisplayName} adminProfile={adminProfile}/>
            }
        </div>
    );
};