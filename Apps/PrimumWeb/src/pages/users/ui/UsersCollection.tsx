import type { AdminProfileDto } from "@/entity/admin/model/types";
import type { UserDto } from "@/entity/user";
import { useUsersByAdmin } from "@/entity/user/model/getUsersByAdmin";
import { Loader } from "@/shared/ui/Loader";
import styles from './UsersPage.module.css';
import { BadgeTypeEnum } from "@/shared/enums/badge";
import { Badge } from "@/shared/ui/Badge/Badge";
import { StatCard } from "@/shared/ui/StatCard/StatCard";
import Button from "@/shared/ui/Button/Button";
import { ButtonSizeEnum, ButtonTypeEnum } from "@/shared/enums";
import { changeBanStatus, createAdminProfile } from "@/entity/user/api/userByAdmin.api";
import { deleteAdminProfile } from "@/entity/admin/api/admin.api";
import { EmptyIcon } from "@/shared/icons/types";
import { useState } from "react";
import { Popup } from "@/shared/ui/Popup";
import { Input } from "@/shared/ui/Input";

const AdminProfileCreation = ({userId, onMutate, isDisabled}: {userId: number, onMutate: () => void, isDisabled: boolean | undefined}) => {
    const [topupPopupOpen, setTopupPopupOpen] = useState(false);
    const [status, setStatus] = useState('');

    return (
        <>
            <Button
                variant={ButtonTypeEnum.PRIMARY}
                size={ButtonSizeEnum.SMALL}
                disabled={isDisabled}
                onClick={() => setTopupPopupOpen(true)}>
                Создать профиль админа
            </Button>
            {topupPopupOpen && (
                <Popup
                    onClose={() => setTopupPopupOpen(false)}
                    title={'Создание профиля админа'}>
                    <div className={styles.popup}>
                        <Input
                            value={status}
                            onChange={(val) => setStatus(val)}
                            placeholder="Введите статус"/>
                        <Button
                            variant={ButtonTypeEnum.PRIMARY}
                            size={ButtonSizeEnum.SMALL}
                            onClick={() => {createAdminProfile(userId, status); onMutate()}}>
                            Создать
                        </Button>
                    </div>
                </Popup>
            )}
        </>
    );
};

const UserCard = ({ user, onMutate, adminProfile }: { user: UserDto; onMutate: () => void; adminProfile: AdminProfileDto | undefined }) => {
    return (
        <div className={styles.card}>
            <div className={styles.userInfo}>
                <p className={styles.userName}>{user.displayName}</p>
                <p className={styles.userEmail}>{user.email}</p>
                <div className={styles.userBadges}>
                    {user.isBanned ? <Badge text="Забанен" badgeType={BadgeTypeEnum.Negative} /> : null}
                    {!user.mailConfirmed ? <Badge text="Почта не подтверждена" badgeType={BadgeTypeEnum.Negative} /> : null}
                    {user.isAvailable ? <Badge text="Доступен" badgeType={BadgeTypeEnum.Positive} /> : null}
                </div>
            </div>
            <p className={styles.verticalLine}/>
            <div className={styles.userRoles}>
                <StatCard title="Студент" value={user.isApprovedStudent ? 'Да' : 'Нет'} />
                <StatCard title="Преподаватель" value={user.isApprovedTeacher ? 'Да' : 'Нет'}/>
                <StatCard title="Администратор" value={user.isAdmin ? 'Да' : 'Нет'} />
            </div>
            <p className={styles.verticalLine}/>
            <div className={styles.userActions}>
                <Button 
                    disabled={!adminProfile?.permissions['ChangeBanStatus']}
                    variant={ButtonTypeEnum.PRIMARY}
                    onClick={() => changeBanStatus(user.id, !user.isBanned).then(onMutate)}>
                    {user.isBanned ? 'Разбанить' : 'Забанить'}
                </Button>
                {user.isAdmin ? 
                    <Button 
                        disabled={!adminProfile?.permissions['CreateAdminProfiles']}
                        variant={ButtonTypeEnum.PRIMARY}
                        onClick={() => {deleteAdminProfile(user.id); onMutate()}}>
                        Удалить профиль админа
                    </Button> : 
                    <AdminProfileCreation userId={user.id} onMutate={onMutate} isDisabled={!adminProfile?.permissions['CreateAdminProfiles']}/>
                }
            </div>
        </div>
    );
};

export const UsersCollection = ({displayName, adminProfile} : {displayName: string, adminProfile: AdminProfileDto | undefined}) => {
    const { users, isLoading, mutate } = useUsersByAdmin(displayName);

    return <>
            {isLoading ?
                <Loader /> :
                <div className={styles.users}>
                    {users && users.length > 0 ? (
                        users.map((user) => (
                            <UserCard key={user.id} user={user} onMutate={mutate} adminProfile={adminProfile}/>
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
            </>
}