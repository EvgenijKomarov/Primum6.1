import { useState, useEffect, useRef, useCallback } from 'react';

export function useInfiniteScroll<T>(
  fetcher: (page: number, pageSize: number) => Promise<{ items: T[] | null; totalPages: number }>,
  pageSize = 20
) {
  const [items, setItems] = useState<T[]>([]);
  const [page, setPage] = useState(0);
  const [loading, setLoading] = useState(false);
  const [hasMore, setHasMore] = useState(true);
  const observerRef = useRef<IntersectionObserver | null>(null);
  const triggerRef = useRef<HTMLDivElement | null>(null);

  const loadNext = useCallback(async () => {
    if (loading || !hasMore) return;
    setLoading(true);
    try {
      const res = await fetcher(page, pageSize);
      const newItems = res.items ?? [];
      setItems(prev => [...prev, ...newItems]);
      setPage(prev => prev + 1);
      setHasMore(page + 1 < res.totalPages);
    } finally {
      setLoading(false);
    }
  }, [page, loading, hasMore, fetcher, pageSize]);

  useEffect(() => {
    const el = triggerRef.current;
    if (!el) return;
    observerRef.current = new IntersectionObserver(
      ([entry]) => { if (entry.isIntersecting) loadNext(); },
      { threshold: 0.1 }
    );
    observerRef.current.observe(el);
    return () => observerRef.current?.disconnect();
  }, [loadNext]);

  return { items, loading, hasMore, triggerRef };
}