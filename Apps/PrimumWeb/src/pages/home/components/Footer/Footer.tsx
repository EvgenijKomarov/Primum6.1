import styles from './Footer.module.css'
import Container from '../Container/Container'

interface FooterProps {
  logo?: string
  copyright?: string
}

export default function Footer({ logo = 'PRIMUMCODE_', copyright = '© 2026 PrimumCode' }: FooterProps) {
  return (
    <footer className={styles.footer}>
      <Container className={styles.inner}>
        <div className={styles.logo}>{logo}</div>
        <div className={styles.copy}>{copyright}</div>
      </Container>
    </footer>
  )
}
