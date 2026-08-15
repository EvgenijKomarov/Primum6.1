import { useIncidents } from '@/entity/incident/model/useIncidents';
import styles from './IncidentsPage.module.css';
import type { IncidentDecision, IncidentDto, IncidentDecisionInputDto } from '@/entity/incident/model/types';
import { translateIncidentDecision, translateIncidentMeaning, translateIncidentStatus } from '@/features/translation/translation';
import { Collapsible } from '@/shared/ui/Collapsible/Collapsible';
import { useState } from 'react';
import Button from '@/shared/ui/Button/Button';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums/button';
import { useToast } from '@/shared/ui/Toast/useToast';
import { solveIncident } from '@/entity/incident/api/incidents.api';

export const IncidentCard = ({ incident, onMutate }: { incident: IncidentDto; onMutate: () => void }) => {
    const [incidentDescription, setIncidentDescription] = useState('');
    const { showToast } = useToast();

    const handleDecision = async (desicion: IncidentDecision) => {
        if (incidentDescription.trim() === '') {
            showToast('Пожалуйста, введите описание решения по инциденту', 'error');
            return;
        }
        const data: IncidentDecisionInputDto = {
            objectId: incident.objectId,
            meaning: incident.meaning,
            decision: desicion,
            decisionExplanation: incidentDescription,
        };
        try{
            await solveIncident(data);
            await onMutate();
            showToast('Решение по инциденту отправлено', 'success');
        } catch (e: unknown) {
            showToast((e as Error).message, 'error');
        }
    }

    return (
        <div className={styles.card}>
            <div className={styles.incidentHeader}>
                <p>{translateIncidentStatus(incident.status)}</p>
            </div>
            <Collapsible title={`${translateIncidentMeaning(incident.meaning)} id: ${incident.objectId}`}>
                <div className={styles.incidentBody}>
                    <div className={styles.incidentDetails}>
                        <p className={styles.incidentInfo}>
                            {incident.commonInfo}
                        </p>
                        <div className={styles.incidentHistory}>
                            {incident.linkedLogs && incident.linkedLogs.length > 0 ? 
                            incident.linkedLogs.map((log) => (
                                <div key={log.id} className={styles.incidentLog}>
                                    <div className={styles.incidentLogAbout}>
                                        <p className={styles.incidentLogAdmin}>{log.adminDisplayName}</p>
                                        <p className={styles.incidentLogDate}>{new Date(log.dateTime).toLocaleString()}</p>
                                    </div>
                                    <p className={styles.incidentLogDescription}>{log.description}</p>
                                </div>
                            )) : <p className={styles.incidentNoLogs}>Решения не принимались</p>}
                        </div>
                    </div>
                    <div className={styles.incidentFooter}>
                        <textarea
                            className={styles.textarea}
                            value={incidentDescription}
                            onChange={(e) => setIncidentDescription(e.target.value)}
                            placeholder="Объясните решение по инциденту"
                        />
                        <div className={styles.incidentDecisions}>
                            {incident.decisions && incident.decisions.length > 0 ? 
                            incident.decisions.map((decision) => (
                                <Button 
                                    key={decision}
                                    onClick={() => {handleDecision(decision)}}
                                    size={ButtonSizeEnum.NORMAL}
                                    variant={ButtonTypeEnum.PRIMARY}
                                    disabled={incidentDescription.trim() === ''}
                                >
                                    {translateIncidentDecision(decision)}
                                </Button>
                            )) : null}
                        </div>
                    </div>
                </div>
            </Collapsible>
        </div>
    );
}

export const IncidentsPage = () => {
    const { incidents, mutate } = useIncidents();

    return (
        <div className={styles.page}>
            <h1 className={styles.title}>Инциденты</h1>
            <p className={styles.line}></p>

            <div className={styles.incidentsList}>
                {incidents.map((incident) => (
                    <IncidentCard key={`${incident.objectId}-${incident.meaning}`} incident={incident} onMutate={mutate} />
                ))}
            </div>
        </div>
    );
}