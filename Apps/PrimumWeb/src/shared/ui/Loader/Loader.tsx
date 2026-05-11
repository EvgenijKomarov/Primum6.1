import clsx from 'clsx';

import styles from './styles.module.css';

interface ILoader {
  position?: 'fixed' | 'absolute' | 'static';
  miniLoader?: boolean;
}

export const Loader = ({ position = 'fixed', miniLoader }: ILoader) => {
  return (
    <div
      className={clsx([
        miniLoader ? styles.miniLoader : styles.loader,
        position === 'absolute' && styles.abslt,
        position === 'static' && styles.sttc,
      ])}
    ></div>
  );
};
