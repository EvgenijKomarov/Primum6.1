import type { IncidentLogDto } from "@/entity/incidentLog/model/types";

export enum IncidentStatus {
    Unknown = 0,
    NeedModeration = 1,
    NeedAdministration = 2,
    NeedManagerApprovement = 3,
    NeedInspectation = 4,
}

export enum IncidentMeaning {
    Unknown = 0,
    Teacher = 1,
    Student = 2,
    Course = 3,
    Lesson = 4,
    LessonReport = 5
}

export enum IncidentDecision {
    Approve = 0,
    Delete = 1,
    SendToAdministrator = 2,
    SendToManager = 3,
    SetMissedByNoReason = 4,
    BanUser = 5,
    SetMissedByValidReason = 6,
    LightBlame = 8,
    Pardon = 10,
    Revise = 7,
}

export interface IncidentDto {
    objectId: number;
    commonInfo: string;
    status: IncidentStatus;
    meaning: IncidentMeaning;
    decisions: IncidentDecision[] | null;
    linkedLogs: IncidentLogDto[] | null;
}

export interface IncidentDecisionInputDto {
    objectId: number;
    meaning: IncidentMeaning;
    decision: IncidentDecision;
    decisionExplanation: string;
}

export interface IncidentDtoPageResult {
  items: IncidentDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}