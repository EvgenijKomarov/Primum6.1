import { useAdminProfiles } from '@/entity/admin/model/useAdminProfiles';
import styles from './UsersPage.module.css';
import type { AdminProfileDto } from '@/entity/admin/model/types';
import { useState } from 'react';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import { changeAdminPermissions, deleteAdminProfile } from '@/entity/admin/api/admin.api';
import Button from '@/shared/ui/Button/Button';
import { Popup } from '@/shared/ui/Popup';
import { Loader } from '@/shared/ui/Loader';
import { DeleteIcon, EditIcon, EmptyIcon, SeenIcon } from '@/shared/icons/types';
import { useIncidentLogs } from '@/entity/incidentLog/model/useIncidentLogs';
import type { IncidentLogDto } from '@/entity/incidentLog/model/types';
import { reviseIncidentLog } from '@/entity/incidentLog/api/incidentLogs.api';
import { formatDateTime } from '@/shared/format/format-config';



const LogCard = ({incidentLog, onMutate}: {incidentLog: IncidentLogDto, onMutate:()=>void}) => {
    return(
        <div className={styles.logCard}>
            <div className={styles.logInformation}>
                <p className={styles.logDisplayName}>{incidentLog.adminDisplayName}</p>
                <p className={styles.logDate}>{formatDateTime(incidentLog.dateTime)}</p>
            </div>
            <p className={styles.logDescription}>
                {incidentLog.description}
            </p>
            {!incidentLog.isRevisioned && 
                <Button
                    icon={<SeenIcon/>}
                    size={ButtonSizeEnum.NORMAL}
                    onClick={async () => {await reviseIncidentLog(incidentLog.id); await onMutate()}}/>}
        </div>
    )
}

const LogsPopup = ({adminUserId, onClose}: {adminUserId?: number, onClose: () => void}) => {
    const [isOnlyRevisioned, setIsOnlyRevisioned] = useState(true);
    const { logs, mutate, isLoading } = useIncidentLogs(isOnlyRevisioned, adminUserId);

    return (
        <Popup title='Логи действий' onClose={onClose}>
            <div className={styles.logContent}>
                <p className={styles.onlyUnrevisioned}>
                    Только непроверенные
                    <input
                        type="checkbox"
                        checked={isOnlyRevisioned}
                        onChange={(e) => setIsOnlyRevisioned(e.target.checked)}
                    />
                </p>
                <div className={styles.logsList}>
                    {isLoading ? <Loader/> : logs.map((log)=><LogCard incidentLog={log} onMutate={async () => await mutate()}/>)}
                </div>
            </div>
        </Popup>
    )
}

const AdminCard = ({ admin, onMutate, subjAdminProfile }: { admin: AdminProfileDto; onMutate: () => void; subjAdminProfile: AdminProfileDto | undefined }) => {
    const [logsCardPopupOpen, setLogsCardPopupOpen] = useState(false);
    
    return (
        <div className={styles.card}>
            <div className={styles.userInfo}>
                <p className={styles.userName}>{admin.displayName}</p>
                <p className={styles.userEmail}>{admin.status}</p>
            </div>
            <p className={styles.verticalLine}/>
            <div className={styles.userActions}>
                <AdminProfilePopup admin={admin} onMutate={onMutate} isDisabled={!subjAdminProfile?.permissions['EditPermissions']}/>
                <Button 
                    disabled={!subjAdminProfile?.permissions['InspectIncidentLogs']}
                    variant={ButtonTypeEnum.PRIMARY}
                    size={ButtonSizeEnum.SMALL}
                    icon={<SeenIcon/>}
                    onClick={async () => setLogsCardPopupOpen(true)}
                    >
                    Лог действий
                </Button>
                <Button 
                    disabled={!subjAdminProfile?.permissions['CreateAdminProfiles']}
                    variant={ButtonTypeEnum.PRIMARY}
                    size={ButtonSizeEnum.SMALL}
                    icon={<DeleteIcon/>}
                    onClick={async () => {await deleteAdminProfile(admin.userId);await onMutate()}}
                    >
                    Удалить профиль
                </Button>
            </div>
            {logsCardPopupOpen && <LogsPopup adminUserId={admin.userId} onClose={() => setLogsCardPopupOpen(false)}/>}
        </div>
    );
};

const AdminProfilePopup = ({admin, onMutate, isDisabled}: {admin: AdminProfileDto, onMutate: () => void, isDisabled: boolean | undefined}) => {
    const [topupPopupOpen, setTopupPopupOpen] = useState(false);
    const [permissions, setPermissions] = useState<Record<string, boolean>>(admin?.permissions ?? {});

    const handleToggle = (key: string) => {
        setPermissions(prev => ({
            ...prev,
            [key]: !prev[key],
        }));
    };

    return(
        <>
            <Button
                disabled={isDisabled}
                variant={ButtonTypeEnum.PRIMARY}
                size={ButtonSizeEnum.SMALL}
                icon={<EditIcon/>}
                onClick={() => setTopupPopupOpen(true)}>
                Изменить права админа
            </Button>
            {topupPopupOpen && (
                <Popup
                    onClose={() => setTopupPopupOpen(false)}
                    title={'Изменение прав админа'}>
                    <div className={styles.popup}>
                        <div className={styles.permissions}>
                            {Object.entries(permissions).map(([key, value]) => (
                                <label key={key} className={styles.rightRow}>
                                    {key}
                                    <input
                                        className={styles.checkbox}
                                        type="checkbox"
                                        checked={value}
                                        onChange={() => handleToggle(key)}
                                    />
                                </label>
                            ))}
                        </div>
                        <Button
                            variant={ButtonTypeEnum.PRIMARY}
                            size={ButtonSizeEnum.SMALL}
                            onClick={async () => {await changeAdminPermissions(admin.userId, permissions); await onMutate(); await setTopupPopupOpen(false)}}>
                            Изменить
                        </Button>
                    </div>
                </Popup>
            )}
        </>
    );
}

export const AdminsCollection = ({displayName, adminProfile} : {displayName: string, adminProfile: AdminProfileDto | undefined}) => {
    const { admins, isLoading, mutate } = useAdminProfiles(displayName);
    const [allAdminsLogPopupOpen, setAllAdminsLogPopupOpen] = useState(false);

    return (<div className={styles.collection}>
            <Button
                icon={<SeenIcon/>}
                onClick={() => setAllAdminsLogPopupOpen(true)}
                disabled={!adminProfile?.permissions['InspectIncidentLogs']}>
                Логи всех администраторов
            </Button>
            {allAdminsLogPopupOpen && <LogsPopup onClose={() => setAllAdminsLogPopupOpen(false)}/>}
            {isLoading ? 
                <Loader /> :
                <div className={styles.users}>
                    {admins && admins.length > 0 ? (
                        admins.map((admin) => (
                            <AdminCard key={admin.userId} admin={admin} onMutate={mutate} subjAdminProfile={adminProfile}/>
                        ))
                    ) : (
                        <div className={styles.empty}>
                            <EmptyIcon />
                            <p className={styles.emptyText}>
                                Пользователи не найдены
                            </p>
                        </div>
                    )}
                </div>}
            </div>)
};