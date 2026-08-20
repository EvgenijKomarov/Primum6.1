import { useState } from 'react';
import styles from './UsersPage.module.css';
import { useSelfAdminProfile } from '@/entity/admin/model/useCurrentAdminProfile';
import { UsersCollection } from './UsersCollection';
import { AdminsCollection } from './AdminCollection';
import { useDebouncedValue } from '@/shared/lib/debounced/debounced';

export const UsersPage = () => {
    const { debouncedValue, value, setValue } = useDebouncedValue('', 400);

    const [isAdminCollectionEnabled, setIsAdminCollectionEnabled] = useState(false);
    const { adminProfile } = useSelfAdminProfile();

    return (
        <div className={styles.page}>
            <h1 className={styles.title}>Пользователи</h1>
            <textarea
                className={styles.textarea}
                placeholder="Введите ФИО пользователя"
                value={value}
                onChange={(e) => setValue(e.target.value)}
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
                <UsersCollection displayName={debouncedValue} adminProfile={adminProfile}/> :
                <AdminsCollection displayName={debouncedValue} adminProfile={adminProfile}/>
            }
        </div>
    );
};