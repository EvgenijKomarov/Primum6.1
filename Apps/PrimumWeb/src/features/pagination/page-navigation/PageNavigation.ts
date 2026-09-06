import { useState, useEffect, useCallback } from 'react';

export function usePageNavigation<T>(
  fetcher: (page: number, pageSize: number) => Promise<{
    items: T[] | null;
    totalPages: number;
    currentPage: number;
  }>,
  pageSize = 20
) {
  const [page, setPage] = useState(0);
  const [state, setState] = useState<{
    items: T[];
    totalPages: number;
    loading: boolean;
  }>({ items: [], totalPages: 0, loading: true });

  useEffect(() => {
    let cancelled = false;

    fetcher(page, pageSize).then(res => {
      if (cancelled) return;
      setState({
        items: res.items ?? [],
        totalPages: res.totalPages,
        loading: false,
      });
    });

    return () => { cancelled = true; };
  }, [page, pageSize]);

  const goTo = useCallback((next: number | ((p: number) => number)) => {
    setState(prev => ({ ...prev, loading: true }));
    setPage(next);
  }, []);

  return { ...state, page, setPage: goTo };
}