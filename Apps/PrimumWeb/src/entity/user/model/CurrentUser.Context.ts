import type { Role } from "@/shared/enums";
import type { UserDto } from "./types";
import { createContext } from "react";

export type CurrentUserContextValue = {
  user: UserDto | undefined;
  role: Role;
  availableRoles: Role[];
  setActiveRole: (role: Role) => void;
  isLoading: boolean;
  mutate: () => void;
};

export const CurrentUserContext = createContext<CurrentUserContextValue | null>(null);