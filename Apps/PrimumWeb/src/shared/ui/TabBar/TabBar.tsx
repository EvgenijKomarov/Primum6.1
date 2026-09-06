import type { ReactNode } from 'react';
import styles from './TabBar.module.css';

export interface TabItem<T extends string = string> {
    value: T;
    label: string;
    content: ReactNode;
}

interface TabsProps<T extends string = string> {
    tabs: TabItem<T>[];
    value: T;
    onChange: (value: T) => void;
}

export function Tabs<T extends string = string>({ tabs, value, onChange }: TabsProps<T>) {
    return (
        <div>
            <div className={styles.filterBar} role="tablist">
                {tabs.map((tab) => (
                    <button
                        key={tab.value}
                        type="button"
                        role="tab"
                        aria-selected={value === tab.value}
                        aria-controls={`tabpanel-${tab.value}`}
                        id={`tab-${tab.value}`}
                        className={`${styles.chip} ${value === tab.value ? styles.chipActive : ''}`}
                        onClick={() => onChange(tab.value)}
                    >
                        {tab.label}
                    </button>
                ))}
            </div>
            <div className={styles.tabContent}>
                {tabs.map((tab) => (
                    <div
                        key={tab.value}
                        role="tabpanel"
                        id={`tabpanel-${tab.value}`}
                        aria-labelledby={`tab-${tab.value}`}
                        hidden={value !== tab.value}
                    >
                        {tab.content}
                    </div>
                ))}
            </div>
        </div>
    );
}