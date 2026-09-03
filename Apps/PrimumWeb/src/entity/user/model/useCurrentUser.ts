import { useContext } from "react";
import { CurrentUserContext } from "./CurrentUser.Context";


export const useCurrentUser = () => {
  const ctx = useContext(CurrentUserContext);
  if (!ctx) throw new Error('useCurrentUser must be used within CurrentUserProvider');
  return ctx;
};