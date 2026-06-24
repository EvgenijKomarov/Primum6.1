import { fetcherInstance } from '@/shared/api/axios';
import { FetchError } from '@/shared/api/fetchError';
import { useEffect, useRef } from 'react';
import { useNavigate } from 'react-router';

interface RedirectPageProps {
  apiUrl: string;
  redirectTo: string;
  defaultRedirect?: string;
}

export const RedirectPage = ({ apiUrl, redirectTo='/profile', defaultRedirect = '/' }: RedirectPageProps) => {
  const navigate = useNavigate();
  const hasRun = useRef(false);

  useEffect(() => {
    if (hasRun.current) return;
    hasRun.current = true;

    const run = async () => {
      const raw = window.location.search;
      const token = raw.startsWith('?token=')
        ? decodeURIComponent(raw.slice(7))
        : null;

      try {
        await fetcherInstance({
          url: apiUrl,
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          data: JSON.stringify(token),
        });
        navigate(redirectTo, { replace: true });
      } catch (e) {
        console.log(e);
        if (e instanceof FetchError && e.status === 401) {
          navigate('/auth', { replace: true });
        } else {
          navigate(defaultRedirect, { replace: true });
        }
      }
    };

    run();
  }, []);

  return null;
};