import { useCurrentUser } from '@/entity/user';
import { fetcherInstance } from '@/shared/api/axios';
import { FetchError } from '@/shared/api/fetchError';
import { useToast } from '@/shared/ui/Toast/useToast';
import { useEffect, useRef } from 'react';
import { useNavigate } from 'react-router';

interface RedirectPageProps {
  apiUrl: string;
  redirectTo: string;
  defaultRedirect?: string;
  onSuccessMessage: string;
}

export const RedirectPage = ({ apiUrl, onSuccessMessage,  redirectTo = '/profile', defaultRedirect = '/' }: RedirectPageProps) => {
  const navigate = useNavigate();
  const { mutate: mutateUser } = useCurrentUser();
  const hasRun = useRef(false);
  const { showToast } = useToast();

  useEffect(() => {
    if (hasRun.current) return;
    hasRun.current = true;

    const run = async () => {
      const raw = window.location.search;
      const token = raw.startsWith('?token=')
        ? decodeURIComponent(raw.slice(7))
        : null;

      if (!token) {
        navigate(defaultRedirect, { replace: true });
        return;
      }

      try {
        await fetcherInstance({
          url: apiUrl,
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          data: JSON.stringify(token),
        });
        await mutateUser(); // перезапросить пользователя, дождаться свежих данных
        showToast(onSuccessMessage, 'success', 5000);
        navigate(redirectTo, { replace: true });
      } catch (e) {
        console.log(e);
        if (e instanceof FetchError && e.status === 401) {
          showToast('Пожалуйста, войдите в профиль и перейдите по ссылке еще раз', 'error', 5000);
          navigate('/auth', { replace: true });
        } 
        else if (e instanceof FetchError && e.status === 404 && apiUrl == '/student/abonements/referal') {
          showToast('Для создания реферального абонемента необходимо создать профиль ученика', 'error', 8000);
          navigate('/profile', { replace: true });
        }
        else if (e instanceof FetchError && e.status === 500 && apiUrl == '/student/abonements/referal') {
          showToast('Нельзя подписаться на собственный курс', 'error', 5000);
          navigate('/profile', { replace: true });
        }
        else {
          showToast('Произошла ошибка', 'error', 5000);
          navigate(defaultRedirect, { replace: true });
        }
      }
    };

    run();
  }, []);

  return null;
};