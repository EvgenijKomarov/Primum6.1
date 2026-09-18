import { clsx } from 'clsx';

import styles from './styles.module.css';

interface ILoader {
  position?: 'fixed' | 'absolute' | 'static';
  miniLoader?: boolean;
}

export const Loader = ({ position = 'fixed', miniLoader }: ILoader) => {
  return (
    <div
      className={clsx(
        styles.wrap,
        miniLoader ? styles.miniWrap : styles.loaderWrap,
        position === 'fixed' && styles.fixed,
        position === 'absolute' && styles.abslt,
        position === 'static' && styles.sttc
      )}
    >
      <div className={styles.pixel} />
    </div>
  );
};
