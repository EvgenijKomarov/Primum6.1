import { useEffect, useState } from "react";


export function useDebouncedValue<T>(initialValue: T, delay = 400) {
    const [value, setValue] = useState<T>(initialValue);
    const [debouncedValue, setDebounced] = useState(value);


    useEffect(() => {
        const timer = setTimeout(() => setDebounced(value), delay);
        return () => clearTimeout(timer);
    }, [value, delay]);

    return { debouncedValue, value, setValue };
}