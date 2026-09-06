import { useMemo, useState } from 'react';
import styles from './PlatformConfigPage.module.css';
import { Tabs, type TabItem } from '@/shared/ui/TabBar/TabBar';
import { PromocodesList } from './PromocodesList';
import { useSelfAdminProfile } from '@/entity/admin/model/useCurrentAdminProfile';
import { ThemesList } from './ThemesList';

type PlatformTab = 'promocodes' | 'themes';

export const PlatformConfigPage = () => {
    const { adminProfile } = useSelfAdminProfile();
    const [currentTab, setCurrentTab] = useState<PlatformTab>('promocodes');

    const TABS = useMemo<TabItem<PlatformTab>[]>(() => [
        {
            value: 'promocodes',
            label: 'Промокоды',
            content: adminProfile ? <PromocodesList {...adminProfile} /> : null,
        },
        {
            value: 'themes',
            label: 'Темы курсов',
            content: adminProfile ? <ThemesList {...adminProfile} /> : null,
        },
    ], [adminProfile]);

    return (
        <div className={styles.page}>
            <h1 className={styles.title}>Настройки платформы</h1>
            <Tabs tabs={TABS} value={currentTab} onChange={setCurrentTab} />
        </div>
    );
};