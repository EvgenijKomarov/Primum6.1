import styles from './PhotoFrame.module.css'

interface PhotoFrameProps {
  src: string
  alt: string
}

export default function PhotoFrame({ src, alt }: PhotoFrameProps) {
  return (
    <div className={styles.frame}>
      <img src={src} alt={alt} />
    </div>
  )
}
